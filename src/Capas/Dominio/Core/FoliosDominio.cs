using Dominio.Entidad.Respuestas;
using Dominio.Interfaz;
using Infraestructura.Interfaz;

namespace Dominio.Core
{
  public class FoliosDominio : IFoliosDominio
  {
    private readonly IAutenticacionFoliosRepositorio _autenticacionFoliosRepositorio;
    private readonly IFoliosRepositorio _foliosRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;

    public FoliosDominio(IFoliosRepositorio foliosRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IAutenticacionFoliosRepositorio autenticacionFoliosRepositorio)
    {
      _foliosRepositorio = foliosRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _autenticacionFoliosRepositorio = autenticacionFoliosRepositorio;
    }
    public RespuestaConsultarResumenFolios ConsultarResumen(string bearerToken)
    {
      RespuestaConsultarResumenFolios respuesta = new();
      var autenticacionFolios = _autenticacionFoliosRepositorio.AutenticarUsuarioFolios();
      if (autenticacionFolios != null && autenticacionFolios.Codigo == 200)
      {
        var respuestaToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(bearerToken);
        return (_foliosRepositorio.ConsultarResumen(respuestaToken.IdEmpresa!, autenticacionFolios.Token!));
      }
      else
      {
        respuesta.Codigo = autenticacionFolios!.Codigo;
        respuesta.Resultado = autenticacionFolios.Resultado;
        respuesta.Mensaje = autenticacionFolios.Mensaje;
        return (respuesta);
      }
    }
  }
}
