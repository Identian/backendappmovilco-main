using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes;
using Dominio.Core;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Infraestructura.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Transversal.Comun.Utils;

namespace BackendAppMovilCOTest.Capas.Dominio.Core
{
  public class DocumentosDominioTest
  {
    private DocumentosDominio _documentosDominio;
    private Mock<IRedisCacheRepositorio> _redisCacheRepositorio = new();
    private Mock<IDocumentosRepositorio> _documentosRepositorio = new();
    private Mock<IDocumentosRepositorioSql> _documentosRepositorioSql = new();
    private Mock<IEmpresaAutenticacionRepositorio> _empresaAutenticacionRepositorio = new();
    private Mock<IReportesRepositorio> _reportesRepositorio = new();
    private Mock<IClientesRepositorioApi> _clientesRepositorioApi = new();
    private Mock<IClientesRepositorioSql> _clientesRepositorioSql = new();
    private Mock<IProductosRepositorioSql> _productosRepositorioSql = new();
    private Mock<INumeracionAutorizadaRepositorio> _numeracionAutorizadaRepositorio = new();
    private Mock<IEstadoDocumentoRepositorio> _estadoDocumentosRepositorio = new();
    private readonly Mock<IDeliveryRepositorio> _deliveryRepositorio = new();
    private Mock<IReferenciaDocumentosRepositorio> _referenciaDocumentoRepositorio = new();
    private Mock<IDispositivosAppMovilRepositorioSql> _dispositivosAppMovilRepositorioSql = new();
    private Mock<IEmpresaRepositorio> _empresaRepositorio = new();
    private readonly RespuestasCompartidas _respuestasCompartidas = new();
    private readonly RespuestasCompartidasDocumentos _respuestasCompartidasDocumentos = new();
    private readonly RespuestasCompartidasEmpresa _respuestasCompartidasEmpresa = new();
    private readonly RespuestasCompartidasEmpresaAutenticacion _respuestasCompartidasEmpresaAutenticacion = new();
    private readonly RespuestasCompartidasReportes _respuestasCompartidasReportes = new();
    private readonly RespuestasCompartidasClientes _respuestasCompartidasClientes = new();
    private readonly RespuestasCompartidasNumeracionAutorizada _respuestasCompartidasNumeracionAutorizada = new();
    private readonly RespuestasCompartidasProductosSql _respuestasCompartidasProductosSql = new();
    private readonly RespuestasCompartidasEstadoDocumento _respuestasCompartidasEstadoDocumento = new();
    private readonly RespuestasCompartidasReferenciaDocumento _respuestasCompartidasConsultarReferenciaDocumento = new();
    private readonly RespuestasCompartidasDispositivos _respuestasCompartidasDispositivos = new();
    private readonly SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento = new();
    private readonly SolicitudesCompartidasConsultarEstadoDocumento _solicitudesCompartidasConsultarEstadoDocumento = new();
    private readonly SolicitudesCompartidasConsultarReferenciaDocumento _solicitudesCompartidasConsultarReferenciaDocumento = new();
    private readonly SolicitudesCompartidasDocumentos _solicitudesCompartidasDocumentos = new();
    private IConfiguration _configuracion;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:EmisionRest:Destinatario:NotificarDestinatarioNull", "NO" },
        { "ServiciosFacturacion:EmisionRest:ApiEnviar:DevolverXML", "0" }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();

      _respuestasCompartidasDocumentos.InicializarRespuestas();
      _respuestasCompartidasEmpresa.InicializarRespuestas();
      _solicitudesCompartidasEmitirDocumento.InicializarSolicitudes();
      _solicitudesCompartidasDocumentos.InicializarSolicitudes();
      _respuestasCompartidasReportes.InicializarRespuestas();
      _respuestasCompartidasNumeracionAutorizada.InicializarRespuestas();
      _respuestasCompartidasDispositivos.InicializarRespuestas();

      _redisCacheRepositorio = new();
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosa!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionExitosaUsuarioNoEncontradoEnBD!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache && s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionTimeOutCache!);
      _redisCacheRepositorio.Setup(r => r.ConsultarUsuarioAutenticado(It.Is<RespuestaIniciarSesion>(s => s.token != ConstantesCompartidasFacturacion.BearerTokenValido && s.token != ConstantesCompartidasFacturacion.BearerTokenValidoUsuarioNoEncontrado && s.token != ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidas.RespuestaUsuarioAutenticacionUsuarioNoEncontradoEnCache!);


      _empresaRepositorio = new();
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaInvalidaInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaDatosTokenInvalidos);
      _empresaRepositorio.Setup(r => r.ConsultarEmpresaPorId(It.Is<int>(c => c == ConstantesCompartidasFacturacion.IdEmpresaExistenteInt))).Returns(_respuestasCompartidasEmpresa.RespuestaConsultarEmpresaExisteDB);


      _documentosRepositorio = new();
      _documentosRepositorio.Setup(d => d.EmitirDocumento(It.IsAny<SolicitudEmitirDocumento>(), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente!);
      _documentosRepositorio.Setup(d => d.EmitirDocumento(It.IsAny<SolicitudEmitirDocumento>(), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente!);
      _documentosRepositorio.Setup(d => d.EmitirDocumento(It.IsAny<SolicitudEmitirDocumento>(), null!)).Returns(_respuestasCompartidasDocumentos.RespuestaDocumentoNoAutenticado!);
      _documentosRepositorio.Setup(d => d.EmitirDocumento(It.IsAny<SolicitudEmitirDocumento>(), string.Empty)).Returns(_respuestasCompartidasDocumentos.RespuestaDocumentoNoAutenticado!);
      _documentosRepositorio.Setup(d => d.EmitirDocumento(It.Is<SolicitudEmitirDocumento>(s => s.Factura!.Cliente!.IdCliente == "00"), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente!);

      _documentosRepositorioSql = new();
      _documentosRepositorioSql.Setup(d => d.ConsultarMontoFacturaPos()).Returns(_respuestasCompartidasDocumentos.RespuestaConsultarMontoFacturaPosExitosa!);

      _numeracionAutorizadaRepositorio = new();
      _numeracionAutorizadaRepositorio.Setup(x => x.ConsultarNumeracionAutorizada(It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidasNumeracionAutorizada.DatosExistentesConsultarNumeraciones);
      _numeracionAutorizadaRepositorio.Setup(x => x.ConsultarNumeracionAutorizada(null!, It.IsAny<string>())).Returns(_respuestasCompartidasNumeracionAutorizada.DatosIdRangoNumeracionNoExisteConsultarNumeraciones);

      _clientesRepositorioApi = new();
      _clientesRepositorioApi.Setup(c => c.ObtenerInformacionClienteEscenarioNull()).Returns(_respuestasCompartidasClientes.ClienteFinalEscenarioNull);

      _clientesRepositorioSql = new();
      _clientesRepositorioSql.Setup(c => c.ConsultarPorId(It.Is<string>(ic => ic != ConstantesCompartidasFacturacion.IdClienteInexistente), It.Is<string>(ie => ie != ConstantesCompartidasFacturacion.IdEmpresaInexistente))).Returns(_respuestasCompartidasClientes.DatosExistentesConsultarClienteRegistrado);
      _clientesRepositorioSql.Setup(c => c.ConsultarPorId(It.Is<string>(ic => ic == ConstantesCompartidasFacturacion.IdClienteDestinatarioNull), It.Is<string>(ie => ie != ConstantesCompartidasFacturacion.IdEmpresaInexistente))).Returns(_respuestasCompartidasClientes.DatosExistentesConsultarClienteRegistradoDestinarioNull);
      _clientesRepositorioSql.Setup(c => c.ConsultarPorId(It.Is<string>(ic => ic == ConstantesCompartidasFacturacion.IdClienteNotificarNo), It.Is<string>(ie => ie != ConstantesCompartidasFacturacion.IdEmpresaInexistente))).Returns(_respuestasCompartidasClientes.DatosExistentesConsultarClienteRegistradoNotificarNo);

      _productosRepositorioSql = new();
      _productosRepositorioSql.Setup(p => p.ConsultarPorId(It.Is<int>(i => i == Convert.ToInt32(ConstantesCompartidasFacturacion.IdProductoExistente)), It.IsAny<int>())).Returns(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto);
      _productosRepositorioSql.Setup(p => p.ConsultarPorId(It.Is<int>(i => i != Convert.ToInt32(ConstantesCompartidasFacturacion.IdProductoExistente)), It.IsAny<int>())).Returns(_respuestasCompartidasProductosSql.DatosNoEncontradosConsultarProducto);

      _empresaAutenticacionRepositorio = new();
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(s => s != ConstantesCompartidasFacturacion.ValorBearerTokenValidoTimeOutRedisCache))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosa);
      _empresaAutenticacionRepositorio.Setup(e => e.ObtenerDatosToken(It.Is<string>(s => s == ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid))).Returns(_respuestasCompartidasEmpresaAutenticacion.EmpresaAutenticacionExitosaBuscarDespuesDeTimeOutRedisCache);

      _reportesRepositorio = new();
      _reportesRepositorio.Setup(r => r.ObtenerCodigoReporteGeneralEnLinea(It.Is<string>(s => s == RespuestasCompartidasReportes.CodigoReporteGeneralSistemaFacturacion || s == null || s == string.Empty))).Returns(RespuestasCompartidasReportes.CodigoReporteGeneralSistemaFacturacion);
      _reportesRepositorio.Setup(r => r.ReporteEnLinea(It.IsAny<SolicitudReporteEnLinea>(), It.IsAny<string>())).Returns(_respuestasCompartidasReportes.RespuestaReporteEnLineaExitosa);
      _reportesRepositorio.Setup(r => r.ReporteEnLinea(It.IsAny<SolicitudReporteEnLinea>(), It.Is<string>(t => t != ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasReportes.RespuestaReporteEnLineaNoAutenticado);
      _reportesRepositorio.Setup(r => r.ReporteEnLinea(It.Is<SolicitudReporteEnLinea>(s => s.Filtros!.FechaInicio == null || s.Filtros.FechaInicio == string.Empty), It.IsAny<string>())).Returns(_respuestasCompartidasReportes.RespuestaReporteEnLineaErrorValidaciones);
      _reportesRepositorio.Setup(r => r.ReporteEnLinea(It.Is<SolicitudReporteEnLinea>(s => s.Filtros!.FechaHasta == null || s.Filtros.FechaHasta == string.Empty), It.IsAny<string>())).Returns(_respuestasCompartidasReportes.RespuestaReporteEnLineaErrorValidaciones);
      _reportesRepositorio.Setup(r => r.ReporteEnLinea(It.Is<SolicitudReporteEnLinea>(s => s.Filtros!.FechaInicio == RespuestasCompartidasReportes.FechaInicioSinDocumentos && s.Filtros.FechaHasta == RespuestasCompartidasReportes.FechaHastaSinDocumentos), It.IsAny<string>())).Returns(_respuestasCompartidasReportes.RespuestaReporteEnLineaSinDocumentos);

      _estadoDocumentosRepositorio = new();
      _estadoDocumentosRepositorio.Setup(r => r.ConsultarDocumentoPorConsecutivoFactura(It.Is<SolicitudConsultarEstadoDocumentoFacturacion>(s => s.Consecutivo == ConstantesCompartidasFacturacion.ConsecutivoDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido || s == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidasEstadoDocumento.DatosExistentesConsultarEstadoDocumento);
      _estadoDocumentosRepositorio.Setup(r => r.ConsultarDocumentoPorConsecutivoFactura(It.Is<SolicitudConsultarEstadoDocumentoFacturacion>(s => s.Consecutivo != ConstantesCompartidasFacturacion.ConsecutivoDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasEstadoDocumento.DatosDocumentoNoExisteConsultarEstadoDocumento);

      _referenciaDocumentoRepositorio = new();
      _referenciaDocumentoRepositorio.Setup(r => r.ConsultarRefereciaDocumentoFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido && s.TipoConsulta == ConstantesCompartidasFacturacion.TipoConsulta1), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido || s == ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache))).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1);
      _referenciaDocumentoRepositorio.Setup(r => r.ConsultarRefereciaDocumentoFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido && s.TipoConsulta == ConstantesCompartidasFacturacion.TipoConsulta0), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2);
      _referenciaDocumentoRepositorio.Setup(r => r.ConsultarRefereciaDocumentoFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice != ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido))).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoNoExisteConsultarReferenciaDocumento);

      _dispositivosAppMovilRepositorioSql = new();
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.IsAny<string>()))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoNoAsociadoAEmpresa);
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente)))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoExistente);
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionNoActiva)))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoNoActiva);
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionNoVigente)))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoNoVigente);
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionAunNoVigente)))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoAunNoVigente);
      _dispositivosAppMovilRepositorioSql.Setup(r => r.ValidarSuscripcionDispositivoPorSerialLogico(It.Is<string>(i => i == ConstantesCompartidasFacturacion.IdEmpresaExistente),
                                                                                                    It.Is<string>(s => s == ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente)))
                                                                                                    .Returns(_respuestasCompartidasDispositivos.SuscripcionDispositivoInexistente);


      _documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
    }

    #region EmitirDocumento

    [Test]
    public void EmitirDocumento_TimeoutRedisCache_DatosTokenvalidos_DevuelveCodigo200()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    [TestCase("2023-02-15 12:50:25", "yyyy-MM-dd HH:mm:ss", "-5", "2023-02-15 07:50:25")]
    [TestCase("2023-02-15 12:50:25", "yyyy-MM-dd HH:mm:ss", "-1", "2023-02-15 11:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "-1", "15/02/2023 11:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "+1", "15/02/2023 13:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy", "+1", "15/02/2023")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "", "15/02/2023 12:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", null, "15/02/2023 12:50:25")]
    [TestCase("2023-02-15 12:50:25", "", "+1", "2023-02-15 13:50:25")]
    [TestCase("2023-02-15 12:50:25", null, "+1", "2023-02-15 13:50:25")]
    [TestCase("2023-02-15 12:50:25", "", "", "2023-02-15 12:50:25")]
    [TestCase("2023-02-15 12:50:25", null, null, "2023-02-15 12:50:25")]
    public void FormatoFechaHoraSolicitudes(string fechaHora, string? formato, string? diferenciaHoras, string fechaEsperada)
    {
      //Arrange
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:Solicitudes:FormatoFechaHora", formato! },
        { "ServiciosFacturacion:Solicitudes:DiferenciaHoras", diferenciaHoras! }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();

      DateTime fechaHoraDT = DateTime.Parse(fechaHora, new CultureInfo("es-ES"));

      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.FormatoFechaHoraSolicitudes(fechaHoraDT);

      //Assert
      Assert.That(respuesta, Is.EqualTo(fechaEsperada));
    }

    [Test]
    [TestCase("2023-02-15 12:50:25", "yyyy-MM-dd HH:mm:ss", "-5", "2023-02-15 07:50:25")]
    [TestCase("2023-02-15 12:50:25", "yyyy-MM-dd HH:mm:ss", "-1", "2023-02-15 11:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "-1", "15/02/2023 11:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "+1", "15/02/2023 13:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy", "+1", "15/02/2023")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", "", "15/02/2023 12:50:25")]
    [TestCase("2023-02-15 12:50:25", "dd/MM/yyyy HH:mm:ss", null, "15/02/2023 12:50:25")]
    [TestCase("2023-02-15 12:50:25", "", "+1", "2023-02-15 13:50:25")]
    [TestCase("2023-02-15 12:50:25", null, "+1", "2023-02-15 13:50:25")]
    [TestCase("2023-02-15 12:50:25", "", "", "2023-02-15 12:50:25")]
    [TestCase("2023-02-15 12:50:25", null, null, "2023-02-15 12:50:25")]
    public void FormatoFechaHoraRespuestas(string fechaHora, string? formato, string? diferenciaHoras, string fechaEsperada)
    {
      //Arrange
      Dictionary<string, string> appSettings = new()
      {
        { "ServiciosFacturacion:Respuestas:FormatoFechaHora", formato! },
        { "ServiciosFacturacion:Respuestas:DiferenciaHoras", diferenciaHoras! }
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();

      DateTime fechaHoraDT = DateTime.Parse(fechaHora, new CultureInfo("es-ES"));

      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.FormatoFechaHoraRespuestas(fechaHoraDT);

      //Assert
      Assert.That(respuesta, Is.EqualTo(fechaEsperada));
    }

    [Test]
    public void EmitirDocumento_BearerTokenValido()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void CompletarDatosFacturaGeneral_DebeAsignarTipoDocumentoYRangoNumeracion_SiExisteNumeracionAutorizada()
    {
      // Arrange
      var factura = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!;

      // Act
      _documentosDominio.CompletarDatosFacturaGeneral(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, "74");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(factura.TipoDocumento, Is.EqualTo("01"));
        Assert.That(factura.RangoNumeracion, Is.EqualTo("PRF-1"));
      });
    }

    [Test]
    public void CompletarDatosFacturaGeneral_NoDebeAsignarTipoDocumentoNiRangoNumeracion_SiNoExisteNumeracionAutorizada()
    {
      // Arrange
      var factura = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoIdRangoNumeracionNull!.Factura!;
      factura.TipoDocumento = "02"; // Valor inicial diferente al esperado
      factura.RangoNumeracion = "0000-0000"; // Valor inicial diferente al esperado

      // Act
      _documentosDominio.CompletarDatosFacturaGeneral(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoIdRangoNumeracionNull, "74");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(factura.TipoDocumento, Is.EqualTo("02"));
        Assert.That(factura.RangoNumeracion, Is.EqualTo("0000-0000"));
      });
    }

    [Test]
    public void CompletarDatosFacturaGeneral_DebeAsignarConsecutivoDocumentoCero_SiEsNulo()
    {
      // Arrange
      var factura = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!;
      factura.ConsecutivoDocumento = null;

      // Act
      _documentosDominio.CompletarDatosFacturaGeneral(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, "74");

      // Assert
      Assert.That(factura.ConsecutivoDocumento, Is.EqualTo("0"));
    }


    [Test]
    public void CompletarDatosFacturaGeneral_NoDebeCambiarConsecutivoDocumento_SiEsNoNulo()
    {
      // Arrange
      var factura = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!;
      factura.ConsecutivoDocumento = "12345";

      // Act
      _documentosDominio.CompletarDatosFacturaGeneral(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, "74");

      // Assert
      Assert.That(factura.ConsecutivoDocumento, Is.EqualTo("12345"));
    }


    [Test]
    public void CompletarDatosFacturaGeneral_DebeCalcularTotalProductos()
    {
      // Arrange
      var factura = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!;

      // Act
      _documentosDominio.CompletarDatosFacturaGeneral(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, "74");

      // Assert
      Assert.That(factura.TotalProductos, Is.EqualTo("2"));
    }

    [Test]
    public void EmitirDocumento_CuandoIdClienteEsNull()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!.Cliente!.IdCliente = null;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_CuandoClienteDestinarioEsNull()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!.Cliente!.IdCliente = ConstantesCompartidasFacturacion.IdClienteDestinatarioNull;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_CuandoClienteNotificarNo()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.Factura!.Cliente!.IdCliente = ConstantesCompartidasFacturacion.IdClienteNotificarNo;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_CuandoIdClienteEsDobleCero_TomarDatosRequest()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoClienteValido!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void CuandoIdProductoExiste_ConsultarInformacionProductos_CompletaInformacionBD()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      documentosDominio.ConsultarInformacionProductos(_solicitudesCompartidasEmitirDocumento.SolicitudDetalleFacturaProductoExistente!, Convert.ToInt32(ConstantesCompartidasFacturacion.IdEmpresaExistente));
      var respuesta = _solicitudesCompartidasEmitirDocumento.SolicitudDetalleFacturaProductoExistente!.FirstOrDefault();

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta!.CodigoProducto, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos!.CodigoProducto));
        Assert.That(respuesta.Descripcion, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.Descripcion));
        Assert.That(respuesta.Nota, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.Nota));
        Assert.That(respuesta.CantidadPorEmpaque, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.CantidadPorEmpaque));
        Assert.That(respuesta.EstandarCodigoProducto, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.EstandarCodigoProducto));
        Assert.That(respuesta.EstandarCodigo, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.EstandarCodigo));
        Assert.That(respuesta.CantidadReal, Is.EqualTo(_respuestasCompartidasProductosSql.DatosExistentesConsultarProducto.Datos.CantidadReal));
      });
    }

    [Test]
    public void EmitirDocumento_TipoApp1_SerialLogicoDispositivoSuscripcionExistente()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "1";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoSuscripcionExistente()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionExistente;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoSuscripcionNoActiva()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionNoActiva;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaSuscripcionDispositivoNoActiva);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoSuscripcionNoVigente()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionNoVigente;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaSuscripcionDispositivoNoVigente);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoSuscripcionAunNoVigente()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionAunNoVigente;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaSuscripcionDispositivoAunNoVigente);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoSuscripcionInexistente()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoSuscripcionInexistente;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaSuscripcionDispositivoInexistente);
    }

    [Test]
    public void EmitirDocumento_TipoApp2_SerialLogicoDispositivoNoAsociadoAEmpresa()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.TipoApp = "2";
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!.SerialLogico = ConstantesCompartidasFacturacion.SerialLogicoDispositivoValido;

      //Act
      var respuesta = documentosDominio.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumento!, ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.BearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaSuscripcionDispositivoNoAsociadoAEmpresa);
    }
    #endregion

    #region ConsultarDocumentos
    [Test]
    [TestCase("2")] //Recepción
    [TestCase("3")] //Nómina
    public void ConsultarDocumentos_SistemaNoDisponible_DevuelveError403(string sistema)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.Sistema = sistema;
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSistemaNoDisponible!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSistemaNoDisponible.Resultado));
        Assert.That(respuesta.Mensaje, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSistemaNoDisponible.Mensaje));
      });
    }

    [Test]
    [TestCase(null)]
    [TestCase("csv")]
    public void ConsultarDocumentos_FormatoRespuestaNoDisponible_DevuelveError403(string? formatoRequerido)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.FormatoRequerido = formatoRequerido;
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible.Resultado));
        Assert.That(respuesta.Mensaje, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible.Mensaje));
      });
    }

    [Test]
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
    public void ConsultarDocumentos_BearerTokenInvalido_DevuelveError401(string? bearerToken)
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, bearerToken!, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosNoAutenticado!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosNoAutenticado.Resultado));
      });
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void ConsultarDocumentos_FiltroFechaInicioInvalida_DevuelveError400(string? fechaInicio)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.Filtros!.FechaInicio = fechaInicio;
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosInvalidos));
        Assert.That(respuesta.Equals, Is.Not.Null);
      });
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void ConsultarDocumentos_FiltroFechaHastaInvalida_DevuelveError400(string? fechaHasta)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.Filtros!.FechaHasta = fechaHasta;
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosInvalidos));
        Assert.That(respuesta.Equals, Is.Not.Null);
      });
    }

    [Test]
    public void ConsultarDocumentos_RengoFechasSinDocumentos_DevuelveCodigo404()
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.Filtros!.FechaInicio = RespuestasCompartidasReportes.FechaInicioSinDocumentos;
      _solicitudesCompartidasDocumentos.solicitudReporteEnLinea.Filtros.FechaHasta = RespuestasCompartidasReportes.FechaHastaSinDocumentos;
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSinDocumentos!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSinDocumentos.Resultado));
        Assert.That(respuesta.Mensaje, Is.EqualTo(_respuestasCompartidasDocumentos.RespuestaConsultarDocumentosSinDocumentos.Mensaje));
      });
    }

    [Test]
    public void ConsultarDocumentos_SolicitudValida_DevuelveDocumentos()
    {
      //Arrange
      DocumentosDominio documentosDominio = new(_configuracion, _redisCacheRepositorio.Object, _empresaAutenticacionRepositorio.Object, _documentosRepositorio.Object, _documentosRepositorioSql.Object, _reportesRepositorio.Object, _clientesRepositorioApi.Object, _clientesRepositorioSql.Object, _productosRepositorioSql.Object, _numeracionAutorizadaRepositorio.Object, _estadoDocumentosRepositorio.Object, _deliveryRepositorio.Object, _referenciaDocumentoRepositorio.Object, _dispositivosAppMovilRepositorioSql.Object, _empresaRepositorio.Object);

      //Act
      var respuesta = documentosDominio.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudReporteEnLinea, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidos));
        Assert.That(respuesta.Documentos, Is.TypeOf<JArray>());
        Assert.That(respuesta.Errores, Is.Null);
      });
    }
    #endregion

    #region ConsultarEstadoDocumento
    [Test]
    public void ConsultarEstadoDocumento_TimeoutRedisCache_DatosTokenvalidos_devuelve_consultaExitosa()
    {
      // Arrange

      var Solicitud = _solicitudesCompartidasConsultarEstadoDocumento.SolicitudConsultarDocumentoValido;

      // Act
      var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidosEstadoDocumento));
        Assert.That(respuesta.Documento, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    public void ConsultarEstadoDocumento_DatosValidos_RespuestaExitosa()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarEstadoDocumento.SolicitudConsultarDocumentoValido;

      // Act
      var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidosEstadoDocumento));
        Assert.That(respuesta.Documento, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    [TestCase("132156")]
    [TestCase("ñalsls")]
    [TestCase("!#=)#$(%")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJleHAiOjE2ODY4NDkyNzgsImlhdCI6MTY4Njg0NTY3OCwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7ImVudGVycHJpc2VUb2tlbiI6ImQ0ODg3MTczODQ3NmY5MDU3NTQ4OGFkYzg0OGQ0MDJlODRlNmZkNTUiLCJlbnRlcHJpc2VJZCI6MTAwLCJlbnRlcnByaXNlTml0IjoiOTAwMzkwMTI2IiwiZW50ZXJwcmlzZXNjaGVtZWlkIjoiMzEiLCJlbnZpcm9tZW50IjowfX19.h47wPelIWhowjx0oI7Li0_MODKGPJ59Vrf6gA81062c")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void CuandoSeHaCerradoSesion_ConsultarInformacion_Devuelve401(string? bearerToken)
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarEstadoDocumento.SolicitudConsultarDocumentoValido;

      // Act
      var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(Solicitud, bearerToken!, bearerToken!);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasEstadoDocumento.SeHaCerradoSesionConsultarUsuario);
    }

    [Test]
    public void ConsultarEstadoDocumento_NroDocumentoInvalido_RespuestaError102()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarEstadoDocumento.SolicitudConsultarDocumentoInvalido;

      // Act
      var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoNroDocumentoInvalido));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeNroDocumentoInvalido));
        Assert.That(respuesta.Documento, Is.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    public void ConsultarEstadoDocumento_CuandoPlataformaNoDisponible_DevuelveError403()
    {
      //Arrange
      var Solicitud = _solicitudesCompartidasConsultarEstadoDocumento.SolicitudConsultarPlataformaNoDisponible;

      //Act
      var respuesta = _documentosDominio.ConsultarDocumentoPorConsecutivoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajePlataformaNoDisponible));
        Assert.That(respuesta.Documento, Is.Null);
      });
    }

    #endregion

    #region ConsultarReferenciaDocumento

    [Test]
    public void ConsultarReferenciaDocumento1_TimeoutRedisCache_DatosTokenvalidos_DevuelveConsultaExitosa()
    {
      //Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarDocumentoValido1;
      //Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidosConsultarReferencia));
        Assert.That(respuesta.Message, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeExitosoConsultarReferencia1));
        Assert.That(respuesta.Rango, Is.Not.Null);
        Assert.That(respuesta.Sucursales, Is.Not.Null);
        Assert.That(respuesta.Factura, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }
    [Test]
    public void ConsultarReferenciaDocumento1_DatosValidos_RespuestaExitosa()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarDocumentoValido1;

      // Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidosConsultarReferencia));
        Assert.That(respuesta.Message, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeExitosoConsultarReferencia1));
        Assert.That(respuesta.Rango, Is.Not.Null);
        Assert.That(respuesta.Sucursales, Is.Not.Null);
        Assert.That(respuesta.Factura, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    public void ConsultarReferenciaDocumento0_DatosValidos_RespuestaExitosa()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarDocumentoValido2;

      // Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidosConsultarReferencia));
        Assert.That(respuesta.Message, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeExitosoConsultarReferencia2));
        Assert.That(respuesta.Rango, Is.Not.Null);
        Assert.That(respuesta.Sucursales, Is.Not.Null);
        Assert.That(respuesta.Factura, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    [TestCase("132156")]
    [TestCase("ñalsls")]
    [TestCase("!#=)#$(%")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJleHAiOjE2ODY4NDkyNzgsImlhdCI6MTY4Njg0NTY3OCwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7ImVudGVycHJpc2VUb2tlbiI6ImQ0ODg3MTczODQ3NmY5MDU3NTQ4OGFkYzg0OGQ0MDJlODRlNmZkNTUiLCJlbnRlcHJpc2VJZCI6MTAwLCJlbnRlcnByaXNlTml0IjoiOTAwMzkwMTI2IiwiZW50ZXJwcmlzZXNjaGVtZWlkIjoiMzEiLCJlbnZpcm9tZW50IjowfX19.h47wPelIWhowjx0oI7Li0_MODKGPJ59Vrf6gA81062c")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void CuandoSeHaCerradoSesion_ConsultarReferenciaDocumento_Devuelve401(string? bearerToken)
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarDocumentoValido1;

      // Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, bearerToken!, bearerToken!);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasConsultarReferenciaDocumento.SeHaCerradoSesionConsultarUsuario);
    }

    [Test]
    public void ConsultarReferenciaDocumento_NroDocumentoInvalido_RespuestaError102()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarDocumentoInvalido;

      // Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoReferenciaDocumentoInvalido));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido));
        Assert.That(respuesta.Message, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDocumentoNoExiste));
        Assert.That(respuesta.Rango, Is.Null);
        Assert.That(respuesta.Sucursales, Is.Null);
        Assert.That(respuesta.Factura, Is.Null);
        Assert.That(respuesta.Errores, Is.Null);

      });
    }

    [Test]
    public void ConsultarReferenciaDocumento_CuandoPlataformaNoDisponible_DevuelveError403()
    {
      //Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumento.SolicitudConsultarPlataformaNoDisponible;

      //Act
      var respuesta = _documentosDominio.ConsultarReferenciaDocumentosFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible));
        Assert.That(respuesta.Message, Is.EqualTo(ConstantesCompartidasFacturacion.MensajePlataformaNoDisponible));
        Assert.That(respuesta.Rango, Is.Null);
        Assert.That(respuesta.Sucursales, Is.Null);
        Assert.That(respuesta.Factura, Is.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }
    #endregion

    #region Consultar Monto FacturaPos

    [Test]
    public void ConsultarMontoFacturaPos_TimeoutRedisCache_DatosTokenvalidos_DevuelveConsultaExitosa()
    {
      //Arrange

      //Act
      var respuesta = _documentosDominio.ConsultarMontoFacturaPos(ConstantesCompartidasFacturacion.BearerTokenTimeOutRedisCache, ConstantesCompartidasFacturacion.ValorBearerTokenValidoConsultarPorid);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaConsultarMontoFacturaPosExitosa);
    }

    [Test]
    public void ConsultarMontoFacturaPos_BearerTokenValido_DevuelveMontoFacturaPos()
    {
      //Arrange

      //Act
      var respuesta = _documentosDominio.ConsultarMontoFacturaPos(ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaConsultarMontoFacturaPosExitosa);
    }

    [Test]
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
    public void ConsultarMontoFacturaPos_BearerTokenInValido_DevuelveError401(string? bearerToken)
    {
      //Arrange

      //Act
      var respuesta = _documentosDominio.ConsultarMontoFacturaPos(ConstantesCompartidasFacturacion.ValorBearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasDocumentos.RespuestaConsultarMontoFacturaPosNoAutenticado);
    }
    #endregion
  }
}
