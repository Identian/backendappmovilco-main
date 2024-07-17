using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Dispositivos;
using Dominio.Core;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Newtonsoft.Json.Linq;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class DispositivosDominioTest
  {
    private Mock<IGestionAccesoAppMovil> _gestionAccesoAppMovil = new();
    private Mock<IAutenticacionGestionAccesoAppMovil> _autenticacionGestionAccesoAppMovil = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IDispositivosAppMovilRepositorioSql> _dispositivosAppMovilRepositorioSql = new();
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private DispositivosDominio _dispositivosDominio;
    private RespuestasCompartidasDispositivos _respuestas = new();
    private readonly RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private SolicitudesCompartidasDispositivos _solicitudes = new();
    private readonly RespuestasCompartidasEmpresa _respuestasEmpresa = new();
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private readonly RespuestasCompartidas _respuestasCompartidas = new();

    [SetUp]
    public void SetUp()
    {
      _solicitudes = new();
      _respuestas = new();
      _respuestasEmpresa.InicializarRespuestas();

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);


      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);



      _empresaRepositorio = new();
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB);

      _autenticacionGestionAccesoAppMovil = new();
      _autenticacionGestionAccesoAppMovil.Setup(e => e.AutenticarUsuarioGestionAppMovil()).Returns(RespuestasCompartidas.DatosValidosAutenticacionUsuarioFacturacion);


      _gestionAccesoAppMovil = new();
      _gestionAccesoAppMovil.Setup(c => c.CrearAccesoDispositivo(It.Is<Dispositivo>(s => s.IdEmpresa == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                 It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                                 .Returns(_respuestas.DatosValidosCrearDispositivo);
      _gestionAccesoAppMovil.Setup(c => c.CrearAccesoDispositivo(It.Is<Dispositivo>(s => s.SerialLogico != ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                                 It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                                 .Returns(_respuestas.DatosInvalidosDispositivoYaExiste);

      _gestionAccesoAppMovil.Setup(g => g.ConsultarSuscripcionDispositivo(It.IsAny<string>(),
                                                                          It.IsAny<string>(),
                                                                          It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                                          .Returns(_respuestas.ConsultarSuscripcionDispositivoNoRegistrado);
      _gestionAccesoAppMovil.Setup(g => g.ConsultarSuscripcionDispositivo(It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                          It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdSuscripcionDispositivoInexistente),
                                                                          It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                                          .Returns(_respuestas.ConsultarSuscripcionDispositivoInexistente);
      _gestionAccesoAppMovil.Setup(g => g.ConsultarSuscripcionDispositivo(It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                          It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdSuscripcionDispositivoExistente),
                                                                          It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                                          .Returns(_respuestas.ConsultarSuscripcionDispositivoExistente);


      _gestionAccesoAppMovil.Setup(g => g.AsociarAlias(It.Is<SolicitudAsociarAlias>(s => s.IdEmpresa == ConstantesCompartidasFacturacion.IdEmpresaExistente && s.SerialLogico == ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                       It.Is<string>(s => s == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                       .Returns(_respuestas.DatosValidosAsociarAlias);
      _gestionAccesoAppMovil.Setup(g => g.AsociarAlias(It.Is<SolicitudAsociarAlias>(s => s.IdEmpresa == ConstantesCompartidasFacturacion.IdEmpresaExistente && s.SerialLogico != ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                       It.Is<string>(s => s == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil)))
                                                      .Returns(_respuestas.DatosInvalidosAsociarAlias);

      _dispositivosAppMovilRepositorioSql = new();
      _dispositivosAppMovilRepositorioSql.Setup(d => d.ConsultarSuscripcionDispositivoPorSerialLogico(It.IsAny<string>(), It.IsAny<string>()))
                                                                                                      .Returns(_respuestas.ConsultarSuscripcionDispositivoPorSerialLogicoNoRegistrado);
      _dispositivosAppMovilRepositorioSql.Setup(d => d.ConsultarSuscripcionDispositivoPorSerialLogico(It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                      It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente)))
                                                                                                      .Returns(_respuestas.ConsultarSuscripcionDispositivoPorSerialLogicoInexistente);
      _dispositivosAppMovilRepositorioSql.Setup(d => d.ConsultarSuscripcionDispositivoPorSerialLogico(It.Is<string>(s => s == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                      It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente)))
                                                                                                      .Returns(_respuestas.ConsultarSuscripcionDispositivoPorSerialLogicoExistente);

      _dispositivosDominio = new(_autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object, _empresaAutenticacionRepositorio.Object, _redisCacheRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
    }

    #region crear 

    [Test]
    public void CrearDispositivo_TimeoutRedisCache_DatosTokenvalidos_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivo,
      ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache,
      ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosValidosCrearDispositivo);
    }

    [Test]

    public void CrearDispositivo_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivo,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosValidosCrearDispositivo);
    }
    [Test]
    public void CrearDispositivo_DispositivoNoValido_DevuelveCodigo400()
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivo.SerialLogico = "njfdkfnd";
      //Act
      var respuesta = _dispositivosDominio.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivo,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosInvalidosDispositivoYaExiste);
    }
    [Test]
    public void CrearDispositivo_SesionInvalida_DevuelveCodigo401()
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivo.SerialLogico = "njfdkfnd";
      //Act
      var respuesta = _dispositivosDominio.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivo,
      ConstantesCompartidasFacturacion.BearerTokenInvalido,
      "bearertokennovalido");
      //Assert
      respuesta.Should().BeEquivalentTo(JToken.FromObject(_respuestasCompartidas.RespuestaUsuarioAutenticacionTokenExpiradoEnCache!));
    }
    #endregion

    #region Consultar Suscripcion Dispositivo

    [Test]
    public void ConsultarSuscripcionDispositivo_TimeoutRedisCache_DatosTokenvalidos_DevuelveConsultaExitosa()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosDominio.ConsultarSuscripcionDispositivo(ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente,
      ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache,
      ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);
      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoExistente);
    }

    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoSuscripcionExistente_ConsultaExistosa()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosDominio.ConsultarSuscripcionDispositivo(ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoExistente);
    }
    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoSuscripcionInexistente_DevuelveCodigo404()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosDominio.ConsultarSuscripcionDispositivo(ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoInexistente);
    }
    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoNoRegistrado_DevuelveCodigo404()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosDominio.ConsultarSuscripcionDispositivo(ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoNoRegistrado);
    }

    [Test]
    public void ConsultarSuscripcionDispositivo_BearerTokenInvalido_DevuelveCodigo401()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.ConsultarSuscripcionDispositivo(ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente,
                                                                           ConstantesCompartidasFacturacion.BearerTokenInvalido,
                                                                           ConstantesCompartidasFacturacion.ValorBearerTokenInvalido);
      //Assert
      respuesta.Should().BeEquivalentTo(JToken.FromObject(_respuestasCompartidas.RespuestaUsuarioAutenticacionTokenExpiradoEnCache!));
    }
    #endregion

    #region asociar alias 

    [Test]
    public void AsociarAlias_TimeoutRedisCache_DatosTokenvalidos_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.AsociarAlias(_solicitudes.DatosValidosAsociarAlias,
      ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache,
      ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosValidosAsociarAlias);
    }

    [Test]
    public void AsociarAlias_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.AsociarAlias(_solicitudes.DatosValidosAsociarAlias,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosValidosAsociarAlias);
    }
    [Test]
    public void AsociarAlias_DispositivoNoValido_DevuelveCodigo404()
    {
      //Arrange
      _solicitudes.DatosValidosAsociarAlias.SerialLogico = "njfdkfnd";
      //Act
      var respuesta = _dispositivosDominio.AsociarAlias(_solicitudes.DatosValidosAsociarAlias,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosInvalidosAsociarAlias);
    }
    [Test]
    public void AsociarAlias_SesionInvalida_DevuelveCodigo401()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosDominio.AsociarAlias(_solicitudes.DatosValidosAsociarAlias,
      ConstantesCompartidasFacturacion.BearerTokenInvalido,
      "bearertokennovalido");
      //Assert
      respuesta.Should().BeEquivalentTo(JToken.FromObject(_respuestasCompartidas.RespuestaUsuarioAutenticacionTokenExpiradoEnCache!));
    }
    #endregion
  }
}
