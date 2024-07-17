using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Dominio.Core
{
  public class EmpresaDominio : IEmpresaDominio
  {
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public EmpresaDominio(IRedisCacheRepositorio redisCacheRepositorio, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
      _redisCacheRepositorio = redisCacheRepositorio;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _empresaRepositorio = empresaRepositorio;
    }

    public JToken ConsultarEmpresa(SolicitudConsultarFacturacion solicitud, string bearerToken, string valorBearerToken)
    {
      JToken respuesta;
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken
      };
      var usuarioEnCache = _redisCacheRepositorio.ConsultarUsuarioAutenticado(inicioSesion);
      if (usuarioEnCache.Codigo == 504)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var datosEmpresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));

        if (datosEmpresa.Codigo == 200)
        {
          var datosContribuyente = datosEmpresa.Contribuyente!;
          if (datosContribuyente.IdEmpresa == datosToken.IdEmpresa! && datosContribuyente.NumeroIdentificacion == datosToken.NitEmpresa && datosContribuyente.TokenEmpresa == datosToken.TokenEmpresa)
          {
            usuarioEnCache.Codigo = datosEmpresa.Codigo;
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
            return JToken.FromObject(RespuestaBase.Error401("Dominio", "Datos Token inválidos"));
          }

        }
      }
      if ((usuarioEnCache != null) && (usuarioEnCache.Codigo == 200))
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);
        var empresa = _empresaRepositorio.ConsultarEmpresaPorId(Convert.ToInt32(datosToken.IdEmpresa));
        solicitud.Nit = empresa.Contribuyente!.NumeroIdentificacion;
        solicitud.TokenEmpresa = empresa.Contribuyente!.TokenEmpresa;
        solicitud.TokenClave = empresa.Contribuyente!.TokenClave;
        respuesta = _empresaRepositorio.ConsultarEmpresa(solicitud);
      }
      else
      {
        RespuestaBase respuestaConError = new()
        {
          Codigo = 401,
          Resultado = "Error",
          Mensaje = "Se ha cerrado la sesión del usuario"
        };
        respuesta = JToken.FromObject(respuestaConError);
      }
      return respuesta;
    }
  }
}
