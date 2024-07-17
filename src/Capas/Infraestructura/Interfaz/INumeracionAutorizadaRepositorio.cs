using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface INumeracionAutorizadaRepositorio
  {
    public RespuestaConsultarNumeracionAutorizada ConsultarNumeracionAutorizada(string IdNumeracion, string IdEmpresa);
  }
}
