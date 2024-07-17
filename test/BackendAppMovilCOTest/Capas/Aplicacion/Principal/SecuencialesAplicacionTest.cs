using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas.Secuenciales;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Secuenciales;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Comun.Utils;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class SecuencialesAplicacionTest
  {
    private Mock<ISecuencialesDominio> _secuencialesDominio = new();
    private SecuencialesAplicacion _secuencialesAplicacion;
    private RespuestasCompartidasSecuencial _respuestasCompartidasSecuencial = new();
    private SolicitudesCompartidasSecuencial _solicitudes = new();
    private IMapper _mapeador;



    [SetUp]
    public void SetUp()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestasCompartidasSecuencial = new();

      _secuencialesDominio = new();
      _secuencialesDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdNumeracionValido), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasSecuencial.DatosExistentesSeleccionarSecuencialExisoso!);
      _secuencialesDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdNumeracionInvalido), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasSecuencial.DatosSecuencialNoAsociado!);
      _secuencialesDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarSecuencial>(s => s.IdNumeracion == ConstantesCompartidasFacturacion.IdIdNumeracionYaSeleccionado), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasSecuencial.DatosSecuencialYaSeleccionado!);

      _secuencialesAplicacion = new(_secuencialesDominio.Object, _mapeador);

    }

    #region SeleccionarSecuencial
    [Test]
    public void SeleccionarSecuencial_Cuando_secuencialEsValido_RespuestaExisa()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencialDto!.IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionValido;
      //Act
      var respuesta = _secuencialesAplicacion.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencialDto!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosExistentesSeleccionarSecuencialExisosoDto!.ToString());
    }

    [Test]
    public void SeleccionarSecuencial_CuandosecuencialNoAsociado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencialDto!.IdNumeracion = ConstantesCompartidasFacturacion.IdNumeracionInvalido;
      //Act
      var respuesta = _secuencialesAplicacion.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencialDto!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosSecuencialNoAsociadoDto!.ToString());
    }

    [Test]
    public void SeleccionarSecuencial_Cuando_secuencialYaSeleccionado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.datosValidosSeleccionarSecuencialDto!.IdNumeracion = ConstantesCompartidasFacturacion.IdIdNumeracionYaSeleccionado;
      //Act
      var respuesta = _secuencialesAplicacion.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencialDto!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.DatosSecuencialYaSeleccionadoDto!.ToString());
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
      var respuesta = _secuencialesAplicacion.Seleccionar(_solicitudes.datosValidosSeleccionarSecuencialDto!, bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasSecuencial.RespuestaEmpresaNoAutenticadoDto!.ToString());
    }


    #endregion|
  }
}
