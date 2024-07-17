using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Establecimiento;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Establecimiento;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Comun.Utils;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class EstablecimientoAplicacionTest
  {
    private Mock<IEstablecimientosDominio> _establecimientoDominio = new();
    private EstablecimientosAplicacion _establecimientoAplicacion;
    private RespuestasCompartidasEstablecimiento _respuestasCompartidasEstablecimento = new();
    private SolicitudesCompartidasSeleccionarEstablecimiento _solicitudes = new();
    private IMapper _mapeador;

    [SetUp]
    public void SetUp()
    {
      //Arrange general

      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestasCompartidasEstablecimento = new();

      _establecimientoDominio = new();
      _establecimientoDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoValido), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasEstablecimento.DatosExistentesSeleccionarEstablecimientoExisoso!);
      _establecimientoDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoInvalido), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasEstablecimento.DatosEstablecimientoNoAsociado!);
      _establecimientoDominio.Setup(e => e.Seleccionar(It.Is<SolicitudSeleccionarEstablecimiento>(s => s.IdEstablecimiento == ConstantesCompartidasFacturacion.IdEstablecimientoYaSeleccionado), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasEstablecimento.DatosEstablecimientoYaSeleccionado!);

      _establecimientoAplicacion = new(_establecimientoDominio.Object, _mapeador);
    }
    #region SeleccionarEstablecimiento
    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoExiste_RespuestaExisoza()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimientoDto.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoValido;
      //Act
      var respuesta = _establecimientoAplicacion.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimientoDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosExistentesSeleccionarEstablecimientoExisosoDto!.ToString());
    }

    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoNoAsociadoAEmpresa_RespuestaError_404()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimientoDto.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoInvalido;
      //Act
      var respuesta = _establecimientoAplicacion.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimientoDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosEstablecimientoNoAsociadoDto!.ToString());
    }

    [Test]
    public void SeleccionarEstablecimiento_Cuando_EstablecimientoYaSeleccionado_RespuestaError_404()
    {
      //Arrange
      _solicitudes.solicitudSeleccionarEstablecimientoDto.IdEstablecimiento = ConstantesCompartidasFacturacion.IdEstablecimientoYaSeleccionado;
      //Act
      var respuesta = _establecimientoAplicacion.Seleccionar(_solicitudes.solicitudSeleccionarEstablecimientoDto, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido).ToString();

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstablecimento.DatosEstablecimientoYaSeleccionadoDto!.ToString());
    }

    #endregion
  }
}