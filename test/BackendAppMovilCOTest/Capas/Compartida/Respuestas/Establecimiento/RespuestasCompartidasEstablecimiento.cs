using Aplicacion.Dto.Respuestas;
using Dominio.Entidad;
using Dominio.Entidad.Respuestas;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendAppMovilCOTest.Capas.Compartida.Establecimiento
{
  public class RespuestasCompartidasEstablecimiento
  {
    public RespuestaSeleccionarEstablecimientoDto? DatosExistentesSeleccionarEstablecimientoExisosoDto { get; set; }
    public RespuestaSeleccionarEstablecimientoDto? DatosEstablecimientoNoAsociadoDto { get; set; }
    public RespuestaSeleccionarEstablecimientoDto? DatosEstablecimientoYaSeleccionadoDto { get; set; }
    public RespuestaSeleccionarEstablecimiento? DatosExistentesSeleccionarEstablecimientoExisoso { get; set; }
    public RespuestaSeleccionarEstablecimiento? DatosEstablecimientoNoAsociado { get; set; }
    public RespuestaSeleccionarEstablecimiento? DatosEstablecimientoYaSeleccionado { get; set; }

    public RespuestaSeleccionarEstablecimiento? RespuestaConsultarEmpresaNoAutenticado { get; set; } 

    private void InicializarRespuestas()
    {
      DatosExistentesSeleccionarEstablecimientoExisosoDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido,
        Errores = null
      };

      DatosEstablecimientoNoAsociadoDto = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Establecimiento no asociado a esta empresa",
        Nit = null,
        IdEmpresa = null,
        IdEstablecimiento = null,
        Errores = null
      };

      DatosEstablecimientoYaSeleccionadoDto = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Establecimiento ya se encuentra seleccionado",
        Nit = null,
        IdEmpresa = null,
        IdEstablecimiento = null,
        Errores = null
      };


      DatosExistentesSeleccionarEstablecimientoExisoso = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido,
        Errores = null
      };

      DatosEstablecimientoNoAsociado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Establecimiento no asociado a esta empresa",
        Nit = null,
        IdEmpresa = null,
        IdEstablecimiento = null,
        Errores = null
      };

      DatosEstablecimientoYaSeleccionado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Establecimiento ya se encuentra seleccionado",
        Nit = null,
        IdEmpresa = null,
        IdEstablecimiento = null,
        Errores = null
      };

      RespuestaConsultarEmpresaNoAutenticado= new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoSessionCerrada,
        Resultado = ConstantesCompartidasFacturacion.ResultadoError,
        Mensaje = ConstantesCompartidasFacturacion.MensajeSesionCerrada
      };
   
    }

    public RespuestasCompartidasEstablecimiento()
    {
      InicializarRespuestas();
    }
  }
}
