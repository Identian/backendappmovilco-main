using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Dominio.Core
{
  public class DocumentosDominio : IDocumentosDominio
  {
    private readonly IConfiguration _configuracion;
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IDocumentosRepositorio _documentosRepositorio;
    private readonly IDocumentosRepositorioSql _documentosRepositorioSql;
    private readonly IReportesRepositorio _reportesRepositorio;
    private readonly IClientesRepositorioApi _clientesRepositorioApi;
    private readonly IClientesRepositorioSql _clientesRepositorioSql;
    private readonly IProductosRepositorioSql _productosRepositorioSql;
    private readonly INumeracionAutorizadaRepositorio _numeracionAutorizadaRepositorio;
    private readonly IEstadoDocumentoRepositorio _estadoDocumentoRepositorio;
    private readonly IDeliveryRepositorio _deliveryRepositorio;
    private readonly IReferenciaDocumentosRepositorio _referenciaDocumentoRepositorio;
    private readonly IDispositivosAppMovilRepositorioSql _dispositivosAppMovilRepositorioSql;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public DocumentosDominio(IConfiguration configuracion, IRedisCacheRepositorio redisCacheRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IDocumentosRepositorio documentosRepositorio, IDocumentosRepositorioSql documentosRepositorioSql, IReportesRepositorio reportesRepositorio, IClientesRepositorioApi clientesRepositorioApi, IClientesRepositorioSql clientesRepositorioSql, IProductosRepositorioSql productosRepositorioSql, INumeracionAutorizadaRepositorio numeracionAutorizadaRepositorio, IEstadoDocumentoRepositorio estadoDocumentoRepositorio, IDeliveryRepositorio deliveryRepositorio, IReferenciaDocumentosRepositorio referenciaDocumentosRepositorio, IDispositivosAppMovilRepositorioSql dispositivosAppMovilRepositorioSql, IEmpresaRepositorio empresaRepositorio)
    {
      _configuracion = configuracion;
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _documentosRepositorio = documentosRepositorio;
      _documentosRepositorioSql = documentosRepositorioSql;
      _reportesRepositorio = reportesRepositorio;
      _clientesRepositorioApi = clientesRepositorioApi;
      _clientesRepositorioSql = clientesRepositorioSql;
      _productosRepositorioSql = productosRepositorioSql;
      _numeracionAutorizadaRepositorio = numeracionAutorizadaRepositorio;
      _estadoDocumentoRepositorio = estadoDocumentoRepositorio;
      _deliveryRepositorio = deliveryRepositorio;
      _referenciaDocumentoRepositorio = referenciaDocumentosRepositorio;
      _dispositivosAppMovilRepositorioSql = dispositivosAppMovilRepositorioSql;
      _empresaRepositorio = empresaRepositorio;
    }

    public RespuestaEmitirDocumento EmitirDocumento(SolicitudEmitirDocumento solicitudEmitirDocumento, string bearerToken, string valorBearerToken)
    {
      RespuestaEmitirDocumento respuesta = new();
      var respuestaToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);

      ConsultarInformacionProductos(solicitudEmitirDocumento.Factura!.DetalleDeFactura!, Convert.ToInt32(respuestaToken.IdEmpresa));
      CompletarDatosFacturaGeneral(solicitudEmitirDocumento, respuestaToken.IdEmpresa!);
      //Obtener correo de usuario en caché
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken.Replace("Bearer ", "")
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;

          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        //Validar sucripción dispositivo
        if (solicitudEmitirDocumento.TipoApp == "2" && solicitudEmitirDocumento.SerialLogico != null)
        {
          var validarSuscripcion = _dispositivosAppMovilRepositorioSql.ValidarSuscripcionDispositivoPorSerialLogico(respuestaToken.IdEmpresa!, solicitudEmitirDocumento.SerialLogico);
          if (validarSuscripcion.Codigo != 200)
          {
            respuesta.Codigo = validarSuscripcion.Codigo;
            respuesta.Resultado = validarSuscripcion.Resultado;
            respuesta.Mensaje = "El documento no superó las validaciones.";
            respuesta.MensajesValidacion = new List<string>()
            {
              validarSuscripcion.Mensaje ?? "Error al validar la suscripción del dispositivo"
            };
            return (respuesta);
          }
          else
          {
            solicitudEmitirDocumento.IdSuscripcion = validarSuscripcion.IdSuscripcion;
          }
        }

        //Obtener información cliente
        if (solicitudEmitirDocumento!.Factura.Cliente != null && solicitudEmitirDocumento!.Factura.Cliente.IdCliente != null && solicitudEmitirDocumento!.Factura.Cliente.IdCliente != "00")
        {
          var respuestaCliente = _clientesRepositorioSql.ConsultarPorId(solicitudEmitirDocumento.Factura.Cliente.IdCliente, respuestaToken.IdEmpresa!);
          if (respuestaCliente.Codigo == 200)
          {
            if (respuestaCliente.Datos != null)
            {
              if (respuestaCliente.Datos.Notificar == _configuracion["ServiciosFacturacion:EmisionRest:Destinatario:NotificarDestinatarioNull"])
              {
                respuestaCliente.Datos.Destinatario = null;
              }
              else if (respuestaCliente.Datos.Destinatario == null)
              {
                respuestaCliente.Datos.Notificar = _configuracion["ServiciosFacturacion:EmisionRest:Destinatario:NotificarDestinatarioNull"];
              }
            }
            solicitudEmitirDocumento.Factura.Cliente = respuestaCliente.Datos;
            //Enviar documento a servicio de Emisión
            respuesta = _documentosRepositorio.EmitirDocumento(solicitudEmitirDocumento, bearerToken);
          }
          else
          {
            respuesta.Codigo = respuestaCliente.Codigo;
            respuesta.Mensaje = respuestaCliente.Mensaje;
          }
        }
        else if (solicitudEmitirDocumento!.Factura.Cliente != null && solicitudEmitirDocumento.Factura.Cliente.IdCliente == "00")
        {
          //Enviar documento a servicio de Emisión
          respuesta = _documentosRepositorio.EmitirDocumento(solicitudEmitirDocumento, bearerToken);
        }
        else
        {
          solicitudEmitirDocumento.Factura.Cliente = _clientesRepositorioApi.ObtenerInformacionClienteEscenarioNull();
          //Enviar documento a servicio de Emisión
          respuesta = _documentosRepositorio.EmitirDocumento(solicitudEmitirDocumento, bearerToken);
        }
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      if (_configuracion["ServiciosFacturacion:EmisionRest:ApiEnviar:DevolverXML"] != "1")
      {
        respuesta.Xml = null;
      }
      return (respuesta);
    }

    private static string FormatoFechaHora(DateTime fechaHora, string formato, string diferenciaHoras)
    {
      DateTime resultado = fechaHora;
      if (string.IsNullOrEmpty(formato))
      {
        formato = "yyyy-MM-dd HH:mm:ss";
      }
      if (!string.IsNullOrEmpty(diferenciaHoras))
      {
        resultado = fechaHora.AddHours(Convert.ToInt32(diferenciaHoras));
      }
      return resultado.ToString(formato);
    }

    public string FormatoFechaHoraSolicitudes(DateTime fechaHora)
    {
      var formatoFechaHora = _configuracion["ServiciosFacturacion:Solicitudes:FormatoFechaHora"]!;
      var diferenciaHoras = _configuracion["ServiciosFacturacion:Solicitudes:DiferenciaHoras"]!;
      return FormatoFechaHora(fechaHora, formatoFechaHora, diferenciaHoras);
    }

    public string FormatoFechaHoraRespuestas(DateTime fechaHora)
    {
      var formatoFechaHora = _configuracion["ServiciosFacturacion:Respuestas:FormatoFechaHora"]!;
      var diferenciaHoras = _configuracion["ServiciosFacturacion:Respuestas:DiferenciaHoras"]!;
      return FormatoFechaHora(fechaHora, formatoFechaHora, diferenciaHoras);
    }

    public void CompletarDatosFacturaGeneral(SolicitudEmitirDocumento solicitudEmitirDocumento, string IdEmpresa)
    {
      var numeracionAutorizada = _numeracionAutorizadaRepositorio.ConsultarNumeracionAutorizada(solicitudEmitirDocumento.Factura!.IdRangoNumeracion!, IdEmpresa);
      if (numeracionAutorizada.Datos != null)
      {
        solicitudEmitirDocumento.Factura.TipoDocumento = numeracionAutorizada.Datos.TipoDocumento;
        solicitudEmitirDocumento.Factura.RangoNumeracion = numeracionAutorizada.Datos.RangoNumeracion;
      }
      solicitudEmitirDocumento.Factura.ConsecutivoDocumento = solicitudEmitirDocumento.Factura.ConsecutivoDocumento ?? "0";
      solicitudEmitirDocumento.Factura.Propina = solicitudEmitirDocumento.Factura.Propina ?? null;

      //FacturaDetalle
      solicitudEmitirDocumento.Factura.TotalProductos = solicitudEmitirDocumento.Factura.DetalleDeFactura!.Count().ToString();
      solicitudEmitirDocumento.Factura.Extras = solicitudEmitirDocumento.Factura.Extras ?? null;
    }

    public void ConsultarInformacionProductos(IEnumerable<FacturaDetalle> detalleDeFactura, int idEmpresa)
    {
      //Obtener información productos
      if (detalleDeFactura.Any())
      {
        int secuencia = 1;
        foreach (FacturaDetalle facturaDetalle in detalleDeFactura)
        {
          facturaDetalle.Secuencia = secuencia.ToString();
          if (facturaDetalle.IdProducto != null)
          {
            int idProducto = Convert.ToInt32(facturaDetalle.IdProducto);
            var producto = _productosRepositorioSql.ConsultarPorId(idProducto, idEmpresa);
            if (producto.Codigo == 200 && producto.Datos != null)
            {
              facturaDetalle.CodigoProducto = producto.Datos.CodigoProducto;
              facturaDetalle.Descripcion = producto.Datos.Descripcion;
              facturaDetalle.Nota = producto.Datos.Nota;
              facturaDetalle.CantidadPorEmpaque = producto.Datos.CantidadPorEmpaque;
              facturaDetalle.EstandarCodigoProducto = producto.Datos.EstandarCodigoProducto;
              facturaDetalle.EstandarCodigo = producto.Datos.EstandarCodigo;
              facturaDetalle.CantidadReal = producto.Datos.CantidadReal;
            }
          }
          secuencia++;
        }
      }
    }

    public RespuestaConsultarDocumentos ConsultarDocumentos(SolicitudReporteEnLinea solicitudReporteEnLinea, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarDocumentos respuesta = new();

      if (!string.IsNullOrEmpty(solicitudReporteEnLinea.Sistema) && solicitudReporteEnLinea.Sistema != "1")
      {
        respuesta.Codigo = 403;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Sistema no disponible";
      }
      else
      {
        switch (solicitudReporteEnLinea.FormatoRequerido)
        {
          case "json":
            solicitudReporteEnLinea.Filtros!.CodigoReporte = _reportesRepositorio.ObtenerCodigoReporteGeneralEnLinea(solicitudReporteEnLinea.Sistema!);
            respuesta = ConsultarDocumentosFormatoJson(solicitudReporteEnLinea, bearerToken);
            break;

          default:
            respuesta.Codigo = 403;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Formato de respuesta no disponible";
            break;
        }
      }

      var respuestaDatosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
      respuesta.IdEmpresa = respuestaDatosToken.IdEmpresa;
      respuesta.Nit = respuestaDatosToken.NitEmpresa;
      return respuesta;
    }

    private RespuestaConsultarDocumentos ConsultarDocumentosFormatoJson(SolicitudReporteEnLinea solicitudReporteEnLinea, string bearerToken)
    {
      RespuestaConsultarDocumentos respuesta = new();
      var respuestaReporte = _reportesRepositorio.ReporteEnLinea(solicitudReporteEnLinea, bearerToken);
      if (respuestaReporte.Codigo == 200)
      {
        respuesta.Codigo = respuestaReporte.Codigo;
        respuesta.Resultado = "Exitoso";
        respuesta.Mensaje = "Consulta Exitosa";
        respuesta.TotalDocumentos = 0;
        if (!string.IsNullOrEmpty(respuestaReporte.ReporteCodificado))
        {
          string documentos = Encoding.UTF8.GetString(Convert.FromBase64String(respuestaReporte.ReporteCodificado));
          var objetoJson = JObject.Parse(documentos);
          respuesta.Documentos = objetoJson.First!.First;
          respuesta.TotalDocumentos = respuesta.Documentos!.Count();
        }
      }
      else
      {
        respuesta.Codigo = respuestaReporte.Codigo;
        if (respuesta.Codigo == 201) { respuesta.Codigo = 404; }
        respuesta.Resultado = respuestaReporte.Resultado;
        respuesta.Mensaje = respuestaReporte.Mensaje;
        respuesta.Errores = respuestaReporte.Errores;
      }
      return respuesta;
    }

    public RespuestaConsultarEstadoDocumento ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacion solicitudConsultarDocumento, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarEstadoDocumento respuesta = new();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        var respuestaDocumento = _estadoDocumentoRepositorio.ConsultarDocumentoPorConsecutivoFactura(solicitudConsultarDocumento, bearerToken);
        if (respuestaDocumento.Documento!.Codigo == 200)
        {
          switch (solicitudConsultarDocumento.Plataforma)
          {
            case "TFHKA":
              var respuestaDatosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
              respuesta = respuestaDocumento;
              respuesta.Plataforma = solicitudConsultarDocumento.Plataforma;
              respuesta.Nit = solicitudConsultarDocumento.Nit;
              respuesta.IdEmpresa = respuestaDatosToken.IdEmpresa;
              respuesta.Codigo = respuesta.Documento.Codigo;
              respuesta.Mensaje = respuesta.Documento.Mensaje;
              respuesta.Resultado = respuesta.Documento.Resultado;
              break;
            default:
              respuesta.Codigo = 403;
              respuesta.Resultado = "Error";
              respuesta.Mensaje = "Funcionalidad no disponible";
              break;
          }
        }
        else
        {
          respuesta.Codigo = respuestaDocumento.Codigo;
          respuesta.Resultado = respuestaDocumento.Resultado;
          respuesta.Mensaje = respuestaDocumento.Mensaje;
          respuesta.Documento = null;
        }
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }

      return (respuesta);
    }

    public RespuestaConsultarReferenciaDocumento ConsultarReferenciaDocumentosFactura(SolicitudConsultarReferenciaDocumentoFacturacion solicitudConsultarDocumento, string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarReferenciaDocumento respuesta = new();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Message = "Datos Token inválidos";
            return respuesta;
          }

        }
      }

      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {


        switch (solicitudConsultarDocumento.Plataforma)
        {
          case "TFHKA":
            var respuestaDocumento = _referenciaDocumentoRepositorio.ConsultarRefereciaDocumentoFactura(solicitudConsultarDocumento, bearerToken);
            if (respuestaDocumento.Codigo == 200)
            {

              var respuestaDatosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
              respuesta = respuestaDocumento;
              respuesta.Plataforma = solicitudConsultarDocumento.Plataforma;
              respuesta.Nit = solicitudConsultarDocumento.Nit;
              respuesta.IdEmpresa = respuestaDatosToken.IdEmpresa;
            }
            else
            {
              respuesta.Codigo = respuestaDocumento.Codigo;
              respuesta.Resultado = respuestaDocumento.Resultado;
              respuesta.Message = respuestaDocumento.Message;
              respuesta.Extras = null;
              respuesta.Rango = null;
              respuesta.DocumentosAdjuntos = null;
              respuesta.IdUsuario = null;
            }
            break;
          default:
            respuesta.Codigo = 403;
            respuesta.Resultado = "Error";
            respuesta.Message = "Funcionalidad no disponible";
            break;
        }

      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Message = "Se ha cerrado la sesión del usuario";
      }

      return (respuesta);
    }

    public RespuestaEnviarCorreoIndividual EnviarCorreoIndividual(SolicitudEnviarCorreoIndividual solicitud, string bearerToken, string valorBearerToken)
    {
      RespuestaEnviarCorreoIndividual respuesta = new();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        respuesta = _deliveryRepositorio.EnviarCorreoIndividual(solicitud, bearerToken, valorBearerToken);
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      return (respuesta);
    }

    public RespuestaConsultarMontoFacturaPos ConsultarMontoFacturaPos(string bearerToken, string valorBearerToken)
    {
      RespuestaConsultarMontoFacturaPos respuesta = new();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        respuesta = _documentosRepositorioSql.ConsultarMontoFacturaPos();
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      return respuesta;
    }
  }
}
