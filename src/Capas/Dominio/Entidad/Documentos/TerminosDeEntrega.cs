namespace Dominio.Entidad.Documentos
{
  public class TerminosDeEntrega
  {
    public string? Identificacion { get; set; }
    public string? CostoTransporte { get; set; }
    public string? CodigoCondicionEntrega { get; set; }
    public string? ResponsableEntrega { get; set; }
    public DireccionBase? DireccionEntrega { get; set; }
    public IEnumerable<CargosDescuentos>? CargosDescuentos { get; set; }
    public string? Monto { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
