namespace Aplicacion.Dto.Solicitudes
{
  public class UsuarioAutenticacionDto
  {
    public string? Usuario { get; set; }
    public string? Contrasena { get; set; }
    public string? TipoSesion { get; set; }
    public string? TipoApp {  get; set; }

    public void AsignarValoresPorDefecto()
    {
      TipoSesion ??= "Iniciar";
      TipoApp ??= "1";
    }

  }
}