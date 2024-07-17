namespace Aplicacion.Entidad.Documentos
{
  public class SectorSaludDto
  {
    public string? TipoEscenario { get; set; }
    public string? IdPersonalizacion { get; set; }
    public BeneficiarioSaludDto? Beneficiario { get; set; }
    public IEnumerable<DatosPacienteSaludDto>? Pacientes { get; set; }
    public IEnumerable<ExtrasDto>? Extras { get; set; }
  }
}
