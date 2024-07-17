using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Validaciones;
using Aplicacion.Interfaz;
using AutoMapper;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Microsoft.Extensions.Configuration;

namespace Aplicacion.Principal
{
  public class CatalogosAplicacion : ICatalogosAplicacion
  {
    private readonly IConfiguration _configuracion;
    private readonly ICatalogosDominio _catalogosDominio;
    private readonly IMapper _mapeador;

    public CatalogosAplicacion(IConfiguration configuracion, ICatalogosDominio catalogosDominio, IMapper mapeador)
    {
      _configuracion = configuracion;
      _catalogosDominio = catalogosDominio;
      _mapeador = mapeador;
    }

    public RespuestaConsultarCatalogoDto Consultar(SolicitudConsultarCatalogoDto solicitudDto)
    {
      RespuestaConsultarCatalogoDto respuestaDto = new();
      try
      {
        var validaciones = new SolicitudConsultarCatalogoDtoValidator(_configuracion);
        var resultadoValidaciones = validaciones.Validate(solicitudDto);
        if (resultadoValidaciones.IsValid)
        {
          var solicitud = _mapeador.Map<SolicitudConsultarCatalogo>(solicitudDto);
          var respuesta = _catalogosDominio.Consultar(solicitud);
          respuestaDto = _mapeador.Map<RespuestaConsultarCatalogoDto>(respuesta);
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
      }
      catch (Exception ex)
      {
        respuestaDto.Codigo = 500;
        respuestaDto.Mensaje = ex.Message;
        respuestaDto.Resultado = "Error";
      }
      return (respuestaDto);
    }
  }
}
