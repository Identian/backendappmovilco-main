using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasFolios
  {
    public RespuestaConsultarResumenFolios RespuestaConsultarResumenFoliosExitosa;
    public RespuestaConsultarResumenFolios RespuestaConsultarResumenFoliosNoEncontrado;
    public RespuestaUsuarioAutenticacionFolios RespuestaAutenticacionFoliosExitosa;
    public RespuestaUsuarioAutenticacionFolios RespuestaAutenticacionFoliosNoEncontrado;
    public EmpresaAutenticacion RespuestaEmpresaAutenticacionExitosa;
    public EmpresaAutenticacion RespuestaEmpresaAutenticacionNoEncontrado;

    public void InicializarRespuestas()
    {
      RespuestaConsultarResumenFoliosExitosa = new()
      {
        Codigo = 200,
        Resultado = "Exitoso",
        Mensaje = "Consulta Exitosa",
        Nit = "900390126",
        IdEmpresa = "1111111111",
        Folios = new()
        {
          Comprados = "1000",
          Consumidos = "200",
          Restantes = "800"
        },
        Errores = null
      };

      RespuestaConsultarResumenFoliosNoEncontrado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Datos no encontrados",
        Nit = null,
        IdEmpresa = null,
        Folios = null,
        Errores = null
      };
      RespuestaAutenticacionFoliosExitosa = new()
      {
        Codigo = 200,
        Resultado = "Exitoso",
        Mensaje = "Consulta Exitosa",
        Token = "eyJleHAiOjE2Nzg0NTczOTMsImlhdCI6MTY3ODQ1Mzc5MywiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7I",
        TiempoExpiracion = "",
        Errores = null
      };
      RespuestaAutenticacionFoliosNoEncontrado = new()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "Datos no encontrados",
        Token = null,
        TiempoExpiracion = null,
        Errores = null
      };
      RespuestaEmpresaAutenticacionExitosa = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaExistente,
        TokenEmpresa = "1231654489481231894das4d8a4d3a",
        TokenClave = "1231654489481231894das4d8a4d3a",
        NitEmpresa = "123456789",
        IdEsquemaEmpresa = "1",
        Entorno = "0"
      };
      RespuestaEmpresaAutenticacionNoEncontrado = new()
      {
        IdEmpresa = ConstantesCompartidasFacturacion.IdEmpresaInexistente,
        TokenEmpresa = null,
        TokenClave = null,
        NitEmpresa = null,
        IdEsquemaEmpresa = null,
        Entorno = null
      };
    }
  }
}
