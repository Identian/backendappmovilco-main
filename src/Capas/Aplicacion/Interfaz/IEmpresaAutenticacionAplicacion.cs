using Aplicacion.Dto.Empresas;
using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;

namespace Aplicacion.Interfaz
{
  public interface IEmpresaAutenticacionAplicacion
  {
    public EmpresaAutenticacionDto ObtenerDatosToken(string bearerToken);
    public RespuestaValidarClaveSecretaDto validarClaveSecreta(SolicitudValidarClaveSecretaDto solicitudClaveSecreta, string bearerToken, string valorBearerToken);
  }
}
