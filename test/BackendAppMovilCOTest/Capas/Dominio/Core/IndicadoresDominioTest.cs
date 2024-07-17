using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Transversal.Comun.Respuestas;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class IndicadoresDominioTest
  {
    private IndicadoresDominio _indicadoresDominio;
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private Mock<IIndicadoresRepositorio> _indicadoresRepositorio = new();
    private readonly SolicitudesCompartidasIndicadores _solicitudesCompartidasIndicadores = new();
    private readonly RespuestasCompartidas _respuestasCompartidas = new();
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private readonly RespuestasCompartidasEmpresa _respuestasEmpresa = new();
    private readonly RespuestasCompartidasIndicadores _respuestasCompartidasIndicadores = new();

    [SetUp]
    public void SetUp()
    {
      //Arrange general

      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);


      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.IsAny<string>())).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);

      _empresaRepositorio = new();
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.IsAny<int>())).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaExisteDB);
      _empresaRepositorio.Setup(e => e.ConsultarEmpresaPorId(It.Is<int>(id => id == Convert.ToInt32(ConstantesCompartidasFacturacion.IdEmpresaInexistente)))).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaNoExisteDB);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasEmpresa.RespuestaConsultarEmpresaExisteDB);


      _indicadoresRepositorio = new();
      _indicadoresRepositorio.Setup(i => i.ConsultarTotalDocumentos(It.Is<SolicitudConsultarTotalDocumentos>(f => f.Filtros!.Anio != "2000"))).Returns(_respuestasCompartidasIndicadores!.DatosExistentesJTokenConsultarTotalDocumentosDto!);
      _indicadoresRepositorio.Setup(i => i.ConsultarTotalDocumentos(It.Is<SolicitudConsultarTotalDocumentos>(f => f.Filtros!.Anio == "2000"))).Returns(_respuestasCompartidasIndicadores!.DatosVaciosJTokenConsultarTotalDocumentosDto!);

      _indicadoresDominio = new(_redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _empresaRepositorio.Object, _indicadoresRepositorio.Object);
    }

    #region ConsultarTotalDocumentos

    [Test]
    public void ConsultarTotalDocumentos_TimeoutRedisCache_DatosTokenvalidos_DevuelveCodigo200()
    {
      //Act
      RespuestaBase respuestaDto = RespuestaBase.ConvertirJTokenARespuestaBase(_indicadoresDominio.ConsultarTotalDocumentos(_solicitudesCompartidasIndicadores.DatosExistentesConsultarTotalDocumentos!, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid));

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestasCompartidasIndicadores.DatosExistentesConsultarTotalDocumentosDto);
    }

    [Test]
    public void ConsultarTotalDocumentos_CuandoDatosExisten_DevuelveCodigo200()
    {
      //Act
      RespuestaBase respuestaDto = RespuestaBase.ConvertirJTokenARespuestaBase(_indicadoresDominio.ConsultarTotalDocumentos(_solicitudesCompartidasIndicadores.DatosExistentesConsultarTotalDocumentos!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido));

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestasCompartidasIndicadores.DatosExistentesConsultarTotalDocumentosDto);
    }

    [Test]
    public void ConsultarTotalDocumentos_CuandoDatosVacios_DevuelveCodigo404()
    {
      //Act
      RespuestaBase respuestaDto = RespuestaBase.ConvertirJTokenARespuestaBase(_indicadoresDominio.ConsultarTotalDocumentos(_solicitudesCompartidasIndicadores.DatosVaciosConsultarTotalDocumentos!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido));

      //Assert
      respuestaDto.Should().BeEquivalentTo(_respuestasCompartidasIndicadores.DatosVaciosConsultarTotalDocumentosDto);
    }
    #endregion ConsultarTotalDocumentos
  }
}
