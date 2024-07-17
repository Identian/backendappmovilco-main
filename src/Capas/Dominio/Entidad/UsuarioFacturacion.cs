namespace Dominio.Entidad
{
  public class UsuarioFacturacion
  {
    public string? Id { get; set; }
    public string? Correo { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Administrador { get; set; }
    public List<RollUsuarioFacturacion>? Roles { get; set; }
    public byte Activo { get; set; }
  }
}
