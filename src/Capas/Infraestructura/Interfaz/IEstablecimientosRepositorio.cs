using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Newtonsoft.Json.Linq;

namespace Infraestructura.Interfaz
{
  public interface IEstablecimientosRepositorio
  {
 
    public RespuestaSeleccionarEstablecimiento Seleccionar(SolicitudSeleccionarEstablecimiento solicitud , EmpresaAutenticacion datostoken);
  }
}
