using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Comun.Utils;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class EmpresaAplicacionTest
  {
    private Mock<IEmpresaDominio> _empresaDominio = new();
    private EmpresaAplicacion _empresaAplicacion;
    private RespuestasCompartidasEmpresa _respuestas = new();
    private SolicitudesCompartidasEmpresa _solicitudes = new();
    private IMapper _mapeador;

    [SetUp]
    public void SetUp()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestas = new();

      _empresaDominio = new();
      _empresaDominio.Setup(c => c.ConsultarEmpresa(It.Is<SolicitudConsultarFacturacion>(s => s.Plataforma == ConstantesCompartidasFacturacion.PlataformaNoDisponible), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestas.RespuestaPlataformaNoDisponibleConsultarEmpresaApi!);
      _empresaDominio.Setup(c => c.ConsultarEmpresa(It.Is<SolicitudConsultarFacturacion>(s => s.Plataforma == ConstantesCompartidasFacturacion.PlataformaTFHKA), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestas.RespuestaConsultarEmpresaNoAutenticado!);
      _empresaDominio.Setup(c => c.ConsultarEmpresa(It.Is<SolicitudConsultarFacturacion>(s => s.Plataforma == ConstantesCompartidasFacturacion.PlataformaTFHKA), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValido))).Returns(_respuestas.RespuestaConsultarEmpresaExisteApi!);

      _empresaAplicacion = new(_empresaDominio.Object, _mapeador);
    }

    #region Consultar Empresa
    [Test]
    public void ConsultarEmpresa_CuandoDatosExisten_DevuelveInformacionEmpresa()
    {
      //Arrange

      //Act
      var respuesta = _empresaAplicacion.ConsultarEmpresa(_solicitudes.DatosExistentesConsultarEmpresaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.RespuestaConsultarEmpresaExisteApi!.ToString());
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
      var respuesta = _empresaAplicacion.ConsultarEmpresa(_solicitudes.DatosExistentesConsultarEmpresaDto, bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.RespuestaConsultarEmpresaNoAutenticado!.ToString());
    }

    [Test]
    public void ConsultarEmpresa_CuandoPlataformaNoDisponible_DevuelveError403()
    {
      //Arrange

      //Act
      var respuesta = _empresaAplicacion.ConsultarEmpresa(_solicitudes.PlataformaNoDisponibleConsultarEmpresaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.RespuestaPlataformaNoDisponibleConsultarEmpresaApi!.ToString());
    }

    [Test]
    [TestCase("OTRA")]
    [TestCase("")]
    [TestCase(" ")]
    public void ConsultarEmpresa_PlataformaInvalida_DevuelveError400(string? plataforma)
    {
      //Arrange
      _solicitudes.PlataformaInvalidaConsultarEmpresaDto.Plataforma = plataforma;

      //Act
      var respuesta = _empresaAplicacion.ConsultarEmpresa(_solicitudes.PlataformaInvalidaConsultarEmpresaDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.RespuestaPlataformaInvalidaConsultarEmpresa);
    }
    #endregion
  }
}
