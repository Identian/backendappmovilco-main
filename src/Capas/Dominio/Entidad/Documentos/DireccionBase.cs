namespace Dominio.Entidad.Documentos
{
  public class DireccionBase
  {
    public string? Tipo { get; set; }
    public string? Municipio { get; set; }
    public string? Buzon { get; set; }
    public string? Piso { get; set; }
    public string? Habitacion { get; set; }
    public string? Calle { get; set; }
    public string? CalleAdicional { get; set; }
    public string? Bloque { get; set; }
    public string? NombreEdificio { get; set; }
    public string? NumeroEdificio { get; set; }
    public string? Ubicacion { get; set; }
    public string? DepartamentoOrg { get; set; }
    public string? ALaAtencionDe { get; set; }
    public string? ACuidadoDe { get; set; }
    public string? NumeroParcela { get; set; }
    public string? SubDivision { get; set; }
    public string? Ciudad { get; set; }
    public string? ZonaPostal { get; set; }
    public string? Departamento { get; set; }
    public string? CodigoDepartamento { get; set; }
    public string? Region { get; set; }
    public string? Distrito { get; set; }
    public string? CorreccionHusoHorario { get; set; }
    public string? Direccion { get; set; }
    public string? Pais { get; set; }
    public string? Lenguaje { get; set; }
    public IEnumerable<Coordenadas>? Localizacion { get; set; }
    public IEnumerable<Extensible>? Extras { get; set; }
  }
}
