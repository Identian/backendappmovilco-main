using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;

namespace Infraestructura.Interfaz
{
  public interface IDocumentosRepositorio
  {
    public RespuestaEmitirDocumento EmitirDocumento(SolicitudEmitirDocumento solicitudEmitirDocumento, string bearerToken);   
  }
}
