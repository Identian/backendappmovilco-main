using Aplicacion.Interfaz;
using Aplicacion.Principal;
using Dominio.Core;
using Dominio.Interfaz;
using Infraestructura.Datos.Fabricas;
using Infraestructura.Interfaz;
using Infraestructura.Repositorio;
using Infraestructura.Repositorio.GestionDeAcceso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Transversal.Comun.Fabricas;
using Transversal.Mapeo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
  .AddNewtonsoftJson(options =>
  {
    // Use the default property (Pascal) casing
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
  });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo { Title = "Documentación APIs App Móvil Colombia - " + builder.Environment.EnvironmentName, Version = "v1" });
  // Se oculta lista de Apis cuando ambiente es Producción.
  if (builder.Environment.IsProduction())
  {
    options.DocumentFilter<OcultarApisSwagger>();
  }
  options.DocInclusionPredicate((name, api) => true);
  options.TagActionsBy(api => api.GroupName);

  options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
  {
    Description = "Authorization by API Key.",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Name = "Authorization",
    Scheme = JwtBearerDefaults.AuthenticationScheme
  });
  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Authorization"
        }
      },
      new List<string>()
    }
  });

});
builder.Services.AddSwaggerGenNewtonsoftSupport();

//Disable Validation in Request
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});

#region Authentication
//Auth Bearer Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddCookie()
  .AddJwtBearer(JwtBearerOptions =>
  {
    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidateActor = true,
      ValidateAudience = false,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = builder.Configuration["Autenticacion:Token:Issuer"],
      ValidAudience = builder.Configuration["Autenticacion:Token:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Autenticacion:Token:Key"]))
    };
  });
#endregion

#region Inyección de dependencias
builder.Services.AddAutoMapper(typeof(PerfilMapeo));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IFabricaConexionSql, FabricaConexionSqlServer>();
builder.Services.AddSingleton<IFabricaConexionRedis, FabricaConexionRedisCache>();

//Redis Caché
builder.Services.AddScoped<IRedisCacheRepositorio, RedisCacheRepositorio>();

builder.Services.AddScoped<IUsuarioAutenticacionAplicacion, UsuarioAutenticacionAplicacion>();
builder.Services.AddScoped<IUsuarioAutenticacionDominio, UsuarioAutenticacionDominio>();
builder.Services.AddScoped<IUsuarioAutenticacionRepositorio, UsuarioAutenticacionRepositorio>();

builder.Services.AddScoped<ICifradoRepositorio, CifradoRepositorio>();

builder.Services.AddScoped<IDocumentosAplicacion, DocumentosAplicacion>();
builder.Services.AddScoped<IDocumentosDominio, DocumentosDominio>();
builder.Services.AddScoped<IDocumentosRepositorio, DocumentosRepositorio>();
builder.Services.AddScoped<IDocumentosRepositorioSql, DocumentosRepositorioSql>();
builder.Services.AddScoped<IEstadoDocumentoRepositorio, EstadoDocumentosRepositorio>();
builder.Services.AddScoped<IEstadoDocumentoRepositorio, EstadoDocumentosRepositorio>();
builder.Services.AddScoped<IReferenciaDocumentosRepositorio, ReferenciaDocumentosRepositorio>();

builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IEmpresaDominio, EmpresaDominio>();
builder.Services.AddScoped<IEmpresaAplicacion, EmpresaAplicacion>();

builder.Services.AddScoped<ISecuencialesRepositorio, SecuencialesRepositorio>();
builder.Services.AddScoped<ISecuencialesDominio, SecuencialesDominio>();
builder.Services.AddScoped<ISecuencialesAplicacion, SecuencialesAplicacion>();

builder.Services.AddScoped<IEmpresaAutenticacionRepositorio, EmpresaAutenticacionRepositorio>();
builder.Services.AddScoped<IEmpresaAutenticacionDominio, EmpresaAutenticacionDominio>();
builder.Services.AddScoped<IEmpresaAutenticacionAplicacion, EmpresaAutenticacionAplicacion>();

builder.Services.AddScoped<IClientesRepositorioApi, ClientesRepositorioApi>();
builder.Services.AddScoped<IClientesRepositorioSql, ClientesRepositorioSql>();

builder.Services.AddScoped<IProductosRepositorioSql, ProductosRepositorioSql>();

builder.Services.AddScoped<IReportesRepositorio, ReportesRepositorio>();

builder.Services.AddScoped<IFoliosRepositorio, FoliosRepositorio>();
builder.Services.AddScoped<IFoliosDominio, FoliosDominio>();
builder.Services.AddScoped<IFoliosAplicacion, FoliosAplicacion>();
builder.Services.AddScoped<IAutenticacionFoliosRepositorio, AutenticacionFoliosRepositorio>();

builder.Services.AddScoped<IAutenticacionGestionAccesoAppMovil, AutenticacionGestionAccesoAppMovil>();
builder.Services.AddScoped<IGestionAccesoAppMovil, GestionAccesoAppMovil>();

builder.Services.AddScoped<ICatalogosRepositorio, CatalogosRepositorio>();
builder.Services.AddScoped<ICatalogosDominio, CatalogosDominio>();
builder.Services.AddScoped<ICatalogosAplicacion, CatalogosAplicacion>();

builder.Services.AddScoped<ICertificadoRepositorio, CertificadoRepositorio>();
builder.Services.AddScoped<ICertificadoDominio, CertificadoDominio>();
builder.Services.AddScoped<ICertificadoAplicacion, CertificadoAplicacion>();

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioDominio, UsuarioDominio>();
builder.Services.AddScoped<IUsuarioAplicacion, UsuarioAplicacion>();

builder.Services.AddScoped<INumeracionAutorizadaRepositorio, NumeracionAutorizadaRepositorio>();

builder.Services.AddScoped<IDeliveryRepositorio, DeliveryRepositorio>();

builder.Services.AddScoped<IIndicadoresAplicacion, IndicadoresAplicacion>();
builder.Services.AddScoped<IIndicadoresDominio, IndicadoresDominio>();
builder.Services.AddScoped<IIndicadoresRepositorio, IndicadoresRepositorio>();

builder.Services.AddScoped<IDispositivosAplicacion, DispositivosAplicacion>();
builder.Services.AddScoped<IDispositivosDominio, DispositivosDominio>();
builder.Services.AddScoped<IDispositivosAppMovilRepositorioSql, DispositivosAppMovilRepositorioSql>();

builder.Services.AddScoped<IEstablecimientosAplicacion, EstablecimientosAplicacion>();
builder.Services.AddScoped<IEstablecimientosDominio, EstablecimientosDominio>();
builder.Services.AddScoped<IEstablecimientosRepositorio, EstablecimientosRepositorio>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
  options.DefaultModelsExpandDepth(-1);
  options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend App Móvil CO");
  options.RoutePrefix = string.Empty;
  options.DocumentTitle = "Api's App Móvil";
  options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
