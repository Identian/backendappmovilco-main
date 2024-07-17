using Aplicacion.Dto.Solicitudes;
using Aplicacion.Principal;
using AutoMapper;
using BackendAppMovilCOTest.Capas.Compartida;
using BackendAppMovilCOTest.Capas.Compartida.Respuestas;
using BackendAppMovilCOTest.Capas.Compartida.Solicitudes;
using Dominio.Entidad.Solicitudes;
using Dominio.Interfaz;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Transversal.Comun.Utils;
using Transversal.Mapeo;

namespace BackendAppMovilCOTest.Capas.Aplicacion.Principal
{
  public class DocumentosAplicacionTest
  {
    private Mock<IDocumentosDominio> _documentosDominio = new();
    private readonly RespuestasCompartidasDocumentos _respuestasCompartidas = new();
    private readonly RespuestasCompartidasEstadoDocumento _respuestasCompartidasEstadoDocumentoDto = new();
    private readonly RespuestasCompartidasReferenciaDocumento _respuestasCompartidasConsultarReferenciaDocumento = new();
    private readonly SolicitudesCompartidasDocumentos _solicitudesCompartidasDocumentos = new();
    private SolicitudesCompartidasEmitirDocumento _solicitudesCompartidasEmitirDocumento = new();
    private SolicitudEmitirDocumentoDto _solicitudEmitirDocumentoDto;
    private readonly SolicitudesCompartidasConsultarEstadoDocumento _solicitudesCompartidasConsultarEstadoDocumentoDto = new();
    private readonly SolicitudesCompartidasConsultarReferenciaDocumento _solicitudesCompartidasConsultarReferenciaDocumentoDto = new();
    private Mapper _mapeador;
    private IConfiguration _configuracion;
    private DocumentosAplicacion _documentosAplicacion;

    [SetUp]
    public void SetUp()
    {
      //Arrange general
      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new PerfilMapeo()));
      _mapeador = new Mapper(mappingConfig);

      Dictionary<string, string> appSettings = new()
      {
        { "ConsultaReferenciaDocumentoTipo:Exitosos", "1" },
        { "ConsultaReferenciaDocumentoTipo:Rechacazdos", "0" },
        { "ServiciosFacturacion:EmisionRest:ApiEnviar:DevolverXML", "0" },
      };
      _configuracion = new ConfigurationBuilder().AddInMemoryCollection(appSettings!).Build();

      _respuestasCompartidas.InicializarRespuestas();
      _solicitudesCompartidasDocumentos.InicializarSolicitudes();
      _documentosDominio = new();
      _documentosDominio.Setup(d => d.EmitirDocumento(It.IsAny<SolicitudEmitirDocumento>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaDocumentoEmitidoExitosamente!);
      _documentosDominio.Setup(d => d.ConsultarDocumentos(It.IsAny<SolicitudReporteEnLinea>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarDocumentosExitoso!);
      _documentosDominio.Setup(d => d.ConsultarDocumentos(It.Is<SolicitudReporteEnLinea>(s => s.Sistema != RespuestasCompartidasDocumentos.SistemaDisponible && s.Sistema != null), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarDocumentosSistemaNoDisponible!);
      _documentosDominio.Setup(d => d.ConsultarDocumentos(It.Is<SolicitudReporteEnLinea>(s => s.FormatoRequerido != RespuestasCompartidasDocumentos.FormatoRespuestaDisponible), It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible!);
      _documentosDominio.Setup(d => d.ConsultarDocumentos(It.IsAny<SolicitudReporteEnLinea>(), It.Is<string>(t => t != ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarDocumentosNoAutenticado!);
      _documentosDominio.Setup(d => d.FormatoFechaHoraRespuestas(It.IsAny<DateTime>())).Returns(RespuestasCompartidasDocumentos.FechaRespuestaEmitirDocumento);
      _documentosDominio.Setup(r => r.ConsultarDocumentoPorConsecutivoFactura(It.Is<SolicitudConsultarEstadoDocumentoFacturacion>(s => s.Consecutivo == ConstantesCompartidasFacturacion.ConsecutivoDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasEstadoDocumentoDto.DatosExistentesConsultarEstadoDocumento);
      _documentosDominio.Setup(r => r.ConsultarDocumentoPorConsecutivoFactura(It.Is<SolicitudConsultarEstadoDocumentoFacturacion>(s => s.Consecutivo != ConstantesCompartidasFacturacion.ConsecutivoDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasEstadoDocumentoDto.DatosDocumentoNoExisteConsultarEstadoDocumento);
      _documentosDominio.Setup(r => r.ConsultarReferenciaDocumentosFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido && s.TipoConsulta == ConstantesCompartidasFacturacion.TipoConsulta1), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoExisteConsultarReferenciaDocumentoConsulta1);
      _documentosDominio.Setup(r => r.ConsultarReferenciaDocumentosFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido && s.TipoConsulta == ConstantesCompartidasFacturacion.TipoConsulta0), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoExisteConsultarReferenciaDocumentoConsulta2);
      _documentosDominio.Setup(r => r.ConsultarReferenciaDocumentosFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice != ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasConsultarReferenciaDocumento.DatosDocumentoNoExisteConsultarReferenciaDocumento);
      _documentosDominio.Setup(r => r.ConsultarReferenciaDocumentosFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido && s.Plataforma == ConstantesCompartidasFacturacion.PlataformaNoDisponible), It.Is<string>(s => s == ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasConsultarReferenciaDocumento.PlataformaNoDisponibleConsultarReferenciaDocumento);
      _documentosDominio.Setup(r => r.ConsultarReferenciaDocumentosFactura(It.Is<SolicitudConsultarReferenciaDocumentoFacturacion>(s => s.IdInvoice == ConstantesCompartidasFacturacion.IdInvoiceDocumentoValido), It.Is<string>(s => s != ConstantesCompartidasFacturacion.BearerTokenValido), It.IsAny<string>())).Returns(_respuestasCompartidasConsultarReferenciaDocumento.SeHaCerradoSesionConsultarUsuario);

      _documentosDominio.Setup(d => d.ConsultarMontoFacturaPos(It.IsAny<string>(), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarMontoFacturaPosNoAutenticado!);
      _documentosDominio.Setup(d => d.ConsultarMontoFacturaPos(It.Is<string>(s => s.Equals(ConstantesCompartidasFacturacion.BearerTokenValido)), It.IsAny<string>())).Returns(_respuestasCompartidas.RespuestaConsultarMontoFacturaPosExitosa!);

      _solicitudesCompartidasEmitirDocumento = new();
      _solicitudEmitirDocumentoDto = _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!;

      _documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);
    }

    #region EmitirDocumento
    [Test]
    [TestCase("1")]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    public void EmitirDocumento_IdRangoNumeracion_Valido(string idRangoNumeracion)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.IdRangoNumeracion = idRangoNumeracion;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("1.2")]
    [TestCase("2,3")]
    [TestCase("a")]
    [TestCase("Z")]
    [TestCase("*")]
    public void EmitirDocumento_IdRangoNumeracion_NoValido(string? idRangoNumeracion)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.IdRangoNumeracion = idRangoNumeracion;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(RespuestasCompartidasDocumentos.CodigoErrorValidaciones));
        Assert.That(respuesta.Resultado, Is.EqualTo(RespuestasCompartidasDocumentos.ResultadoError));
        Assert.That(respuesta.Mensaje, Is.EqualTo(RespuestasCompartidasDocumentos.MensajeErrorValidaciones));
        Assert.That(respuesta.MensajesValidacion, Is.Not.Null);
      });
    }

    [Test]
    [TestCase("1")]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    [TestCase(null)]
    public void EmitirDocumento_IdCliente_Valido(string? idCliente)
    {
      //Arrange
      _solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto!.Factura!.Cliente!.IdCliente = idCliente;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudesCompartidasEmitirDocumento.SolicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    public void EmitirDocumento_IdCliente_Valido_doblecero()
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.Cliente!.IdCliente = "00";
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaDocumentoEmitidoExitosamente);
    }


    [Test]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("1.2")]
    [TestCase("2,3")]
    [TestCase("a")]
    [TestCase("Z")]
    [TestCase("*")]
    public void EmitirDocumento_IdCliente_NoValido(string? idCliente)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.Cliente!.IdCliente = idCliente;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(RespuestasCompartidasDocumentos.CodigoErrorValidaciones));
        Assert.That(respuesta.Resultado, Is.EqualTo(RespuestasCompartidasDocumentos.ResultadoError));
        Assert.That(respuesta.Mensaje, Is.EqualTo(RespuestasCompartidasDocumentos.MensajeErrorValidaciones));
        Assert.That(respuesta.MensajesValidacion, Is.Not.Null);
      });
    }

    [Test]
    [TestCase("1")]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    public void EmitirDocumento_IdProducto_Valido(string? idProducto)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.DetalleDeFactura!.FirstOrDefault()!.IdProducto = idProducto;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaDocumentoEmitidoExitosamente);
    }

    [Test]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("1.2")]
    [TestCase("2,3")]
    [TestCase("a")]
    [TestCase("Z")]
    [TestCase("*")]
    public void EmitirDocumento_IdProducto_NoValido(string? idProducto)
    {
      //Arrange
      _solicitudEmitirDocumentoDto.Factura!.DetalleDeFactura!.FirstOrDefault()!.IdProducto = idProducto;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.EmitirDocumento(_solicitudEmitirDocumentoDto, "bearer asdfldñfj", "asdfldñfj");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(RespuestasCompartidasDocumentos.CodigoErrorValidaciones));
        Assert.That(respuesta.Resultado, Is.EqualTo(RespuestasCompartidasDocumentos.ResultadoError));
        Assert.That(respuesta.Mensaje, Is.EqualTo(RespuestasCompartidasDocumentos.MensajeErrorValidaciones));
        Assert.That(respuesta.MensajesValidacion, Is.Not.Null);
      });
    }
    #endregion

    #region ConsultarDocumentos

    [Test]
    public void ConsultarDocumentos_SolicitudValida_DevuelveDocumentos()
    {
      //Arrange
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

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

    [Test]
    [TestCase(null)] //Facturación (valor por defecto)
    [TestCase("1")] //Facturación
    public void ConsultarDocumentos_Sistema_EsValido(string? sistema)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.Sistema = sistema;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidos));
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51)]
    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("1.2")]
    [TestCase("2,3")]
    [TestCase("a")]
    [TestCase("Z")]
    [TestCase("*")]
    [TestCase("")]
    [TestCase(" ")]
    public void ConsultarDocumentos_Sistema_NoEsValido(string sistema)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.Sistema = sistema;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosInvalidos));
        Assert.That(respuesta.Errores, Is.Not.Null);
      });
    }

    [Test]
    [TestCase("json")]
    public void ConsultarDocumentos_FormatoRequerido_EsValido(string formatoRequerido)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.FormatoRequerido = formatoRequerido;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosValidos));
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    [Test]
    [TestCase("Csv")]
    [TestCase("Json")]
    [TestCase("CSV")]
    [TestCase("JSON")]
    [TestCase("txt")]
    [TestCase("xls")]
    [TestCase("Excel")]
    [TestCase("rpt")]
    [TestCase(UtilidadesCadenas.Pruebas.Digitos)]
    [TestCase(UtilidadesCadenas.Pruebas.MaximoValorEntero)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConValorMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.EnteroConAnchoMayorAlMaximo)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMinusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.AlfabetoMayusculas)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho50)]
    [TestCase(UtilidadesCadenas.Pruebas.TextoConAncho51)]
    [TestCase("-1")]
    [TestCase("0")]
    [TestCase("1")]
    [TestCase("1.2")]
    [TestCase("2,3")]
    [TestCase("a")]
    [TestCase("Z")]
    [TestCase("*")]
    [TestCase("")]
    [TestCase(" ")]
    public void ConsultarDocumentos_FormatoRequerido_NoEsValido(string formatoRequerido)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.FormatoRequerido = formatoRequerido;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosInvalidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosInvalidos));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDatosInvalidos));
        Assert.That(respuesta.Errores, Is.Not.Null);
      });
    }

    [Test]
    [TestCase("2")] //Recepción
    [TestCase("3")] //Nómina
    public void ConsultarDocumentos_SistemaNoDisponible_DevuelveError403(string sistema)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.Sistema = sistema;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosSistemaNoDisponible!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosSistemaNoDisponible!.Resultado));
        Assert.That(respuesta.Mensaje, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosSistemaNoDisponible.Mensaje));
      });
    }

    [Test]
    [TestCase(null)]
    [TestCase("csv")]
    public void ConsultarDocumentos_FormatoRespuestaNoDisponible_DevuelveError403(string? formatoRequerido)
    {
      //Arrange
      _solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto.FormatoRequerido = formatoRequerido;
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible.Resultado));
        Assert.That(respuesta.Mensaje, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosFormatoRespuestaNoDisponible.Mensaje));
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
      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentos(_solicitudesCompartidasDocumentos.solicitudConsultarDocumentosDto, bearerToken!, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosNoAutenticado!.Codigo));
        Assert.That(respuesta.Resultado, Is.EqualTo(_respuestasCompartidas.RespuestaConsultarDocumentosNoAutenticado.Resultado));
      });
    }
    #endregion

    #region ConsultarEstadoDocumento

    [Test]
    public void ConsultarEstadoDocumento_ConsecutivoDocumentoValido_RespuestaExitosa()
    {
      //Arrange

      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentoPorConsecutivoFactura(_solicitudesCompartidasConsultarEstadoDocumentoDto.SolicitudConsultarDocumentoValidoDto, ConstantesCompartidasFacturacion.BearerTokenValido, "asdfldñfj");

      //Assert
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
    public void ConsultarEstadoDocumento_ConsecutivoDocumentoInvalido_RespuestaError102()
    {
      //Arrange

      DocumentosAplicacion documentosAplicacion = new(_configuracion, _documentosDominio.Object, _mapeador);

      //Act
      var respuesta = documentosAplicacion.ConsultarDocumentoPorConsecutivoFactura(_solicitudesCompartidasConsultarEstadoDocumentoDto.SolicitudConsultarDocumentoInvalidoDto, ConstantesCompartidasFacturacion.BearerTokenValido, "asdfldñfj");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoNroDocumentoInvalido));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeNroDocumentoInvalido));
        Assert.That(respuesta.Documento, Is.Not.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }

    #endregion

    #region ConsultarReferenciaDocumento
    [Test]
    public void ConsultarReferenciaDocumento1_DatosValidos_RespuestaExitosa()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumentoDto.SolicitudConsultarDocumentoValido1Dto;

      // Act
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidosConsultarReferencia));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeExitosoConsultarReferencia1));
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
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumentoDto.SolicitudConsultarDocumentoValido2Dto;

      // Act
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoDatosValidos));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoDatosValidosConsultarReferencia));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeExitosoConsultarReferencia2));
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
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumentoDto.SolicitudConsultarDocumentoValido1Dto;

      // Act
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(Solicitud, bearerToken!, bearerToken!);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidasConsultarReferenciaDocumento.SeHaCerradoSesionConsultarUsuarioDto);
    }

    [Test]
    public void ConsultarReferenciaDocumento_NroDocumentoInvalido_RespuestaError102()
    {
      // Arrange
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumentoDto.SolicitudConsultarDocumentoInvalidoDto;

      // Act
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoReferenciaDocumentoInvalido));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoNroDocumentoInvalido));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajeDocumentoNoExiste));
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
      var Solicitud = _solicitudesCompartidasConsultarReferenciaDocumentoDto.SolicitudConsultarPlataformaNoDisponibleDto;

      // Act
      var respuesta = _documentosAplicacion.ConsultarReferenciaDocumentoFactura(Solicitud, ConstantesCompartidasFacturacion.BearerTokenValido, "token");

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(respuesta.Codigo, Is.EqualTo(ConstantesCompartidasFacturacion.CodigoPlataformaNoDisponible));
        Assert.That(respuesta.Resultado, Is.EqualTo(ConstantesCompartidasFacturacion.ResultadoPlataformaNoDisponible));
        Assert.That(respuesta.Mensaje, Is.EqualTo(ConstantesCompartidasFacturacion.MensajePlataformaNoDisponible));
        Assert.That(respuesta.Rango, Is.Null);
        Assert.That(respuesta.Sucursales, Is.Null);
        Assert.That(respuesta.Factura, Is.Null);
        Assert.That(respuesta.Errores, Is.Null);
      });
    }
    #endregion

    #region Consultar Monto FacturaPos
    [Test]
    public void ConsultarMontoFacturaPos_BearerTokenValido_DevuelveMontoFacturaPos()
    {
      //Arrange

      //Act
      var respuesta = _documentosAplicacion.ConsultarMontoFacturaPos(ConstantesCompartidasFacturacion.BearerTokenValido, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaConsultarMontoFacturaPosExitosaDto);
    }

    [Test]
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
    public void ConsultarMontoFacturaPos_BearerTokenInValido_DevuelveError401(string bearerToken)
    {
      //Arrange

      //Act
      var respuesta = _documentosAplicacion.ConsultarMontoFacturaPos(bearerToken!, ConstantesCompartidasFacturacion.ValorBearerTokenValido);

      //Assert
      respuesta.Should().BeEquivalentTo(_respuestasCompartidas.RespuestaConsultarMontoFacturaPosNoAutenticadoDto);
    }
    #endregion
  }
}
