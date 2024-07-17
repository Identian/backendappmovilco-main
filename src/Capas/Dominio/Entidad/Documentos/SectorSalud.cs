namespace Dominio.Entidad.Documentos
{
  public class SectorSalud
  {
    public string? TipoEscenario { get; set; }
    public string? IdPersonalizacion { get; set; }
    public BeneficiarioSalud? Beneficiario { get; set; }
    public IEnumerable<DatosPacienteSalud>? Pacientes { get; set; }
    public IEnumerable<Extras>? Extras { get; set; }
  }
}
