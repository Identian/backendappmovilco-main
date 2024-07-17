using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;

namespace Dominio.Core
{
  public class EmpresaAutenticacionDominio : IEmpresaAutenticacionDominio
  {
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public EmpresaAutenticacionDominio(IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IRedisCacheRepositorio redisCacheRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }
    public EmpresaAutenticacion ObtenerDatosToken(string bearerToken)
    {
      return _empresaAutenticacionRepositorio.ObtenerDatosToken(bearerToken);

    }
    public RespuestaValidarClaveSecreta ValidarClaveSecreta(SolicitudValidarClaveSecreta SolicitudclaveSecreta, string bearerToken, string valorBearerToken)
    {
      var respuesta = new RespuestaValidarClaveSecreta();
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresaporId = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        if (datosEmpresaporId.Codigo == 200)
        {
          var datosContribuyente = datosEmpresaporId.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresaporId.Codigo;
            usuarioEnCache.Contribuyente = JToken.FromObject(new
            {
              IdEmpresa = datosContribuyente.IdEmpresa,
              NitEmpresa = datosContribuyente.NumeroIdentificacion,
              TokenEmpresa = datosContribuyente.TokenEmpresa,
              TokenClave = datosContribuyente.TokenClave
            });
          }
          else
          {
            respuesta.Codigo = 401;
            respuesta.Resultado = "Error";
            respuesta.Mensaje = "Datos Token inválidos";
            return respuesta;

          }

        }
      }
      var datosEmpresa = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200) && (datosEmpresa != null))
      {
        respuesta = _empresaAutenticacionRepositorio.validarClaveSecreta(SolicitudclaveSecreta.ClaveSecreta, datosEmpresa.IdEmpresa);
      }
      else
      {
        respuesta.Codigo = 401;
        respuesta.Resultado = "Error";
        respuesta.Mensaje = "Se ha cerrado la sesión del usuario";
      }
      return respuesta;
    }
  }
}
