using Aplicacion.Dto.Empresas;
using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;

namespace Aplicacion.Principal
{
  public class EmpresaAutenticacionAplicacion : IEmpresaAutenticacionAplicacion
  {
    private readonly IEmpresaAutenticacionDominio _empresaAutenticacionDominio;
    private readonly IMapper _mapeador;


    public EmpresaAutenticacionAplicacion(IEmpresaAutenticacionDominio empresaAutenticacionDominio, IMapper mapeador)
    {
      _empresaAutenticacionDominio = empresaAutenticacionDominio;
      _mapeador = mapeador;
    }

    public EmpresaAutenticacionDto ObtenerDatosToken(string bearerToken)
    {
      EmpresaAutenticacionDto respuestaDto = new();

      try
      {
        var respuesta = _empresaAutenticacionDominio.ObtenerDatosToken(bearerToken);
        respuestaDto = _mapeador.Map<EmpresaAutenticacionDto>(respuesta);
      }
      catch (Exception ex)
      {
        respuestaDto.Mensaje = ex.Message;
      }
      return respuestaDto;
    }

    public RespuestaValidarClaveSecretaDto validarClaveSecreta(SolicitudValidarClaveSecretaDto solicitudClaveSecreta, string bearerToken, string valorBearerToken)
    {
      var respuestaDto = new RespuestaValidarClaveSecretaDto();
      var validaciones = new SolicitudValidarClaveSecretaDtoValidate();
      var resultadoValidaciones = validaciones.Validate(solicitudClaveSecreta);
      if (resultadoValidaciones.IsValid)
      {
        try
        {
          var solicitudDto = _mapeador.Map<SolicitudValidarClaveSecreta>(solicitudClaveSecreta);
          var respuesta = _empresaAutenticacionDominio.ValidarClaveSecreta(solicitudDto, bearerToken, valorBearerToken);
          respuestaDto = _mapeador.Map<RespuestaValidarClaveSecretaDto>(respuesta);
        }
        catch (Exception ex)
        {
          respuestaDto.Codigo = 500;
          respuestaDto.Mensaje = ex.Message;
          respuestaDto.Resultado = "Error";
        }
      }
      else
      {

        respuestaDto.Codigo = 400;
        respuestaDto.Resultado = "Error";
        respuestaDto.Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones";
        var errores = new List<string>();
        foreach (var error in resultadoValidaciones.Errors)
        {
          errores.Add(error.ErrorMessage);
        }
        respuestaDto.Errores = errores;
      }
      return respuestaDto;
    }

  }
}

