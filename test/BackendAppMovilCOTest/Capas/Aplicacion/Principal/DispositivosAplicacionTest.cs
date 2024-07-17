using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes.Dispositivos;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class DispositivosAplicacionTest
  {
    private Mock<IDispositivosDominio> _dispositivosDominio = new();
    private DispositivosAplicacion _dispositivosAplicacion;
    private RespuestasCompartidasDispositivos _respuestas = new();
    private SolicitudesCompartidasDispositivos _solicitudes = new();
    private IMapper _mapeador;

    [SetUp]
    public void Setup()
    {
      //Reutilizar el mapeo real existente en la capa Trasversal para no hacer Mock
      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mapperConfiguration);

      _solicitudes = new();
      _respuestas = new();

      _dispositivosDominio = new();
      _dispositivosDominio.Setup(c => c.CrearAccesoDispositivo(It.Is<Dispositivo>(s => s.IdEmpresa != ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                               It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                               It.Is<string>(vb => vb == ConstantesCompartidasFacturacion.ValorBearerTokenValido)
                                                               )).Returns(_respuestas.DatosValidosCrearDispositivo);

      _dispositivosDominio.Setup(c => c.CrearAccesoDispositivo(It.Is<Dispositivo>(s => s.SerialLogico != ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                         It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                         It.Is<string>(vb => vb == ConstantesCompartidasFacturacion.ValorBearerTokenValido)
                                                         )).Returns(_respuestas.DatosInvalidosDispositivoYaExiste);

      _dispositivosDominio.Setup(c => c.ConsultarSuscripcionDispositivo(It.IsAny<string>(),
                                                                        It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                                        It.Is<string>(vb => vb == ConstantesCompartidasFacturacion.ValorBearerTokenValido)))
                                                                        .Returns(_respuestas.ConsultarSuscripcionDispositivoNoRegistrado);
      _dispositivosDominio.Setup(c => c.ConsultarSuscripcionDispositivo(It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente),
                                                                        It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                                        It.Is<string>(vb => vb == ConstantesCompartidasFacturacion.ValorBearerTokenValido)))
                                                                        .Returns(_respuestas.ConsultarSuscripcionDispositivoInexistente);
      _dispositivosDominio.Setup(c => c.ConsultarSuscripcionDispositivo(It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente),
                                                                        It.Is<string>(bt => bt == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                                        It.Is<string>(vb => vb == ConstantesCompartidasFacturacion.ValorBearerTokenValido)))
                                                                        .Returns(_respuestas.ConsultarSuscripcionDispositivoExistente);

      _dispositivosDominio.Setup(g => g.AsociarAlias(It.Is<SolicitudAsociarAlias>(s => s.SerialLogico == ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                 It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                 It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValido)))
                                                 .Returns(_respuestas.DatosValidosAsociarAlias);
      _dispositivosDominio.Setup(g => g.AsociarAlias(It.Is<SolicitudAsociarAlias>(s => s.SerialLogico != ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido),
                                                 It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido),
                                                 It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValido)))
                                                      .Returns(_respuestas.DatosInvalidosAsociarAlias);


      _dispositivosAplicacion = new(_dispositivosDominio.Object, _mapeador);
    }

    #region crear dispositivo
    [Test]
    public void CrearDispositivo_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosAplicacion.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivoDto,
                                                                     ConstantesCompartidasFacturacion.BearerTokenValido,
                                                                     ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.DatosValidosCrearDispositivo);
    }

    [Test]
    public void CrearDispositivo_CuandoDispositivoYaExiste_DevuelveCodigo400()
    {
      //Arrange
      _solicitudes.DatosValidosCrearDispositivoDto.SerialLogico = "no valido";

      //Act
      var respuestaDto = _dispositivosAplicacion.CrearAccesoDispositivo(_solicitudes.DatosValidosCrearDispositivoDto,
                                                                     ConstantesCompartidasFacturacion.BearerTokenValido,
                                                                     ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.DatosInvalidosDispositivoYaExiste);
    }
    #endregion

    #region Consultar Suscripcion Dispositivo
    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoSuscripcionExistente_ConsultaExistosa()
    {
      //Arrange

      //Act
      var respuestaDto = _dispositivosAplicacion.ConsultarSuscripcionDispositivo(_solicitudes.ConsultarSuscripcionDispositivoDto,
                                                                                 ConstantesCompartidasFacturacion.BearerTokenValido,
                                                                                 ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoExistente);
    }

    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoSuscripcionInexistente_DevuelveCodigo404()
    {
      //Arrange
      _solicitudes.ConsultarSuscripcionDispositivoDto.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente;

      //Act
      var respuestaDto = _dispositivosAplicacion.ConsultarSuscripcionDispositivo(_solicitudes.ConsultarSuscripcionDispositivoDto,
                                                                                 ConstantesCompartidasFacturacion.BearerTokenValido,
                                                                                 ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoInexistente);
    }


    [Test]
    public void ConsultarSuscripcionDispositivo_SerialLogicoDispositivoNoRegistrado_DevuelveCodigo404()
    {
      //Arrange
      _solicitudes.ConsultarSuscripcionDispositivoDto.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido;

      //Act
      var respuestaDto = _dispositivosAplicacion.ConsultarSuscripcionDispositivo(_solicitudes.ConsultarSuscripcionDispositivoDto,
                                                                                 ConstantesCompartidasFacturacion.BearerTokenValido,
                                                                                 ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestas.ConsultarSuscripcionDispositivoNoRegistrado);
    }
    #endregion

    #region asociar alias 

    [Test]
    public void AsociarAlias_DevuelveCodigo201()
    {
      //Arrange

      //Act
      var respuesta = _dispositivosAplicacion.AsociarAlias(_solicitudes.DatosValidosAsociarAliasDto,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosValidosAsociarAlias);
    }
    [Test]
    public void AsociarAlias_DispositivoNoValido_DevuelveCodigo404()
    {
      //Arrange
      _solicitudes.DatosValidosAsociarAliasDto.SerialLogico = "njfdkfnd";
      //Act
      var respuesta = _dispositivosAplicacion.AsociarAlias(_solicitudes.DatosValidosAsociarAliasDto,
      ConstantesCompartidasFacturacion.BearerTokenValido,
      ConstantesCompartidasFacturacion.ValorBearerTokenValido);
      //Assert
      respuesta.Should().BeEquivalentTo(_respuestas.DatosInvalidosAsociarAlias);
    }
    #endregion

  }
}
