using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Dominio.Interfaz
{
  public interface IEmpresaAutenticacionDominio
  {
    public EmpresaAutenticacion ObtenerDatosToken(string bearerToken);
    public RespuestaValidarClaveSecreta ValidarClaveSecreta(SolicitudValidarClaveSecreta claveSecreta, string bearerToken, string valorBearerToken);
  }
}
