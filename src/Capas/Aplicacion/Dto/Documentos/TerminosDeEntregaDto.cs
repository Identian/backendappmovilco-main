namespace Aplicacion.Entidad.Documentos
{
  public class TerminosDeEntregaDto
  {
    public string? Identificacion { get; set; }
    public string? CostoTransporte { get; set; }
    public string? CodigoCondicionEntrega { get; set; }
    public string? ResponsableEntrega { get; set; }
    public DireccionBaseReferenciaDto? DireccionEntrega { get; set; }
    public IEnumerable<CargosDescuentosDto>? CargosDescuentos { get; set; }
    public string? Monto { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
