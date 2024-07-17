using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Establecimiento;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Establecimiento;
using Dominio.Core;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class EstablecimientoDominioTest
  {
    private Mock<IEstablecimientosRepositorio> _establecimientosRepositorio = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private EstablecimientosDominio _establecimientoDominio;
    private RespuestasCompartidas _respuestasCompartidas = new();
    private readonly RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private RespuestasCompartidasEstablecimiento _respuestasCompartidasEstablecimento = new();
    private SolicitudesCompartidasSeleccionarEstablecimiento _solicitudes = new();


    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudes = new();
      _respuestasCompartidas = new();
      _respuestasCompartidasEmpresaAutenticacion = new();
      _respuestasCompartidasEstablecimento = new();


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

      _establecimientosRepositorio = new();
      _establecimientosRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoValido), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasEstablecimento.DatosExistentesSeleccionarEstablecimientoExisoso!);
      _establecimientosRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoInvalido), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasEstablecimento.DatosEstablecimientoNoAsociado!);
      _establecimientosRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoYaSeleccionado), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasEstablecimento.DatosEstablecimientoYaSeleccionado!);

      _establecimientoDominio = new(_establecimientosRepositorio.Object, _empresaAutenticacionRepositorio.Object, _redisCacheRepositorio.Object, _empresaRepositorio.Object);
    }

    #region SeleccionarEstablecimiento

    [Test]
    public void SeleccionarEstablecimiento_TimeoutRedisCache_DatosTokenvalidos_RespuestaExitosa()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimiento!.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido;
      //Act
      var respuesta = _establecimientoDominio.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimiento, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosExistentesSeleccionarEstablecimientoExisoso!.ToString());
    }

    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoExiste_RespuestaExitosa()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimiento!.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido;
      //Act
      var respuesta = _establecimientoDominio.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimiento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosExistentesSeleccionarEstablecimientoExisoso!.ToString());
    }

    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoNoAsociadoAEmpresa_RespuestaError_404()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimiento!.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoInvalido;
      //Act
      var respuesta = _establecimientoDominio.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimiento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosEstablecimientoNoAsociado!.ToString());
    }

    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoYaSeleccionado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimiento!.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoYaSeleccionado;
      //Act
      var respuesta = _establecimientoDominio.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimiento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosEstablecimientoYaSeleccionado!.ToString());
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
    public void SeleccionarEstablecimiento_CuandoSeHaCerradoSesion_Devuelve401(string? bearerToken)
    {
      //Arrange

      //Act
      var respuesta = _establecimientoDominio.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimiento, bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.RespuestaConsultarEmpresaNoAutenticado!.ToString());
    }


    #endregion|
  }
}
