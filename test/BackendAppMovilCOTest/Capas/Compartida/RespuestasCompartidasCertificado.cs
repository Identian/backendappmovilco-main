using Dominio.Entidad;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasCertificado
  {
    public RespuestaConsultarCertificadoFacturacion DatosExistentesConsultarCertificado { get; set; }
    public RespuestaConsultarCertificadoFacturacion DatosNitNoExisteConsultarCertificado { get; set; }
    public RespuestaConsultarCertificadoFacturacion DatosTokensInvalidosConsultarCertificado { get; set; }
    public RespuestaConsultarCertificadoFacturacion DatosInvalidosConsultarCertificado { get; set; }


    private void InicializarRespuestas()
    {
      DatosExistentesConsultarCertificado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Certificado = new CertificadoFacturacion()
        {
          Serie = "1",
          ValidoDesde = "2021-07-23 20:24:00",
          ValidoHasta = "2021-07-23 20:24:00",
          RazonSocial = "THE FACTORY",
          Proveedor = "Bogota D.C."
        },
        Errores = null
      };

      DatosNitNoExisteConsultarCertificado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNitNoExiste,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNitNoExiste,
        Mensaje = ConstantesCompartidasFacturacion.MensajeNitNoExiste,
        Nit = null,
        IdEmpresa = null,
        Certificado = null,
        Errores = null
      };

      DatosTokensInvalidosConsultarCertificado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoTokensInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoTokensInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeTokensInvalidos,
        Nit = null,
        IdEmpresa = null,
        Certificado = null,
        Errores = null
      };


      DatosInvalidosConsultarCertificado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosInvalidos,
        Nit = null,
        IdEmpresa = null,
        Certificado = null,
        Errores = null
      };
    }

    public RespuestasCompartidasCertificado()
    {
      InicializarRespuestas();
    }
  }
}
