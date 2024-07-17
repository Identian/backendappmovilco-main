using Dominio.Entidad.Reportes;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasReportes
  {
    public RespuestaReporteEnLinea RespuestaReporteEnLineaExitosa { get; set; }
    public RespuestaReporteEnLinea RespuestaReporteEnLineaNoAutenticado { get; set; }
    public RespuestaReporteEnLinea RespuestaReporteEnLineaErrorValidaciones { get; set; }
    public RespuestaReporteEnLinea RespuestaReporteEnLineaSinDocumentos { get; set; }

    public static string SistemaFacturacion = "1";
    public static string CodigoReporteGeneralSistemaFacturacion = "9";

    public const string ResultadoError = "Error";
    public const int CodigoNoAutenticado = 401;

    public const int CodigoSinDocumentos = 201;
    public const string MensajeSinDocumentos = "No se consiguieron registros con los filtros aplicados";
    public const string ResultadoSinDocumentos = "Procesado.";

    public const string FechaInicioSinDocumentos = "2023-02-01 00:00:00";
    public const string FechaHastaSinDocumentos = "2023-02-01 23:59:59";

    public void InicializarRespuestas()
    {
      RespuestaReporteEnLineaExitosa = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Mensaje = "El Reporte En Línea se generó exitosamente",
        Resultado = "Procesado",
        Siguiente = "0",
        TrackId = "295",
        Formato = "json",
        EstadoLegal = "0",
        ReporteCodificado = "eyJFbiBMw61uZWEiOlt7Ik9yaWdlbiI6IkludGVncmFjaW9uIiwiQ29kaWdvVGlwb0RvY3VtZW50byI6IjAxIiwiVGlwb0RvY3VtZW50byI6IkZBQ1RVUkEgREUgVkVOVEEiLCJGZWNoYUVtaXNpb24iOiIyMDIyLTEwLTAzIDAxOjAyOjAzIiwiTnVtZXJvRG9jdW1lbnRvIjoiQVJNWElGTkRFVjEiLCJDbGllbnRlIjoiVGhlIEZhY3RvcnkgSEtBIENvbG9tYmlhIiwiTnVtZXJvSWRlbnRpZmljYWNpb25DbGllbnRlIjoiOTAxMDQxNzEwIiwiTnVtZXJvSWRlbnRpZmljYWNpb25DbGllbnRlRHYiOiI1IiwiRXN0YXR1cyI6IjIwMCIsIk1lbnNhamUiOiJQcm9jZXNhZG8gQ29ycmVjdGFtZW50ZSIsIkVzdGF0dXNEaWFuQ29kaWdvIjoiMCIsIkVzdGF0dXNEaWFuTWVuc2FqZSI6IlByb2Nlc2FkbyBDb3JyZWN0YW1lbnRlIiwiRXN0YXR1c1ZhbGlkYWNpb24iOiIiLCJGZWNoYURpYW4iOiIyMDIyLTEwLTI2IDExOjUxOjMyIiwiRXN0YXR1c05vdGlmaWNhY2lvbmVzIjoiIiwiRmVjaGFWYWxpZGFjaW9uIjoiMjAyMi0xMC0yNiAyMTo1MTozMiIsIlRvdGFsQnJ1dG9BbnRlc0ltcHVlc3RvcyI6IjEwMDMuMDAwMDAwIiwiQmFzZUltcG9uaWJsZSI6IjEwMDMuMDAwMDAwIiwiSW1wdWVzdG8iOiIxOTAuNTcwMDAwIiwiTW9udG8iOiIxMTkzLjU3MDAwMCIsIkN1ZmUiOiJlZDdjNzFjOTgxMWU2MmI5ZjBlYTNiMGI1ZDE3ZjAyOWUzNTgwZDAxYTIwMWM5NDNkZWIyMjc5NzM1ZjY2NTgwYzQ5YmExZjk2MzQ2NDIxMzRjMTViMTc2YjFhYzYyZjIiLCJDYW50aWRhZEFydGljdWxvcyI6IjEiLCJDb2RpZ29TdWN1cnNhbCI6IkFSQy0wMSIsIkNvcnJlbyI6ImFkcmFtb3NAdGhlZmFjdG9yeWhrYS5jb20iLCJFc3RhZG9FbnRyZWdhIjoiU2VuZCIsIk1lbnNhamVFc3RhZG9FbnRyZWdhIjoiU2VuZCIsIkVzdGF0dXNDb21lbnRhcmlvIjoiUEVORElFTlRFIiwiQ29tZW50YXJpbyI6IiJ9XX0=",
        Crc = "d49969e5e96576d84cd949547ab2a79a1a1722f866398efed",
        Reporte = null,
        Errores = null
      };

      RespuestaReporteEnLineaNoAutenticado = new()
      {
        Codigo = CodigoNoAutenticado,
        Resultado = ResultadoError
      };

      RespuestaReporteEnLineaErrorValidaciones = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosInvalidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosInvalidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosInvalidos,
        Errores = new List<string>()
      };

      RespuestaReporteEnLineaSinDocumentos = new()
      {
        Codigo = CodigoSinDocumentos,
        Resultado = ResultadoSinDocumentos,
        Mensaje = MensajeSinDocumentos
      };
    }
  }
}
