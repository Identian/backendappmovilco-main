using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Respuestas;

namespace Aplicacion.Principal
{
  public class EmpresaAplicacion : IEmpresaAplicacion
  {
    public readonly IEmpresaDominio _empresaDominio;
    public readonly IMapper _mapeador;

    public EmpresaAplicacion(IEmpresaDominio empresaDominio, IMapper mapeador)
    {
      _empresaDominio = empresaDominio;
      _mapeador = mapeador;
    }

    public JToken ConsultarEmpresa(SolicitudConsultarFacturacionDto solicitudDto, string bearerToken, string valorBearerToken)
    {
      JToken respuestaDto;
      try
      {
        var validaciones = new SolicitudConsultarFacturacionDtoValidator();
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudConsultarFacturacion>(solicitudDto);
          respuestaDto = _empresaDominio.ConsultarEmpresa(solicitud, bearerToken, valorBearerToken);
        }
        else
        {
          var errores = new List<string>();
          foreach (var error in resultadoValidaciones.Errors)
          {
            errores.Add(error.ErrorMessage);
          }
          RespuestaBase respuestaDtoConErrores = new()
          {
            Codigo = 400,
            Resultado = "Error",
            Mensaje = "Los datos presentes en la solicitud no han pasado las validaciones",
            Errores = errores
          };
          respuestaDto = JToken.FromObject(respuestaDtoConErrores);
        }
      }
      catch (Exception ex)
      {
        respuestaDto = JToken.FromObject(RespuestaBase.Error500("Aplicación", ex.Message));
      }
      return respuestaDto;
    }
  }
}
