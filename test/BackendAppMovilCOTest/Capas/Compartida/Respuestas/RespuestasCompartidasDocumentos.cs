using Aplicacion.Dto.Respuestas;
using Dominio.Entidad.Respuestas;
using Newtonsoft.Json.Linq;

namespace BackendAppMovilCOTest.Capas.Compartida.Respuestas
{
  public class RespuestasCompartidasDocumentos
  {
    public RespuestaEmitirDocumento? RespuestaDocumentoEmitidoExitosamente { get; set; }
    public RespuestaEmitirDocumento? RespuestaDocumentoRechazado { get; set; }
    public RespuestaEmitirDocumento? RespuestaDocumentoErrorValidaciones { get; set; }
    public RespuestaEmitirDocumento? RespuestaDocumentoNoAutenticado { get; set; }
    public RespuestaEmitirDocumento? RespuestaDatosTokenInvalidosEmitirDocumento { get; set; }

    public RespuestaEmitirDocumento? RespuestaSuscripcionDispositivoNoActiva { get; set; }
    public RespuestaEmitirDocumento? RespuestaSuscripcionDispositivoNoVigente { get; set; }
    public RespuestaEmitirDocumento? RespuestaSuscripcionDispositivoAunNoVigente { get; set; }
    public RespuestaEmitirDocumento? RespuestaSuscripcionDispositivoInexistente { get; set; }
    public RespuestaEmitirDocumento? RespuestaSuscripcionDispositivoNoAsociadoAEmpresa { get; set; }

    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosExitoso { get; set; }
    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosSistemaNoDisponible { get; set; }
    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosFormatoRespuestaNoDisponible { get; set; }
    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosNoAutenticado { get; set; }
    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosFormatoRespuestaErrorValidaciones { get; set; }
    public RespuestaConsultarDocumentos? RespuestaConsultarDocumentosSinDocumentos { get; set; }
    public RespuestaConsultarEstadoDocumento? RespuestaDatosTokenInvalidosConsultarDocumento { get; set; }
    public RespuestaConsultarReferenciaDocumento? RespuestaDatosTokenInvalidosConsultarReferenciaDocumento { get; set; }


    public RespuestaConsultarMontoFacturaPosDto? RespuestaConsultarMontoFacturaPosExitosaDto { get; set; }
    public RespuestaConsultarMontoFacturaPosDto? RespuestaConsultarMontoFacturaPosNoAutenticadoDto { get; set; }
    public RespuestaConsultarMontoFacturaPos? RespuestaConsultarMontoFacturaPosExitosa { get; set; }
    public RespuestaConsultarMontoFacturaPos? RespuestaConsultarMontoFacturaPosNoAutenticado { get; set; }
    public RespuestaConsultarMontoFacturaPos? RespuestaDatosTokenInvalidosFacturaPos { get; set; }


    public const int CodigoExitoso = 200;
    public const string ResultadoExitoso = "Procesado";
    public const string MensajeExitoso = "Documento se envío correctamente";

    public const int CodigoErrorValidaciones = 400;
    public const string ResultadoError = "Error";
    public const string MensajeErrorValidaciones = "Los datos presentes en la solicitud no han pasado las validaciones";

    public const int CodigoNoAutenticado = 401;

    public const int CodigoSinDocumentos = 404;
    public const string MensajeSinDocumentos = "No se consiguieron registros con los filtros aplicados";
    public const string ResultadoSinDocumentos = "Procesado.";

    public const string SistemaDisponible = "1";
    public const string FormatoRespuestaDisponible = "json";

    public const string FechaRespuestaEmitirDocumento = "2023-02-14 16:40:53";

    public void InicializarRespuestas()
    {
      RespuestaDocumentoEmitidoExitosamente = new()
      {
        Codigo = CodigoExitoso,
        Mensaje = MensajeExitoso,
        Cufe = "88b350c4c5b69d50190d1152ae43a5a5bdde6754e9b2accde122cb913d",
        TipoCufe = "CUFE-SHA384",
        ConsecutivoDocumento = "PREFIJO1",
        FechaRespuesta = FechaRespuestaEmitirDocumento,
        Resultado = ResultadoExitoso,
        Xml = null,
        Hash = "a46ad4dcfasdfwec7d7a56cba45ae5cd8d8fc0ab",
        Nombre = "ad0900390126016223b02e927",
        Qr = "NumFac: PREFIJO1\r\nFecFac: 2023-02-14\r\nHorFac: 16:40:53-05:00\r\nNitFac: 900390126\r\nDocAdq: 900860816\r\nValFac: 345000.00\r\nValIva: 65550.00\r\nValOtroIm: 0.00\r\nValTolFac: 410550.00\r\nCUFE: 88b350c4c5b69d50190d1152ae43a5a5bdde6754e9b2accde122cb913d\r\nhttps://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=88b350c4c5b69d50190d1152ae43a5a5bdde6754e9b2accde122cb913d",
        EsValidoDian = true,
        MensajesValidacion = null,
        ReglasValidacionDIAN = null,
        ReglasNotificacionDIAN = new List<string>()
        {
          "Regla: SinCódigo, Notificación: La validación del estado del RUT próximamente estará disponible."
        },
        FechaAceptacionDIAN = "2022-02-14 16:40:52-05:00"
      };

      RespuestaDocumentoRechazado = new()
      {
        Codigo = 99,
        Mensaje = "El documento se envío obteniendo el siguiente mensaje: Validación contiene errores en campos mandatorios.",
        Cufe = "",
        TipoCufe = null,
        ConsecutivoDocumento = "",
        FechaRespuesta = FechaRespuestaEmitirDocumento,
        Resultado = "Error",
        Xml = "",
        Hash = "",
        Nombre = null,
        Qr = null,
        EsValidoDian = false,
        MensajesValidacion = null,
        ReglasValidacionDIAN = new List<string>()
        {
          "Regla: FAD09c, Rechazo: La fecha de emision es anterior en mas de 10 dias de la fecha actual"
        },
        ReglasNotificacionDIAN = null,
        FechaAceptacionDIAN = null
      };

      RespuestaDocumentoErrorValidaciones = new()
      {
        Codigo = CodigoErrorValidaciones,
        Mensaje = MensajeErrorValidaciones,
        Cufe = "",
        TipoCufe = null,
        ConsecutivoDocumento = "",
        FechaRespuesta = "2023-02-14 16:40:53-05:00",
        Resultado = ResultadoError,
        Xml = "",
        Hash = "",
        Nombre = null,
        Qr = null,
        EsValidoDian = false,
        MensajesValidacion = null,
        ReglasValidacionDIAN = new List<string>()
        {
          "Regla: FAD09c, Rechazo: La fecha de emision es anterior en mas de 10 dias de la fecha actual"
        },
        ReglasNotificacionDIAN = null,
        FechaAceptacionDIAN = null
      };

      RespuestaDocumentoNoAutenticado = new()
      {
        Codigo = CodigoNoAutenticado,
        Resultado = ResultadoError
      };

      RespuestaSuscripcionDispositivoNoActiva = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDocumentoNoSuperoLasValidaciones,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        EsValidoDian = false,
        MensajesValidacion = new List<string>()
        {
          ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoActiva
        }
      };

      RespuestaSuscripcionDispositivoNoVigente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDocumentoNoSuperoLasValidaciones,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        EsValidoDian = false,
        MensajesValidacion = new List<string>()
        {
          ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoVigente
        }
      };

      RespuestaSuscripcionDispositivoAunNoVigente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDocumentoNoSuperoLasValidaciones,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        EsValidoDian = false,
        MensajesValidacion = new List<string>()
        {
          ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoAunNoVigente
        }
      };

      RespuestaSuscripcionDispositivoInexistente = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDocumentoNoSuperoLasValidaciones,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        EsValidoDian = false,
        MensajesValidacion = new List<string>()
        {
          ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoInexistente
        }
      };

      RespuestaSuscripcionDispositivoNoAsociadoAEmpresa = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoErrorValidacionSuscripcion,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDocumentoNoSuperoLasValidaciones,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        EsValidoDian = false,
        MensajesValidacion = new List<string>()
        {
          ConstantesCompartidasFacturacion.MensajeSuscripcionDispositivoNoAsociadoAEmpresa
        }
      };

      RespuestasConsultarDocumentos();
      RespuestasConsultarMontoFacturaPos();
    }

    public void RespuestasConsultarDocumentos()
    {
      RespuestaConsultarDocumentosExitoso = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TotalDocumentos = 1,
        Documentos = JToken.Parse(string.Concat(
          "[",
          "{",
          "\"Origen\": \"Integracion\",",
          "\"CodigoTipoDocumento\": \"01\",",
          "\"TipoDocumento\": \"FACTURA DE VENTA\",",
          "\"FechaEmision\": \"2023-02-13 00:00:00\",",
          "\"NumeroDocumento\": \"ARMXIFNDEV1002\",",
          "\"Cliente\": \"The Factory HKA Colombia\",",
          "\"NumeroIdentificacionCliente\": \"901041710\",",
          "\"NumeroIdentificacionClienteDv\": \"5\",",
          "\"Estatus\": \"200\",",
          "\"Mensaje\": \"Procesado Correctamente\",",
          "\"EstatusDianCodigo\": \"0\",",
          "\"EstatusDianMensaje\": \"Procesado Correctamente\",",
          "\"EstatusValidacion\": \"\",",
          "\"FechaDian\": \"2023-02-13 11:52:55\",",
          "\"EstatusNotificaciones\": \"\",",
          "\"FechaValidacion\": \"2023-02-13 21:52:55\",",
          "\"TotalBrutoAntesImpuestos\": \"1003.000000\",",
          "\"BaseImponible\": \"1003.000000\",",
          "\"Impuesto\": \"190.570000\",",
          "\"Monto\": \"1193.570000\",",
          "\"Cufe\": \"e1117a0b4472b859fdffe7145af47752af91179a486f6440e2ca6a73dd3c4989866b8492b2f28ca0d9ee2b11f50e0573\",",
          "\"CantidadArticulos\": \"1\",",
          "\"CodigoSucursal\": \"ARC-01\",",
          "\"Correo\": \"\",",
          "\"EstadoEntrega\": \"\",",
          "\"MensajeEstadoEntrega\": \"\",",
          "\"EstatusComentario\": \"PENDIENTE\",",
          "\"Comentario\": \"\"",
          "}",
          "]"
          ))
      };

      RespuestaConsultarDocumentosSistemaNoDisponible = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible,
        Mensaje = "Sistema no disponible",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TotalDocumentos = 0,
      };

      RespuestaConsultarDocumentosFormatoRespuestaNoDisponible = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible,
        Resultado = ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible,
        Mensaje = "Formato de respuesta no disponible",
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TotalDocumentos = 0,
      };

      RespuestaConsultarDocumentosNoAutenticado = new()
      {
        Codigo = CodigoNoAutenticado,
        Resultado = ResultadoError
      };

      RespuestaConsultarDocumentosFormatoRespuestaErrorValidaciones = new()
      {
        Codigo = CodigoNoAutenticado,
        Resultado = ResultadoError,
        Mensaje = MensajeErrorValidaciones,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TotalDocumentos = 0,
        Errores = new List<string>()
      };

      RespuestaConsultarDocumentosSinDocumentos = new()
      {
        Codigo = CodigoSinDocumentos,
        Resultado = ResultadoSinDocumentos,
        Mensaje = MensajeSinDocumentos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TotalDocumentos = 0
      };
    }

    public void RespuestasConsultarMontoFacturaPos()
    {
      #region Respuestas Dto
      RespuestaConsultarMontoFacturaPosExitosaDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        MontoUVT = "43212",
        CantidadUVT = "5",
        MontoFacturaPos = "212060",
        FechaVigencia = "2023-12-31 23:59:59",
      };

      RespuestaConsultarMontoFacturaPosNoAutenticadoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      };
      #endregion

      #region Respuestas
      RespuestaConsultarMontoFacturaPosExitosa = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        MontoUVT = "43212",
        CantidadUVT = "5",
        MontoFacturaPos = "212060",
        FechaVigencia = "2023-12-31 23:59:59",
      };

      RespuestaConsultarMontoFacturaPosNoAutenticado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      };
      #endregion
    }

    public RespuestasCompartidasDocumentos()
    {
      InicializarRespuestas();
    }
  }
}
