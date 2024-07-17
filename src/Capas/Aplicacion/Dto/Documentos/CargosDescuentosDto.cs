namespace Aplicacion.Entidad.Documentos
{
  public class CargosDescuentosDto
  {
    public string? Secuencia { get; set; }
    public string? Indicador { get; set; }
    public string? Codigo { get; set; }
    public string? Descripcion { get; set; }
    public string? Porcentaje { get; set; }
    public string? Monto { get; set; }
    public string? MontoBase { get; set; }
    public IEnumerable<ExtensibleReferenciaDto>? Extras { get; set; }
  }
}
