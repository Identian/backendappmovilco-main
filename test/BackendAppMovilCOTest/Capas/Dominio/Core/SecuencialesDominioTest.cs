using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas.Secuenciales;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Secuenciales;
using Dominio.Core;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class SecuencialesDominioTest
  {
    private Mock<ISecuencialesRepositorio> _secuencialesRepositorio = new();
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private readonly Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private SecuencialesDominio _secuencialesDominio;
    private readonly RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private RespuestasCompartidas _respuestasCompartidas = new();
    private RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private RespuestasCompartidasSecuencial _respuestasCompartidasSecuencial = new();
    private SolicitudesCompartidasSecuencial _solicitudes = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      _solicitudes = new();
      _respuestasCompartidas = new();
      _respuestasCompartidasEmpresaAutenticacion = new();
      _respuestasCompartidasSecuencial = new();


      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);

      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB);

      _secuencialesRepositorio = new();
      _secuencialesRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdNumeracionValido), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasSecuencial.DatosExistentesSeleccionarSecuencialExisoso!);
      _secuencialesRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdNumeracionInvalido), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasSecuencial.DatosSecuencialNoAsociado!);
      _secuencialesRepositorio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdIdNumeracionYaSeleccionado), It.IsAny<EmpresaAutenticacion>())).Returns(_respuestasCompartidasSecuencial.DatosSecuencialYaSeleccionado!);

      _secuencialesDominio = new(_secuencialesRepositorio.Object, _empresaRepositorio.Object, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object);
    }

    #region SeleccionarSecuencial

    [Test]
    public void SeleccionarSecuencial_TimeoutRedisCache_DatosTokenvalidos_RespuestaExisa()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencial!.IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido;
      //Act
      var respuesta = _secuencialesDominio.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencial, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosExistentesSeleccionarSecuencialExisoso!.ToString());
    }

    [Test]
    public void SeleccionarSecuencial_Cuando_secuencialEsValido_RespuestaExisa()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencial!.IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido;
      //Act
      var respuesta = _secuencialesDominio.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencial, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosExistentesSeleccionarSecuencialExisoso!.ToString());
    }

    [Test]
    public void SeleccionarSecuencial_CuandosecuencialNoAsociado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencial!.IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionInvalido;
      //Act
      var respuesta = _secuencialesDominio.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencial, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosSecuencialNoAsociado!.ToString());
    }

    [Test]
    public void SeleccionarSecuencial_Cuando_secuencialYaSeleccionado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencial!.IdNumeracion = ConstantesCompartidasFacturacion.IdIdNumeracionYaSeleccionado;
      //Act
      var respuesta = _secuencialesDominio.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencial, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosSecuencialYaSeleccionado!.ToString());
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
    public void SeleccionarSecuancial_CuandoSeHaCerradoSesion_Devuelve401(string? bearerToken)
    {
      //Arrange

      //Act
      var respuesta = _secuencialesDominio.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencial!, bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.RespuestaEmpresaNoAutenticado!.ToString());
    }


    #endregion|
  }
}
