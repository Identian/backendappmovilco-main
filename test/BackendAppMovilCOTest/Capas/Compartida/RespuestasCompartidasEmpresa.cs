using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasEmpresa
  {
    public FacturaDetalle RespuestaProductoDummy = new();
    public RespuestaConsultarEmpresaFacturacion RespuestaConsultarEmpresaExisteDB = new();
    public RespuestaConsultarEmpresaFacturacion RespuestaConsultarEmpresaNoExisteDB = new();
    public RespuestaConsultarEmpresaFacturacion RespuestaConsultarEmpresaDatosTokenInvalidos = new();
    public JToken? RespuestaConsultarEmpresaExisteApi;
    public JToken? RespuestaConsultarNitNoExisteApi;
    public JToken? RespuestaTokensInvalidosConsultarEmpresaApi;
    public JToken? RespuestaPlataformaNoDisponibleConsultarEmpresaApi;
    public JToken? RespuestaPlataformaInvalidaConsultarEmpresa;
    public JToken? RespuestaDatosInvalidosConsultarEmpresaApi;
    public JToken? RespuestaConsultarEmpresaNoAutenticado;

    public RespuestaConsultarClaveSecreta ConsultarClaveSecretaValido = new();
    public RespuestaConsultarClaveSecreta ConsultarClaveSecretaInvalido = new();

    public const int CodigoExitoso = 200;
    public const string ResultadoExitoso = "Procesado";
    public const string MensajeEmpresaExiste = "Consulta Exitosa";

    public const int CodigoEmpresaNoExiste = 404;
    public const string ResultadoEmpresaNoExiste = "Procesado";
    public const string MensajeEmpresaNoExiste = "No se encontro la empresa con el id solicitado";

    public const int CodigoSessionCerrada = 401;
    public const string MensajeSesionCerrada = "Se ha cerrado la sesión del usuario";
    public const string ResultadoError = "Error";

    public RespuestasCompartidasEmpresa()
    {
      InicializarRespuestas();
    }

    public void InicializarRespuestas()
    {
      RespuestaProductoDummy = ProductoPrueba();
      InicializarRespuestasConsultarEmpresaBD();
      InicializarRespuestarConsultarEmpresaApi();
      InicializarRespuestasClaveSecreta();
    }

    private static FacturaDetalle ProductoPrueba()
    {
      return new FacturaDetalle
      {
        CantidadPorEmpaque = "1",
        CantidadReal = "1.00",
        CantidadRealUnidadMedida = "WSD",
        CantidadUnidades = "1.00",
        CodigoProducto = "P000001",
        Descripcion = "Impresora HKA80",
        DescripcionTecnica = "Impresora térmica de punto de venta, ideal para puntos de venta con alto rendimiento",
        EstandarCodigo = "999",
        EstandarCodigoProducto = "PHKA80",
        ImpuestosDetalles = new List<FacturaImpuestos>
        {
          new FacturaImpuestos
          {
            CodigoTOTALImp = "01",
            PorcentajeTOTALImp = "19.00",
            BaseImponibleTOTALImp = "1003.00",
            ValorTOTALImp = "190.57",
            ControlInterno = "",
            UnidadMedidaTributo = "",
            UnidadMedida = "",
            ValorTributoUnidad = ""
          }
        },
        ImpuestosTotales = new List<ImpuestosTotales>
        {
          new ImpuestosTotales
          {
            CodigoTOTALImp = "01",
            MontoTotal = "190.57"
          }
        },
        Marca = "HKA",
        MuestraGratis = "0",
        PrecioTotal = "90.00",
        PrecioTotalSinImpuestos = "1003.00",
        PrecioVentaUnitario = "1003.00",
        Secuencia = "1",
        UnidadMedida = "WSD"
      };
    }

    private void InicializarRespuestasConsultarEmpresaBD()
    {
      RespuestaConsultarEmpresaExisteDB = new()
      {
        Codigo = CodigoExitoso,
        Resultado = ResultadoExitoso,
        Mensaje = MensajeEmpresaExiste,
        Contribuyente = new()
        {
          IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        }
      };

      RespuestaConsultarEmpresaNoExisteDB = new()
      {
        Codigo = CodigoEmpresaNoExiste,
        Resultado = ResultadoEmpresaNoExiste,
        Mensaje = MensajeEmpresaNoExiste
      };

      RespuestaConsultarEmpresaDatosTokenInvalidos = new()
      {
        Codigo = CodigoExitoso,
        Resultado = ResultadoExitoso,
        Mensaje = MensajeEmpresaExiste,
        Contribuyente = new()
        {
          IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
          NumeroIdentificacion = ConstantesCompartidasFacturacion.NitExistente,
          TokenEmpresa = ConstantesCompartidasFacturacion.TokenEmpresaValido,
          TokenClave = ConstantesCompartidasFacturacion.TokenClaveValido,
        }
      };
    }

    private void InicializarRespuestarConsultarEmpresaApi()
    {
      RespuestaConsultarEmpresaExisteApi = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Contribuyente = new
        {
          IdEmpresa = "1",
          Tipo = "1",
          DescripcionTipo = "PruebaTipoContribuyente",
          RazonSocial = "PruebaRazonSocial",
          FechaCreacion = "2022-11-01",
          FechaActualizacion = "2022-11-02",
          Entorno = "0",
          TokenEmpresa = "1231654489481231894das4d8a4d3a",
          TokenClave = "1231654489481231894das4d8a4d3a",
          Activo = 1,
          TipoIdentificacion = "1",
          DescripcionTipoIdentificacion = "Registro civil",
          NumeroIdentificacion = "12345615",
          NumeroIdentificacionDv = "2",
          NombreComercial = "PruebaNombreComercial",
          TieneIntegracion = 1,
          TieneRecepcion = 1,
          TieneAppMovil = 1,
          TieneMetodo = 1,
          Estatus = "1",
          TieneInteroperabilidad = 1,
          EsCasaSoftware = 1,
          TieneConsorcio = 1,
          IdHistorial = 1,
          CodigoTipoRegimen = "1",
          TieneNomina = 1,
          EntornoNomina = "1",
          EstatusProveedor = "1",
          TieneRadian = 1
        }
      });

      RespuestaConsultarNitNoExisteApi = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNitNoExiste,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNitNoExiste,
        Mensaje = ConstantesCompartidasFacturacion.MensajeNitNoExiste
      });

      RespuestaTokensInvalidosConsultarEmpresaApi = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoTokensInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoTokensInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeTokensInvalidos
      });

      RespuestaPlataformaNoDisponibleConsultarEmpresaApi = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible,
        Mensaje = ConstantesCompartidasFacturacion.MensajePlataformaNoDisponible
      });

      RespuestaPlataformaInvalidaConsultarEmpresa = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosInvalidos,
        Errores = new List<string>()
        {
          new string("'Plataforma' inválida")
        }
      });

      RespuestaDatosInvalidosConsultarEmpresaApi = JToken.FromObject(new
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNitNoExiste,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.NoTieneClaveSecreta
      });

      RespuestaConsultarEmpresaNoAutenticado = JToken.FromObject(new RespuestaBase
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      });
    }

    private void InicializarRespuestasClaveSecreta()
    {
      ConsultarClaveSecretaValido = new()
      {
        ClaveSecreta = ConstantesCompartidasFacturacion.ClaveSecretaValida
      };
    }
  }
}
