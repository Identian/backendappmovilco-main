using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IEstadoDocumentoRepositorio
  {   
    public RespuestaConsultarEstadoDocumento ConsultarDocumentoPorConsecutivoFactura(SolicitudConsultarEstadoDocumentoFacturacion solicitudConsultarDocumento , string bearerToken);
  }
}
