using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida.Respuestas
{
  public class RespuestasCompartidasIndicadores
  {
    public RespuestaBase? DatosExistentesConsultarTotalDocumentosDto;
    public JToken? DatosExistentesJTokenConsultarTotalDocumentosDto;
    public RespuestaBase? DatosVaciosConsultarTotalDocumentosDto;
    public JToken? DatosVaciosJTokenConsultarTotalDocumentosDto;

    public RespuestasCompartidasIndicadores()
    {
      Inicializar();
    }

    public void Inicializar()
    {
      InicializarConsultarTotalDocumentos();
    }

    private void InicializarConsultarTotalDocumentos()
    {
      #region Respuestas Dto
      DatosExistentesConsultarTotalDocumentosDto = new()
      {
        Codigo = 200,
        Resultado = "Exitoso",
        Mensaje = "Consulta Exitosa",
        Errores = null,
      };
      DatosExistentesJTokenConsultarTotalDocumentosDto = JToken.FromObject(DatosExistentesConsultarTotalDocumentosDto);

      DatosVaciosConsultarTotalDocumentosDto = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Datos no encontrados al consultar total documentos",
        Errores = null
      };
      DatosVaciosJTokenConsultarTotalDocumentosDto = JToken.FromObject(DatosVaciosConsultarTotalDocumentosDto);
      #endregion

      #region Respuestas
      #endregion
    }
  }
}
