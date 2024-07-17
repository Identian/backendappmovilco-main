using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aplicacion.Principal
{
  public class OcultarApisSwagger : IDocumentFilter
  {
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
      foreach (KeyValuePair<string, OpenApiPathItem> path in swaggerDoc.Paths.ToList())
      {
        foreach (KeyValuePair<OperationType, OpenApiOperation> method in path.Value.Operations.ToList())
        {
          swaggerDoc.Paths[path.Key].Operations.Remove(method.Key);
        }
      }
    }
  }
}
