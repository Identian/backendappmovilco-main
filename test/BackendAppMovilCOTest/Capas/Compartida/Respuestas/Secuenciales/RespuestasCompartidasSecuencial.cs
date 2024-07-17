using Aplicacion.Dto.Respuestas;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida.Respuestas.Secuenciales
{
  public class RespuestasCompartidasSecuencial
  {

    public RespuestaSeleccionarSecuencialDto? DatosExistentesSeleccionarSecuencialExisosoDto { get; set; }
    public RespuestaSeleccionarSecuencialDto? DatosSecuencialNoAsociadoDto { get; set; }
    public RespuestaSeleccionarSecuencialDto? DatosSecuencialYaSeleccionadoDto { get; set; }
    public RespuestaSeleccionarSecuencialDto? RespuestaEmpresaNoAutenticadoDto { get; set; }
    public RespuestaSeleccionarSecuencial? DatosExistentesSeleccionarSecuencialExisoso { get; set; }
    public RespuestaSeleccionarSecuencial? DatosSecuencialNoAsociado { get; set; }
    public RespuestaSeleccionarSecuencial? DatosSecuencialYaSeleccionado { get; set; }
    public RespuestaSeleccionarSecuencial? RespuestaEmpresaNoAutenticado { get; set; }

    private void InicializarRespuestas()
    {
      DatosExistentesSeleccionarSecuencialExisoso = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido,
        Errores = null
      };

      DatosSecuencialNoAsociado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Numeracion no asociado a esta empresa",
        Nit = null,
        IdEmpresa = null,
        IdNumeracion = null,
        Errores = null
      };

      DatosSecuencialYaSeleccionado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Numeracion ya se encuentra seleccionada",
        Nit = null,
        IdEmpresa = null,
        IdNumeracion = null,
        Errores = null
      };

      RespuestaEmpresaNoAutenticado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      };

    }

    private void InicializarRespuestasDto()
    {
      DatosExistentesSeleccionarSecuencialExisosoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido,
        Errores = null
      };

      DatosSecuencialNoAsociadoDto = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Numeracion no asociado a esta empresa",
        Nit = null,
        IdEmpresa = null,
        IdNumeracion = null,
        Errores = null
      };

      DatosSecuencialYaSeleccionadoDto = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Numeracion ya se encuentra seleccionada",
        Nit = null,
        IdEmpresa = null,
        IdNumeracion = null,
        Errores = null
      };
      RespuestaEmpresaNoAutenticadoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      };
    }

    public RespuestasCompartidasSecuencial()
    {
      InicializarRespuestas();
      InicializarRespuestasDto();
    }
  }
}
