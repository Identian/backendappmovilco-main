namespace Dominio.Entidad.Documentos
{
  public class ImpuestosTotales
  {
    public string? CodigoTOTALImp { get; set; }
    public string? MontoTotal { get; set; }
    public string? RedondeoAplicado { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
