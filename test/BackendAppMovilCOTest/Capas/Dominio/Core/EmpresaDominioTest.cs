using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class EmpresaDominioTest
  {
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private EmpresaDominio _empresaDominio;
    private RespuestasCompartidas _respuestasCompartidas = new();
    private RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private SolicitudesCompartidasEmpresa _solicitudes = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudes = new();
      _respuestasCompartidas = new();
      _respuestasCompartidasEmpresaAutenticacion = new();
      _respuestasCompartidasEmpresa = new();

      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);


      _empresaRepositorio = new();
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);

      _empresaRepositorio = new();
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.IsAny<int>())).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaNoExisteDB);
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.Is<int>(id => id == Convert.ToInt32(ConstantesCompartidasFacturacion.IdEmpresaExistente)))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB);

      _empresaRepositorio.Setup(c => c.ConsultarEmpresa(It.Is<SolicitudConsultarFacturacion>(s => s.Nit == ConstantesCompartidasFacturacion.NitExistente
                                                                                               && s.TokenEmpresa == ConstantesCompartidasFacturacion.TokenEmpresaValido
                                                                                               && s.TokenClave == ConstantesCompartidasFacturacion.TokenClaveValido))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteApi!);
      _empresaRepositorio.Setup(c => c.ConsultarEmpresa(It.Is<SolicitudConsultarFacturacion>(s => s.Plataforma == ConstantesCompartidasFacturacion.PlataformaNoDisponible))).Returns(_respuestasCompartidasEmpresa.RespuestaPlataformaNoDisponibleConsultarEmpresaApi!);

      _empresaDominio = new(_redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _empresaRepositorio.Object);
    }

    #region Consultar Empresa

    [Test]
    public void ConsultarEmpresa_TimeoutRedisCache_DatosTokenvalidos_DevuelveEmpresa()
    {
      //Arrange

      //Act
      var respuesta = _empresaDominio.ConsultarEmpresa(_solicitudes.DatosExistentesConsultarEmpresa, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteApi!.ToString());
    }

    [Test]
    public void ConsultarEmpresa_CuandoDatosExisten_DevuelveEmpresa()
    {
      //Arrange

      //Act
      var respuesta = _empresaDominio.ConsultarEmpresa(_solicitudes.DatosExistentesConsultarEmpresa, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteApi!.ToString());
    }
    [TestCase(null)]
    [TestCase("")]
    [TestCase("Bearer")]
    [TestCase("Token")]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51)]
    public void ConsultarEmpresa_CuandoSeHaCerradoSesion_Devuelve401(string? bearerToken)
    {
      //Arrange

      //Act
      var respuesta = _empresaDominio.ConsultarEmpresa(_solicitudes.DatosExistentesConsultarEmpresa, bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaNoAutenticado!.ToString());
    }

    [Test]
    public void ConsultarEmpresa_CuandoPlataformaNoDisponible_DevuelveError403()
    {
      //Arrange

      //Act
      var respuesta = _empresaDominio.ConsultarEmpresa(_solicitudes.PlataformaNoDisponibleConsultarEmpresa, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEmpresa.RespuestaPlataformaNoDisponibleConsultarEmpresaApi!.ToString());
    }
    #endregion
  }
}
