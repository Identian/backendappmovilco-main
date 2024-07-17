using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Utils;

namespace Dominio.Core
{
  public class CatalogosDominio : ICatalogosDominio
  {
    private readonly IConfiguration _configuracion;
    private readonly ICatalogosRepositorio _catalogosRepositorio;

    public CatalogosDominio(IConfiguration configuracion, ICatalogosRepositorio catalogosRepositorio)
    {
      _configuracion = configuracion;
      _catalogosRepositorio = catalogosRepositorio;
    }

    public RespuestaConsultarCatalogo Consultar(SolicitudConsultarCatalogo solicitud)
    {
      RespuestaConsultarCatalogo respuesta = new();
      List<JToken?> catalogosConsultados = new();

      List<string> catalogosSolicitados = new();

      if (solicitud.Identificador != null)
      {
        catalogosSolicitados.Add(solicitud.Identificador);
      }
      else
      {
        catalogosSolicitados.AddRange(UtilidadesCadenas.ListaCatalogosPermitidos(_configuracion)!);
      }

      foreach (string identificador in catalogosSolicitados)
      {
        catalogosConsultados.Add(_catalogosRepositorio.Consultar(identificador));
      }

      respuesta.Codigo = 200;
      respuesta.Resultado = "Exitoso";
      respuesta.Mensaje = "Consulta Exitosa";
      respuesta.Catalogos = catalogosConsultados;

      return (respuesta);
    }
  }
}
