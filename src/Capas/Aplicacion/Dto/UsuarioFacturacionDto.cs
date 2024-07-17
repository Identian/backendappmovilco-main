using Dominio.Entidad;

namespace Aplicacion.Dto
{
  public class UsuarioFacturacionDto
  {
    public string? Id { get; set; }
    public string? Correo { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Administrador { get; set; }
    public List<RollUsuarioFacturacion>? Roles { get; set; }
    public string? Activo { get; set; }
  }
}
