using Aplicacion.Dto.EstadoDocumento;
using Aplicacion.Dto.Respuestas;
using Dominio.Entidad;
using Dominio.Entidad.EstadoDocumento;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasEstadoDocumento
  {
    public RespuestaConsultarEstadoDocumento DatosExistentesConsultarEstadoDocumento { get; set; }
    public RespuestaConsultarEstadoDocumentoDto DatosExistentesConsultarEstadoDocumentoDto { get; set; }
    public RespuestaConsultarEstadoDocumento DatosDocumentoNoExisteConsultarEstadoDocumento { get; set; }
    public RespuestaConsultarEstadoDocumentoDto DatosDocumentoNoExisteConsultarEstadoDocumentoDto { get; set; }
    public RespuestaConsultarEstadoDocumento SeHaCerradoSesionConsultarUsuario { get; set; }
    public RespuestaConsultarEstadoDocumentoDto SeHaCerradoSesionConsultarUsuarioDto { get; set; }

    public const int CodigoUsuarioNoExiste = 404;
    public const string ResultadoError = "Error";
    public const string MensajeUsuarioNoExiste = "No se encontraron datos para el usuario";
    public const int CodigoSessionCerrada = 401;
    public const string MensajeSesionCerrada = "Se ha cerrado la sesión del usuario";
    private void InicializarRespuestas()
    {
      DatosExistentesConsultarEstadoDocumento = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = "Se retornan datos de la Factura.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Documento = new EstadoDocumentoFacturacion(){
              Codigo = 200,
              Resultado = "Exitoso",
              Mensaje = "Se retornan datos de la Factura.",
              FechaDocumento = "2023-06-08 00:00:00-05:00",
              Cufe = "0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
              EstatusDocumento = 0,
              MensajeDocumento = "Procesado Correctamente",
              TipoDocumento = "92",
              DescripcionDocumento = "Nota Débito",
              Consecutivo = "ARFVCRED6",
              Ambiente = "2",
              TipoCufe = "CUDE-SHA384",
              CadenaCufe = "ARFVCRED62023-06-0800:00:00-05:001003.0001190.57040.00030.001193.57900390126123141231541196052",
              EntregaMetodoDIAN = null,
              DescripcionEstatusDocumento = "Procesado Correctamente",
              ReglasValidacionDIAN =  new List<string> { " " },
              PoseeRepresentacionGrafica = true,
              PoseeAdjuntos = false,
              AceptacionFisica = false,
              HistorialDeEntregas = new List<HistorialEntrega>()
              {
                new HistorialEntrega(){
              CanalDeEntrega = "0",
              Email = new List<string> { " " },
              NitProveedorReceptor = "",
              Telefono = "",
              MensajePersonalizado = "",
              FechaProgramada = "",
              EntregaEstatus = "0",
              EntregaEstatusDescripcion = "Pending",
              EntregaFecha = "",
              RecepcionEmailComentario = "",
              RecepcionEmailEstatus = "0",
              RecepcionEmailFecha = "",
              RecepcionEmailIPAddress = "",
              LeidoEstatus = "",
              LeidoFecha = "",
              LeidoEmailIPAddress = null
              }
              },
              EsValidoDIAN = true,
              TrackID = "0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
              CadenaCodigoQR = "NumFac: ARFVCRED6\r\nFecFac: 2023-06-08\r\nHorFac: 00:00:00-05:00\r\nNitFac: 900390126\r\nDocAdq: 123141231541\r\nValFac: 1003.00\r\nValIva: 190.57\r\nValOtroIm: 0.00\r\nValTolFac: 1193.57\r\nCUFE: 0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef\r\nhttps://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
              Eventos = null,
              FechaAceptacionDIAN = "2023-06-14 19:30:49-05:00",
              AcuseEstatus = "0",
              AcuseRespuesta = "0",
              AcuseComentario = "",
              AcuseResponsable = ""
        },

        Errores = null
      };


      DatosDocumentoNoExisteConsultarEstadoDocumento = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNroDocumentoInvalido,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido,
        Mensaje = ConstantesCompartidasFacturacion.MensajeNroDocumentoInvalido,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Documento = new EstadoDocumentoFacturacion()
        {
          Codigo = ConstantesCompartidasFacturacion.CodigoNroDocumentoInvalido,
          Resultado = ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido,
          Mensaje = ConstantesCompartidasFacturacion.MensajeNroDocumentoInvalido
        },
        Errores = null
      };


      SeHaCerradoSesionConsultarUsuario = new()
      {
        Codigo = CodigoSessionCerrada,
        Resultado = ResultadoError,
        Mensaje = MensajeSesionCerrada,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Documento = null,
        Errores = null
      };
    }


    private void InicializarRespuestasDto()
    {
      DatosExistentesConsultarEstadoDocumentoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = "Se retornan datos de la Factura.",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Plataforma = "TFHKA",
        Documento = new EstadoDocumentoFacturacionDto()
        {
          FechaDocumento = "2023-06-08 00:00:00-05:00",
          Cufe = "0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
          EstatusDocumento = "0",
          MensajeDocumento = "Procesado Correctamente",
          TipoDocumento = "92",
          DescripcionDocumento = "Nota Débito",
          Consecutivo = "ARFVCRED6",
          Ambiente = "2",
          TipoCufe = "CUDE-SHA384",
          CadenaCufe = "ARFVCRED62023-06-0800:00:00-05:001003.0001190.57040.00030.001193.57900390126123141231541196052",
          EntregaMetodoDIAN = null,
          DescripcionEstatusDocumento = "Procesado Correctamente",
          ReglasValidacionDIAN = new List<string> { " " },
          PoseeRepresentacionGrafica = "True",
          PoseeAdjuntos = "False",
          AceptacionFisica = "False",
          HistorialDeEntregas = new List<HistorialEntregaDto>()
              {
                new HistorialEntregaDto(){
              CanalDeEntrega = "0",
              Email = new List<string> { " " },
              NitProveedorReceptor = "",
              Telefono = "",
              MensajePersonalizado = "",
              FechaProgramada = "",
              EntregaEstatus = "0",
              EntregaEstatusDescripcion = "Pending",
              EntregaFecha = "",
              RecepcionEmailComentario = "",
              RecepcionEmailEstatus = "0",
              RecepcionEmailFecha = "",
              RecepcionEmailIPAddress = "",
              LeidoEstatus = "",
              LeidoFecha = "",
              LeidoEmailIPAddress = null
              }
              },
          EsValidoDIAN = "True",
          TrackID = "0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
          CadenaCodigoQR = "NumFac: ARFVCRED6\r\nFecFac: 2023-06-08\r\nHorFac: 00:00:00-05:00\r\nNitFac: 900390126\r\nDocAdq: 123141231541\r\nValFac: 1003.00\r\nValIva: 190.57\r\nValOtroIm: 0.00\r\nValTolFac: 1193.57\r\nCUFE: 0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef\r\nhttps://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=0c379654817f643f36d7e6ad0f7bb03e20e943518577efd8aacc721eace426871165a1315668252267cea2c9dba267ef",
          Eventos = null,
          FechaAceptacionDIAN = "2023-06-14 19:30:49-05:00",
          AcuseEstatus = "0",
          AcuseRespuesta = "0",
          AcuseComentario = "",
          AcuseResponsable = ""
        },

        Errores = null
      };


      DatosDocumentoNoExisteConsultarEstadoDocumentoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNroDocumentoInvalido,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido,
        Mensaje = ConstantesCompartidasFacturacion.MensajeNroDocumentoInvalido,
        Nit = null,
        IdEmpresa = null,
        Plataforma = null,
        Documento = null,
        Errores = null
      };


    }


    public RespuestasCompartidasEstadoDocumento()
    {
      InicializarRespuestas();
      InicializarRespuestasDto();
    }
  }
}
