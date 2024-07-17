using Aplicacion.Dto.EstadoDocumento;
using Aplicacion.Dto.Respuestas;
using Dominio.Entidad;
using Dominio.Entidad.ReferenciaDocumento;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Aplicacion.Entidad.Respuestas;
using Aplicacion.Entidad.Documentos;
using Aplicacion.Entidad.ReferenciaDocumento;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasReferenciaDocumento
  {
    public RespuestaConsultarReferenciaDocumento DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1 { get; set; }
    public RespuestaConsultarReferenciaDocumentoDto DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1Dto { get; set; }
    public RespuestaConsultarReferenciaDocumento DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2 { get; set; }
    public RespuestaConsultarReferenciaDocumentoDto DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2Dto { get; set; }
    public RespuestaConsultarReferenciaDocumento DatosDocumentoNoExisteConsultarReferenciaDocumento { get; set; }
    public RespuestaConsultarReferenciaDocumentoDto DatosDocumentoNoExisteConsultarReferenciaDocumentoDto { get; set; }
    public RespuestaConsultarReferenciaDocumento PlataformaNoDisponibleConsultarReferenciaDocumento { get; set; }
    public RespuestaConsultarReferenciaDocumento SeHaCerradoSesionConsultarUsuario { get; set; }
    public RespuestaConsultarReferenciaDocumentoDto SeHaCerradoSesionConsultarUsuarioDto { get; set; }

    public const int CodigoUsuarioNoExiste = 404;
    public const int CodigoDocumentoNoDisponible = 101;
    public const string ResultadoError = "Error";
    public const string MensajeUsuarioNoExiste = "Documento no disponible, no tiene ningún documento rechazado por corregir.";
    public const int CodigoSessionCerrada = 401;
    public const string MensajeSesionCerrada = "Se ha cerrado la sesión del usuario";
    private void InicializarRespuestas()
    {
      DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1 = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = "Procesado",
        Message = "Documento Procesado.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Rango = new()
        {
           IdRango = 1221,
         RangoInicio =  "1" ,
         RangoFin =  "999999999" ,
         Prefijo =  "ARFVCRED" ,
         Documenid =  "ARFVCRED7"
        },
        Sucursales = new()
        {
          idSucursales = 111,
          description = "ALCIDER",
          code = "156486",
          establecimiento = "E-16",         
        },
        Retenciones = new List<ListaRetenciones>
        { },
        Factura = new()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalle>
          {
            new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePago>()
          {
            new MediosDePago
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null,

        Errores = null
      };

      DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2 = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = "Procesado",
        Message = "Documento candidato a corregir.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Rango = new()
        {
          IdRango = 1221,
          RangoInicio = "1",
          RangoFin = "999999999",
          Prefijo = "ARFVCRED",
          Documenid = "ARFVCRED7"
        },
        Sucursales = new()
        {
          idSucursales = 111,
          description = "ALCIDER",
          code = "156486",
          establecimiento = "E-16",
        },
        Retenciones = new List<ListaRetenciones>
        { },
        Factura = new()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalle>
          {
            new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePago>()
          {
            new MediosDePago
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null,

        Errores = null
      };


      DatosDocumentoNoExisteConsultarReferenciaDocumento = new()
      {
        Codigo = CodigoDocumentoNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido,
        Message = MensajeUsuarioNoExiste,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Rango = null,
        Sucursales = null,
        Retenciones = null,
        Factura = null,
        Errores = null
      };


      SeHaCerradoSesionConsultarUsuario = new()
      {
        Codigo = CodigoSessionCerrada,
        Resultado = ResultadoError,
        Message = MensajeSesionCerrada,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Rango = null,
        Sucursales = null,
        Retenciones = null,
        Factura = null,
        Errores = null
      };

      PlataformaNoDisponibleConsultarReferenciaDocumento = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible,
        Message = ConstantesCompartidasFacturacion.MensajePlataformaNoDisponible,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Rango = null,
        Sucursales = null,
        Retenciones = null,
        Factura = null,
        Errores = null
      };
    }


    private void InicializarRespuestasDto()
    {
      DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1Dto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = "Procesado",
        Mensaje = "Documento Procesado.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Rango = new()
        {
          IdRango = 1221,
          RangoInicio = "1",
          RangoFin = "999999999",
          Prefijo = "ARFVCRED",
          Documenid = "ARFVCRED7"
        },
        Sucursales = new()
        {
          idSucursales = 111,
          description = "ALCIDER",
          code = "156486",
          establecimiento = "E-16",
        },
        Retenciones = new List<ListaRetencionesDto>
        { },
        Factura = new FacturaGeneralDto()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalleDto>
          {
            new FacturaDetalleDto
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalleDto
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePagoDto>()
          {
            new MediosDePagoDto
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null,

        Errores = null
      };

      DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2Dto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = "Procesado",
        Mensaje = "Documento candidato a corregir.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Rango = new()
        {
          IdRango = 1221,
          RangoInicio = "1",
          RangoFin = "999999999",
          Prefijo = "ARFVCRED",
          Documenid = "ARFVCRED7"
        },
        Sucursales = new()
        {
          idSucursales = 111,
          description = "ALCIDER",
          code = "156486",
          establecimiento = "E-16",
        },
        Retenciones = new List<ListaRetencionesDto>
        { },
        Factura = new FacturaGeneralDto()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalleDto>
          {
            new FacturaDetalleDto
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalleDto
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePagoDto>()
          {
            new MediosDePagoDto
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null,

        Errores = null
      };


      DatosDocumentoNoExisteConsultarReferenciaDocumentoDto = new()
      {
        Codigo = CodigoDocumentoNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido,
        Mensaje = MensajeUsuarioNoExiste,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Rango = null,
        Sucursales = null,
        Retenciones = null,
        Factura = null,
        Errores = null
      };


      

      SeHaCerradoSesionConsultarUsuarioDto = new()
      {
        Codigo = CodigoSessionCerrada,
        Resultado = ResultadoError,
        Mensaje = MensajeSesionCerrada,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Rango = null,
        Sucursales = null,
        Retenciones = null,
        Factura = null,
        Errores = null
      };

    }


    public RespuestasCompartidasReferenciaDocumento()
    {
      InicializarRespuestas();
      InicializarRespuestasDto();
    }
  }
}
