using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;

namespace Infraestructura.Interfaz
{
  public interface IEmpresaAutenticacionRepositorio
  {
    public EmpresaAutenticacion ObtenerDatosToken(string bearerToken);
    public RespuestaConsultarClaveSecreta ConsultarClaveSecretaEmpresaPorId(string idEmpresa);
    public RespuestaValidarClaveSecreta validarClaveSecreta(string claveSecreta, string idEmpresa);
    public RespuestaConsultarClavesEmpresa ConsultarClavesEmpresaPorIdUsuario(string idEmpresa, string idUsuario);
  }
}
