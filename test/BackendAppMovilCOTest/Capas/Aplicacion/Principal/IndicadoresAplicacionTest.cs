using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes;
using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;
using Dominio.Interfaz;
using Transversal.Comun.Respuestas;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class IndicadoresAplicacionTest
  {
    private IndicadoresAplicacion _indicadoresAplicacion;
    private Mock<IIndicadoresDominio> _indicadoresDominio = new();
    private readonly SolicitudesCompartidasIndicadores _solicitudesCompartidasIndicadores = new();
    private readonly RespuestasCompartidasIndicadores _respuestasCompartidasIndicadores = new();
    private Mapper _mapeador;
    private AppSettingsMock _appSettingsMock;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mappingConfig);

      _appSettingsMock = new();

      _indicadoresDominio = new();
      _indicadoresDominio.Setup(d => d.ConsultarTotalDocumentos(It.Is<FiltrosTotalDocumentos>(f => f.Anio != "2000"), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasIndicadores!.DatosExistentesJTokenConsultarTotalDocumentosDto!);
      _indicadoresDominio.Setup(d => d.ConsultarTotalDocumentos(It.Is<FiltrosTotalDocumentos>(f => f.Anio == "2000"), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasIndicadores!.DatosVaciosJTokenConsultarTotalDocumentosDto!);

      _indicadoresAplicacion = new(_appSettingsMock.Object, _indicadoresDominio.Object, _mapeador);
    }

    #region ConsultarTotalDocumentos
    [Test]
    public void ConsultarTotalDocumentos_CuandoDatosExisten_DevuelveCodigo200()
    {
      //Act
      RespuestaBase respuestaDto = RespuestaBase.ConvertirJTokenARespuestaBase(_indicadoresAplicacion.ConsultarTotalDocumentos(_solicitudesCompartidasIndicadores.DatosExistentesConsultarTotalDocumentosDto!, "bearerToken", "valorBearerToken"));

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestasCompartidasIndicadores.DatosExistentesConsultarTotalDocumentosDto);
    }

    [Test]
    public void ConsultarTotalDocumentos_CuandoDatosVacios_DevuelveCodigo404()
    {
      //Act
      RespuestaBase respuestaDto = RespuestaBase.ConvertirJTokenARespuestaBase(_indicadoresAplicacion.ConsultarTotalDocumentos(_solicitudesCompartidasIndicadores.DatosVaciosConsultarTotalDocumentosDto!, "bearerToken", "valorBearerToken"));

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestasCompartidasIndicadores.DatosVaciosConsultarTotalDocumentosDto);
    }
    #endregion ConsultarTotalDocumentos
  }
}
