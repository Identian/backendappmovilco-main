using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;

namespace Dominio.Core
{
  public class CertificadoDominio : ICertificadoDominio
  {
    private readonly ICertificadoRepositorio _certificadoRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public CertificadoDominio(ICertificadoRepositorio certificadoRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
      _certificadoRepositorio = certificadoRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }

    public RespuestaConsultarCertificadoFacturacion Consultar(SolicitudConsultarFacturacion solicitud)
    {
      var consultaEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(solicitud.IdEmpresa));
      if (consultaEmpresa != null && consultaEmpresa.Contribuyente != null)
      {
        solicitud.TokenClave = consultaEmpresa.Contribuyente.TokenClave;
      }
      return _certificadoRepositorio.Consultar(solicitud);
    }
  }
}
