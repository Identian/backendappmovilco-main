using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Dominio.Core
{
  public class DispositivosDominio : IDispositivosDominio
  {
    private readonly IAutenticacionGestionAccesoAppMovil _autenticacionGestionAccesoAppMovil;
    private readonly IGestionAccesoAppMovil _gestionAccesoAppMovil;
    private readonly IEmpresaAutenticacionRepositorio _empresaAutenticacionRepositorio;
    private readonly IRedisCacheRepositorio _redisCacheRepositorio;
    private readonly IDispositivosAppMovilRepositorioSql _dispositivosAppMovilRepositorioSql;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public DispositivosDominio(IAutenticacionGestionAccesoAppMovil autenticacionGestionAccesoAppMovil, IGestionAccesoAppMovil gestionAccesoAppMovil, IEmpresaAutenticacionRepositorio empresaAutenticacionRepositorio, IRedisCacheRepositorio redisCacheRepositorio, IDispositivosAppMovilRepositorioSql dispositivosAppMovilRepositorioSql, IEmpresaRepositorio empresaRepositorio)
    {
      _autenticacionGestionAccesoAppMovil = autenticacionGestionAccesoAppMovil;
      _gestionAccesoAppMovil = gestionAccesoAppMovil;
      _empresaAutenticacionRepositorio = empresaAutenticacionRepositorio;
      _redisCacheRepositorio = redisCacheRepositorio;
      _dispositivosAppMovilRepositorioSql = dispositivosAppMovilRepositorioSql;
      _empresaRepositorio = empresaRepositorio;
    }

    public JToken? CrearAccesoDispositivo(Dispositivo solicitud, string bearerToken, string valorBearerToken)
    {
      JToken? respuesta;
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken.Replace("Bearer ", "")
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
      if (usuarioEnCache.Codigo == 200)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);

        //insertamos el idempresa en la sulicitud
        solicitud.IdEmpresa = datosToken.IdEmpresa;

        //autenticamos al usuario que va a realizar la creacion del dispositivo en GestionAccesoAppMovil
        var atenticarGestionAccesoAppMovil = _autenticacionGestionAccesoAppMovil.AutenticarUsuarioGestionAppMovil()!;

        // si el usuario es invalido en atenticarGestionAccesoAppMovil o no inicia sesion devolvemos la respuesta
        if (atenticarGestionAccesoAppMovil.Codigo != 200)
        {
          atenticarGestionAccesoAppMovil["Mensaje"] = "No se pudo iniciar sesion en autenticarGestionAccesoAppMovil";
          respuesta = JToken.FromObject(atenticarGestionAccesoAppMovil);
          return respuesta; // devuelve la respuesta en caso de error
        }

        string tokenUsuarioGestionAppMovil = atenticarGestionAccesoAppMovil.Token;

        //llamamos a la api para crear el dispositivo
        if (solicitud.TipoApp == "2")
        {
          respuesta = _dispositivosAppMovilRepositorioSql.ConsultarNombreYSerialDispositivo(solicitud, tokenUsuarioGestionAppMovil);
          return respuesta;
        }
        else
        {
          respuesta = _gestionAccesoAppMovil.CrearAccesoDispositivo(solicitud, tokenUsuarioGestionAppMovil);
        }
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error401("Dominio", "Se ha cerrado la sesión del usuario"));
      }
      return (respuesta);
    }

    public JToken? ConsultarSuscripcionDispositivo(string serialLogico, string bearerToken, string valorBearerToken)
    {
      JToken? respuesta;
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken.Replace("Bearer ", "")
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
      if (usuarioEnCache.Codigo == 200)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);

        var autenticarGestionAccesoAppMovil = _autenticacionGestionAccesoAppMovil.AutenticarUsuarioGestionAppMovil()!;
        if (autenticarGestionAccesoAppMovil.Codigo != 200)
        {
          autenticarGestionAccesoAppMovil["Mensaje"] = "No se pudo iniciar sesión en GestionAccesoAppMovil";
          respuesta = JToken.FromObject(autenticarGestionAccesoAppMovil);
          return respuesta;
        }

        var suscripcionDispositivo = _dispositivosAppMovilRepositorioSql.ConsultarSuscripcionDispositivoPorSerialLogico(datosToken.IdEmpresa!, serialLogico);
        if (suscripcionDispositivo!["Codigo"]!.ToString() != "200")
        {
          respuesta = JToken.FromObject(new
          {
            Codigo = suscripcionDispositivo["Codigo"],
            Resultado = suscripcionDispositivo["Resultado"],
            Mensaje = suscripcionDispositivo["Mensaje"],
            Nit = (string?)null,
            IdEmpresa = (string?)null,
            Suscripciones = (string?)null,
            Errores = (string?)null
          });
          return respuesta;
        }
        respuesta = _gestionAccesoAppMovil.ConsultarSuscripcionDispositivo(datosToken.IdEmpresa!, suscripcionDispositivo["IdSuscripcion"]!.ToString(), (string)autenticarGestionAccesoAppMovil.Token);
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error401("Dominio", "Se ha cerrado la sesión del usuario"));
      }
      return (respuesta);
    }

    public JToken? AsociarAlias(SolicitudAsociarAlias solicitud, string bearerToken, string valorBearerToken)
    {
      JToken? respuesta;
      RespuestaIniciarSesion inicioSesion = new()
      {
        token = bearerToken.Replace("Bearer ", "")
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
      if (usuarioEnCache.Codigo == 200)
      {
        var datosToken = _empresaAutenticacionRepositorio.ObtenerDatosToken(valorBearerToken);

        //insertamos el idempresa en la sulicitud
        solicitud.IdEmpresa = datosToken.IdEmpresa;

        //autenticamos al usuario que va a realizar la creacion del dispositivo en GestionAccesoAppMovil
        var atenticarGestionAccesoAppMovil = _autenticacionGestionAccesoAppMovil.AutenticarUsuarioGestionAppMovil()!;

        // si el usuario es invalido en atenticarGestionAccesoAppMovil o no inicia sesion devolvemos la respuesta
        if (atenticarGestionAccesoAppMovil.Codigo != 200)
        {
          atenticarGestionAccesoAppMovil["Mensaje"] = "No se pudo iniciar sesion en autenticarGestionAccesoAppMovil";
          respuesta = JToken.FromObject(atenticarGestionAccesoAppMovil);
          return respuesta; // devuelve la respuesta en caso de error
        }

        string tokenUsuarioGestionAppMovil = atenticarGestionAccesoAppMovil.Token;

        //llamamos a la api para asociar alias de dispositivo
        respuesta = _gestionAccesoAppMovil.AsociarAlias(solicitud, tokenUsuarioGestionAppMovil);
      }
      else
      {
        respuesta = JToken.FromObject(RespuestaBase.Error401("Dominio", "Se ha cerrado la sesión del usuario"));
      }
      return (respuesta);
    }
  }
}
