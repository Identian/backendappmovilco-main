using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Interfaz
{
  public interface IAutenticacionFoliosRepositorio
  {
    public RespuestaUsuarioAutenticacionFolios AutenticarUsuarioFolios();
  }
}
