using Aplicacion.Dto.Respuestas;
using Dominio.Entidad;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasUsuarioFacturacion
  {
    public RespuestaConsultarUsuarioFacturacion DatosExistentesConsultarUsuario { get; set; }
    public RespuestaConsultarUsuarioFacturacionDto DatosExistentesConsultarUsuarioDto { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosExistentesConsultarUsuarioRolEstandar { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosNoEncontradosConsultarUsuario { get; set; }
    public RespuestaConsultarUsuarioFacturacionDto DatosNoEncontradosConsultarUsuarioDto { get; set; }
    public RespuestaConsultarUsuarioFacturacion SeHaCerradoSesionConsultarUsuario { get; set; }
    public RespuestaConsultarUsuarioFacturacionDto SeHaCerradoSesionConsultarUsuarioDto { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosExistentesConsultarUsuarioActivoAppEmpresaInvalido { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosExistentesConsultarUsuarioActivoAppInvalido { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosExistentesConsultarUsuarioErrorRedisCache { get; set; }
    public RespuestaConsultarUsuarioFacturacion DatosExistentesUsuarioNoEncontradoGestionAppInvalido { get; set; }
    public RespuestaConsultarRollUsuario DatosExistentesConsultarRollUsuario { get; set; }
    public RespuestaConsultarRollUsuario DatosExistentesConsultarRolUsuarioRolEstandar { get; set; }



    public const int CodigoUsuarioNoExiste = 404;
    public const string ResultadoError = "Error";
    public const string MensajeUsuarioNoExiste = "No se encontraron datos para el usuario";
    public const int CodigoSessionCerrada = 401;
    public const string MensajeSesionCerrada = "Se ha cerrado la sesión del usuario";

    public RespuestasCompartidasUsuarioFacturacion()
    {
      InicializarRespuestas();
    }

    private void InicializarRespuestas()
    {
      DatosExistentesConsultarUsuario = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuario,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioExistente,
          Nombre = "Nombre Usuario",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };

      DatosExistentesConsultarUsuarioDto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuario,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioExistente,
          Nombre = "Nombre Usuario",
          Apellido = null,
          NombreUsuario = null,
          Activo = "1"
        },
        Errores = null
      };

      DatosExistentesConsultarUsuarioRolEstandar = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioRolEstandar,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };

      DatosNoEncontradosConsultarUsuario = new()
      {
        Codigo = CodigoUsuarioNoExiste,
        Resultado = ResultadoError,
        Mensaje = MensajeUsuarioNoExiste,
        Nit = null,
        IdEmpresa = null,
        Usuario = null,
        Errores = null
      };

      DatosNoEncontradosConsultarUsuarioDto = new()
      {
        Codigo = CodigoUsuarioNoExiste,
        Resultado = ResultadoError,
        Mensaje = MensajeUsuarioNoExiste,
        Nit = null,
        IdEmpresa = null,
        Usuario = null,
        Errores = null
      };

      SeHaCerradoSesionConsultarUsuario = new()
      {
        Codigo = CodigoSessionCerrada,
        Resultado = ResultadoError,
        Mensaje = MensajeSesionCerrada,
        Nit = null,
        IdEmpresa = null,
        Usuario = null,
        Errores = null
      };

      SeHaCerradoSesionConsultarUsuarioDto = new()
      {
        Codigo = CodigoSessionCerrada,
        Resultado = ResultadoError,
        Mensaje = MensajeSesionCerrada,
        Nit = null,
        IdEmpresa = null,
        Usuario = null,
        Errores = null
      };

      DatosExistentesConsultarRollUsuario = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        IdUsuario = ConstantesCompartidasFacturacion.IdUsuario,
        rollUsuario = new List<RollUsuarioFacturacion>
        {
          new RollUsuarioFacturacion
          {
            Codigo = "23",
            Tipo = "user-advanced",
            Descripcion = "Acceso para agregar y editar registros, ajustes y configuraciones."
          }
        },
        Errores = null
      };

      DatosExistentesConsultarRolUsuarioRolEstandar = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        IdUsuario = ConstantesCompartidasFacturacion.IdUsuarioRolEstandar,
        rollUsuario = new List<RollUsuarioFacturacion>
        {
          new RollUsuarioFacturacion
          {
            Codigo = "22",
            Tipo = "user-standar",
            Descripcion = "Acceso básicos con opciones limitadas para agregar y visualizar sin afectar configuraciones."
          }
        },
        Errores = null
      };

      DatosExistentesConsultarUsuarioErrorRedisCache = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioAutenticadoConErrores,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };

      DatosExistentesConsultarUsuarioActivoAppEmpresaInvalido = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioInvalid0GestionAccesoApp,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };

      DatosExistentesConsultarUsuarioActivoAppInvalido = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioAppInactivo,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };
      DatosExistentesUsuarioNoEncontradoGestionAppInvalido = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Nit = ConstantesCompartidasFacturacion.NitExistente,
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        Usuario = new()
        {
          Id = ConstantesCompartidasFacturacion.IdUsuarioInvalid0GestionAccesoApp,
          Correo = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
          Nombre = "Usuario Estandar",
          Apellido = null,
          NombreUsuario = null,
          Administrador = null,
          Roles = null,
          Activo = 1
        },
        Errores = null
      };

    }
  }
}

