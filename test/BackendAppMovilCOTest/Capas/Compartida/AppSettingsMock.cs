using Microsoft.Extensions.Configuration;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class AppSettingsMock
  {
    private readonly IConfiguration _configuracion;

    public IConfiguration Object
    {
      get { return (_configuracion); }
    }

    public AppSettingsMock()
    {
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:IndicadoresRest:ConsultarTotalDocumentos:ValoresPermitidosFiltroTipo:0", "Cantidad" },
        { "ServiciosFacturacion:IndicadoresRest:ConsultarTotalDocumentos:ValoresPermitidosFiltroTipo:1", "Monto" }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();
    }
  }
}
