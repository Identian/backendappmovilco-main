using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class UsuarioAutenticacionDominioTest
  {
    private Mock<IUsuarioAutenticacionRepositorio> _usuarioAutenticacionRepositorio = new();
    private Mock<ICifradoRepositorio> _cifradoRepositorio = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IUsuarioRepositorio> _usuarioRepositorio = new();
    private readonly Mock<IAutenticacionGestionAccesoAppMovil> _autenticacionGestionAccesoAppMovil = new();
    private readonly Mock<IGestionAccesoAppMovil> _gestionAccesoAppMovil = new();
    private readonly RespuestasCompartidas _respuestasCompartidas = new();
    private readonly RespuestasCompartidasUsuarioFacturacion _respuestasCompartidasUsuarioFacturacion = new();
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private const string _usuarioInvalido = "invalido@email.com";
    private const string _usuarioInactivo = "inactivo@email.com";
    private const string _contrasenaInvalida = "123456789";
    private const string _contrasenaInvalidaCifrada = "C99A018DBCCEB3ECA8A40C069B07AAD2622F2A5D08AE40DCBC288AD87316C61789F6E9C3837430F48C67E26B4BD3C24B3664C745E748CEDFC11F52E68317411A";
    private const string _empresaInactiva = "empresaInvalida@email.com";
    private const string _empresaUsuarioInactivo = "empresaUsuarioInvalido@email.com";
    private const string _empresaTokensInvalidos = "empresaTokensInvalidos@email.com";
    private const string _empresaTokensvalidosActivoAppInvalido = "empresaConactivoAppInvalido@email.com";
    private const string _usuarioTokensvalidosActivoAppInvalido = "usuarioConactivoAppInvalido@email.com";
    private const string _empresaNoEncontradaGestionAccesoApp = "empresaNoEncontradaGestionAccesoAppInvalido@email.com";
    private const string _usuarioNoEncontradoGestionAccesoApp = "usuarioNoEncontradoGestionAccesoAppInvalido@email.com";
    private const string _usuarioErrorProcesamientoRedisCache = "usuarioErrorProcesamientoRedisCache@email.com";

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _usuarioAutenticacionRepositorio = new();
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.IsAny<UsuarioAutenticacion>())).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar)))).Returns(_respuestasCompartidas.RespuestaUsuarioRolEstandarAutenticacionExitosa!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioInvalido)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioInvalido!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.IsNullOrEmpty(x.Usuario)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioInvalido!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioInactivo)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioInactivo!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Contrasena, _contrasenaInvalidaCifrada)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionContrasenaInvalida!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.IsNullOrEmpty(x.Contrasena)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionContrasenaInvalida!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaInactiva)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionEmpresaInactiva!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaUsuarioInactivo)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionEmpresaUsuarioInactivo!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaTokensInvalidos)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTokensInvalidos!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaTokensvalidosActivoAppInvalido)))).Returns(_respuestasCompartidas.RespuestaUsuarioRolEstandarAutenticacionExitosaActivoAppInvalido!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _empresaNoEncontradaGestionAccesoApp)))).Returns(_respuestasCompartidas.RespuestaEmpresaNoEncontradaGestionAccesoApp!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioNoEncontradoGestionAccesoApp)))).Returns(_respuestasCompartidas.RespuestausuarioNoEncontrad0GestionAccesoApp!);
      _usuarioAutenticacionRepositorio.Setup(u => u.AutenticarUsuario(It.Is<UsuarioAutenticacion>(x => string.Equals(x.Usuario, _usuarioErrorProcesamientoRedisCache)))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionProcesadoConErrores!);

      _usuarioRepositorio = new();
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.IsAny<string>())).Returns(_respuestasCompartidasUsuarioFacturacion.DatosNoEncontradosConsultarUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t != null && t != ""))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.CorreoUsuarioExistente))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioRolEstandar);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == _empresaTokensvalidosActivoAppInvalido))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioActivoAppEmpresaInvalido);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == _usuarioTokensvalidosActivoAppInvalido))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioActivoAppInvalido);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == _usuarioNoEncontradoGestionAccesoApp))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioActivoAppEmpresaInvalido);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t == _usuarioErrorProcesamientoRedisCache))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuarioErrorRedisCache);
      _usuarioRepositorio.Setup(u => u.ConsultarInformacion(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido), It.Is<string>(t => t == ConstantesCompartidasFacturacion.NitExistente), It.Is<string>(t => t != _usuarioInvalido))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarUsuario);

      _usuarioRepositorio.Setup(u => u.ConsultarRollUsuario(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdUsuario))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarRollUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarRollUsuario(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdUsuarioAutenticadoConErrores))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarRollUsuario);
      _usuarioRepositorio.Setup(u => u.ConsultarRollUsuario(It.Is<string>(t => t == ConstantesCompartidasFacturacion.IdUsuarioRolEstandar))).Returns(_respuestasCompartidasUsuarioFacturacion.DatosExistentesConsultarRolUsuarioRolEstandar);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.ValorBearerTokenValidoActivoAppInvalido)))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosaActivoAppInvalido);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.ValorBearerTokenValidoActivoAppUsuarioInvalido)))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosaActivoAppUsuarioInvalido);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.ValorBearerTokenValidoEmpresaInvalidaGestionAccessoApp)))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosaEmpresaInvalidaGestionAccesoApp);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.ValorBearerTokenValidoUsuarioInvalidoGestionAccessoApp)))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosaActivoAppUsuarioInvalido);

      _empresaAutenticacionRepositorio.Setup(e => e.ConsultarClavesEmpresaPorIdUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuario))).Returns(_respuestasCompartidasEmpresaAutenticacion.RespuestaConsultarClavesEmpresaExitosa);
      _empresaAutenticacionRepositorio.Setup(e => e.ConsultarClavesEmpresaPorIdUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuarioAutenticadoConErrores))).Returns(_respuestasCompartidasEmpresaAutenticacion.RespuestaConsultarClavesEmpresaExitosa);
      _empresaAutenticacionRepositorio.Setup(e => e.ConsultarClavesEmpresaPorIdUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuarioRolEstandar))).Returns(_respuestasCompartidasEmpresaAutenticacion.RespuestaConsultarClavesEmpresaExitosaSinClaveSecreta);

      _cifradoRepositorio = new();
      _cifradoRepositorio.Setup(c => c.CifrarTexto(It.IsAny<string>())).Returns("ContraseñaCifrada");
      _cifradoRepositorio.Setup(c => c.CifrarTexto(It.Is<string>(x => string.IsNullOrEmpty(x)))).Returns("");
      _cifradoRepositorio.Setup(c => c.CifrarTexto(It.Is<string>(x => string.Equals(x, _contrasenaInvalida)))).Returns(_contrasenaInvalidaCifrada);
      _cifradoRepositorio.Setup(c => c.DecodificarJwtToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.BearerTokenValido)))).Returns(ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      _cifradoRepositorio.Setup(c => c.DecodificarJwtToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.BearerTokenValidoActivoAppInvalido)))).Returns(ConstantesCompartidasFacturacion.ValorBearerTokenValidoActivoAppInvalido);
      _cifradoRepositorio.Setup(c => c.DecodificarJwtToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.BearerTokenValidoActivoAppUsuarioInvalido)))).Returns(ConstantesCompartidasFacturacion.ValorBearerTokenValidoActivoAppUsuarioInvalido);
      _cifradoRepositorio.Setup(c => c.DecodificarJwtToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.BearerTokenValidoEmpresaInvalidaGestionAccesoApp)))).Returns(ConstantesCompartidasFacturacion.ValorBearerTokenValidoEmpresaInvalidaGestionAccessoApp);
      _cifradoRepositorio.Setup(c => c.DecodificarJwtToken(It.Is<string>(x => string.Equals(x, ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontradoGestionAccesoApp)))).Returns(ConstantesCompartidasFacturacion.ValorBearerTokenValidoUsuarioInvalidoGestionAccessoApp);


      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.InsertarUsuarioAutenticado(It.IsAny<RespuestaIniciarSesion>())).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);

      _autenticacionGestionAccesoAppMovil.Setup(e => e.AutenticarUsuarioGestionAppMovil()).Returns(RespuestasCompartidas.DatosValidosAutenticacionUsuarioFacturacion);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoEmpresa(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente))).Returns(RespuestasCompartidas.DatosValidosControlAccesoEmpresa);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoEmpresa(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido))).Returns(RespuestasCompartidas.DatosInvalidosControlAccesoEmpresa);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoEmpresa(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaInvalidaGestionAccesoApp))).Returns(RespuestasCompartidas.DatosInvalidosNoExistenRegistrosControlAccesoEmpresa);

      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.IsAny<string>())).Returns(RespuestasCompartidas.DatosValidosControlAccesoUsuario);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistenteActivoAppInvalido), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuario))).Returns(RespuestasCompartidas.DatosValidosControlAccesoUsuario);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuarioAppInactivo))).Returns(RespuestasCompartidas.DatosInvalidosControlAccesoUsuario);
      _gestionAccesoAppMovil.Setup(e => e.GestionAccesoUsuario(It.Is<string>(x => x == ConstantesCompartidasFacturacion.TokenUsuarioGestionAccesoAppMovil), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdEmpresaExistente), It.Is<string>(x => x == ConstantesCompartidasFacturacion.IdUsuarioInvalid0GestionAccesoApp))).Returns(RespuestasCompartidas.DatosInvalidosNoExistenRegistrosControlAccesoEmpresa);
    }

    [Test]
    [TestCase(_usuarioInvalido, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInvalido)]
    [TestCase(_usuarioInactivo, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInactivo)]
    [TestCase("empleado@email.com", _contrasenaInvalida, 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionContrasenaInvalida)]
    [TestCase(_empresaInactiva, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionEmpresaInactiva)]
    [TestCase(_empresaUsuarioInactivo, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionEmpresaUsuarioInactivo)]
    [TestCase(_empresaTokensInvalidos, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionTokensInvalidos)]
    [TestCase(null, "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInvalido)]
    [TestCase("", "123456789ASDFG", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionUsuarioInvalido)]
    [TestCase("empleado@email.com", null, 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionContrasenaInvalida)]
    [TestCase("empleado@email.com", "", 0, RespuestasCompartidas.responseAutenticacionError, RespuestasCompartidas.messageAutenticacionContrasenaInvalida)]
    public void AutenticarUsuario(string? usuario, string? contrasena, int codigoEsperado, string responseEsperado, string messageEsperado)
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = usuario,
        Contrasena = contrasena,
        TipoApp = "1",
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(codigoEsperado));
        Assert.That(respuesta.response, Is.EqualTo(responseEsperado));
        Assert.That(respuesta.message, Is.EqualTo(messageEsperado));
        Assert.That(string.IsNullOrEmpty(respuesta.token), Is.True);
      });
    }

    [Test]
    [TestCase("usuario@tfhka.com", "123456789ASDFG")]
    [TestCase("empleado@empresa.com.co", "123456789!#$%")]
    [TestCase("empleado_1@empresa.com.co", "123456789!#$%")]
    [TestCase("usuario", "contraseña")]
    [TestCase("1234", "1234")]
    public void CuandoCredencialesCorrectas_AutenticarUsuario_DevuelveAutenticacionExitosa(string usuario, string contrasena)
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = usuario,
        Contrasena = contrasena,
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa);
    }

    [Test]
    public void CuandoRolUsuarioEsEstandar_AutenticarUsuario_DevuelveAutenticacionExitosaSinClaveSecretaContribuyente()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = ConstantesCompartidasFacturacion.CorreoUsuarioRolEstandar,
        Contrasena = "Contraseña",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioRolEstandarAutenticacionExitosa);
    }


    [Test]
    public void CuandoEmpresaEsValida_ActivoAppInvalido_DevuelveCodigo401()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = _usuarioTokensvalidosActivoAppInvalido,
        Contrasena = "Contraseña",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioAppInvalido);
    }

    [Test]
    public void CuandoUsuarioEsValido_ActivoAppInvalido_DevuelveCodigo401()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = _empresaTokensvalidosActivoAppInvalido,
        Contrasena = "Contraseña",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioAppEmpresaInvalido);
    }

    [Test]
    public void CuandoGestionAccesoAppNoEncuentraEmpresa_RetornaCodigo404()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = _empresaNoEncontradaGestionAccesoApp,
        Contrasena = "Contraseña",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaEmpresaNoEncontradaGestionAccesoApp);
    }

    [Test]
    public void CuandoGestionAccesoAppNoEncuentraUsuario_RetornaCodigo404()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = _usuarioNoEncontradoGestionAccesoApp,
        Contrasena = "Contraseña",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestausuarioNoEncontrad0GestionAccesoApp);
    }
    [Test]
    public void TimeOutRediscache_AutenticarUsuario_DevuelveAutenticacion()
    {
      //Arrange
      UsuarioAutenticacionDominio usuarioAutenticacionDominio = new(_redisCacheRepositorio.Object, _usuarioAutenticacionRepositorio.Object, _empresaAutenticacionRepositorio.Object, _usuarioRepositorio.Object, _cifradoRepositorio.Object, _autenticacionGestionAccesoAppMovil.Object, _gestionAccesoAppMovil.Object);
      UsuarioAutenticacion usuarioAutenticacion = new()
      {
        Usuario = _usuarioErrorProcesamientoRedisCache,
        Contrasena = "123456789ASDFG",
        TipoApp = "1"
      };

      //Act
      var respuesta = usuarioAutenticacionDominio.AutenticarUsuario(usuarioAutenticacion);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaUsuarioAutenticacionProcesadoConErrores);
    }
  }
}
