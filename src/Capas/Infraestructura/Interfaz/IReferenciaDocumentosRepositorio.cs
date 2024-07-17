using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IReferenciaDocumentosRepositorio
  {   
    public RespuestaConsultarReferenciaDocumento ConsultarRefereciaDocumentoFactura(SolicitudConsultarReferenciaDocumentoFacturacion solicitudConsultarDocumento , string bearerToken);
  }
}
