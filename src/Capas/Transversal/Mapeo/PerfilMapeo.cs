using Aplicacion.Dto;
using Aplicacion.Dto.Dispositivos;
using Aplicacion.Dto.Empresas;
using Aplicacion.Dto.EstadoDocumento;
using Aplicacion.Dto.Respuestas;
using Aplicacion.Dto.Solicitudes;
using Aplicacion.Dto.Solicitudes.Dispositivos;
using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using Aplicacion.Entidad.Documentos;
using Aplicacion.Entidad.ReferenciaDocumento;
using Aplicacion.Entidad.Respuestas;
using Aplicacion.Entidad.Solicitudes;
using AutoMapper;
using Dominio.Entidad;
using Dominio.Entidad.Dispositivos;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Empresas;
using Dominio.Entidad.EstadoDocumento;

using Dominio.Entidad.ReferenciaDocumento;
using Dominio.Entidad.Reportes;
using Dominio.Entidad.Respuestas;
using Dominio.Entidad.Solicitudes;
using Dominio.Entidad.Solicitudes.FiltrosSolicitudes;
using static Dominio.Entidad.Autenticacion;

namespace Transversal.Mapeo
{
  public class PerfilMapeo : Profile
  {
    public PerfilMapeo()
    {
      AllowNullCollections = true;

      CreateMap<UsuarioAutenticacion, UsuarioAutenticacionDto>().ReverseMap();
      CreateMap<RespuestaIniciarSesion, RespuestaIniciarSesionDto>().ReverseMap();
      CreateMap<RespuestaCerrarSesion, RespuestaCerrarSesionDto>().ReverseMap();

      CreateMap<SolicitudEmitirDocumento, SolicitudEmitirDocumentoDto>().ReverseMap();
      CreateMap<RespuestaEmitirDocumento, RespuestaEmitirDocumentoDto>().ReverseMap();

      CreateMap<SolicitudConsultarFacturacion, SolicitudConsultarFacturacionDto>().ReverseMap();
      CreateMap<RespuestaConsultarNumeraciones, RespuestaConsultarNumeracionesDto>().ReverseMap();
      CreateMap<NumeracionFacturacion, NumeracionFacturacionDto>().ReverseMap();

      CreateMap<EmpresaInfo, EmpresaAutenticacion>()
        .ForMember(d => d.IdEmpresa, opciones => opciones.MapFrom(o => o.entepriseId))
        .ForMember(d => d.TokenEmpresa, opciones => opciones.MapFrom(o => o.enterpriseToken))
        .ForMember(d => d.NitEmpresa, opciones => opciones.MapFrom(o => o.enterpriseNit))
        .ForMember(d => d.IdEsquemaEmpresa, opciones => opciones.MapFrom(o => o.enterpriseschemeid))
        .ForMember(d => d.Entorno, opciones => opciones.MapFrom(o => o.enviroment))
        .ReverseMap();

      CreateMap<EmpresaAutenticacion, EmpresaAutenticacionDto>().ReverseMap();


      CreateMap<SolicitudConsultarDocumentosDto, SolicitudReporteEnLinea>().ReverseMap();
      CreateMap<RespuestaConsultarDocumentos, RespuestaConsultarDocumentosDto>().ReverseMap();
      CreateMap<Filtros, FiltrosConsultarDocumentosDto>().ReverseMap()
        .ForMember(dto => dto.NumeracionFacturaInical, opciones => opciones.MapFrom(entidad => entidad.NumeracionFacturaInicial));

      CreateMap<ResumenFolios, ResumenFoliosDto>().ReverseMap();
      CreateMap<RespuestaConsultarResumenFolios, RespuestaConsultarResumenFoliosDto>().ReverseMap();

      CreateMap<SolicitudConsultarCatalogo, SolicitudConsultarCatalogoDto>().ReverseMap();
      CreateMap<RespuestaConsultarCatalogo, RespuestaConsultarCatalogoDto>().ReverseMap();

      CreateMap<RespuestaConsultarCertificadoFacturacion, RespuestaConsultarCertificadoFacturacionDto>().ReverseMap();
      CreateMap<CertificadoFacturacion, CertificadoFacturacionDto>().ReverseMap();

      CreateMap<RespuestaConsultarUsuarioFacturacion, RespuestaConsultarUsuarioFacturacionDto>().ReverseMap();
      CreateMap<UsuarioFacturacion, UsuarioFacturacionDto>().ReverseMap();

      CreateMap<SolicitudConsultarEstadoDocumentoFacturacionDto, SolicitudConsultarEstadoDocumentoFacturacion>().ReverseMap();
      CreateMap<RespuestaConsultarEstadoDocumentoDto, RespuestaConsultarEstadoDocumento>().ReverseMap();
      CreateMap<EstadoDocumentoFacturacionDto, EstadoDocumentoFacturacion>().ReverseMap();
      CreateMap<ExtensibleDto, Evento>().ReverseMap();
      CreateMap<HistorialEntregaDto, HistorialEntrega>().ReverseMap();

      CreateMap<SolicitudEnviarCorreoIndividual, SolicitudEnviarCorreoIndividualDto>()
        .ForMember(dto => dto.Documento, opciones => opciones.MapFrom(entidad => entidad.Documento))
        .ForMember(dto => dto.Correo, opciones => opciones.MapFrom(entidad => entidad.Email))
        .ForMember(dto => dto.TipoDocumento, opciones => opciones.MapFrom(entidad => entidad.InvoiceType))
        .ForMember(dto => dto.Adjunto, opciones => opciones.MapFrom(entidad => entidad.Adjunto))
        .ForMember(dto => dto.TieneAdjuntos, opciones => opciones.MapFrom(entidad => entidad.AttachementTrue))
        .ForMember(dto => dto.Ruta, opciones => opciones.MapFrom(entidad => entidad.Ruta))
        .ForMember(dto => dto.Formato, opciones => opciones.MapFrom(entidad => entidad.Formato))
        .ForMember(dto => dto.NombreParaMostrar, opciones => opciones.MapFrom(entidad => entidad.Nombre_Display))
        .ForMember(dto => dto.NombreArchivo, opciones => opciones.MapFrom(entidad => entidad.NombreArchivo))
        .ForMember(dto => dto.Tipo, opciones => opciones.MapFrom(entidad => entidad.Type))
        .ReverseMap();
      CreateMap<RespuestaEnviarCorreoIndividual, RespuestaEnviarCorreoIndividualDto>().ReverseMap();

      CreateMap<SolicitudConsultarReferenciaDocumentoFacturacionDto, SolicitudConsultarReferenciaDocumentoFacturacion>().ReverseMap();
      CreateMap<RespuestaConsultarReferenciaDocumentoDto, RespuestaConsultarReferenciaDocumento>()
        .ForMember(d => d.Message, opciones => opciones.MapFrom(o => o.Mensaje))
        .ReverseMap();
      CreateMap<RangoDto, Rango>().ReverseMap();
      CreateMap<ListaRetencionesDto, ListaRetenciones>().ReverseMap();
      CreateMap<SucursalesDto, Sucursales>().ReverseMap();
      CreateMap<FacturaGeneralDto, FacturaGeneral>().ReverseMap();
      CreateMap<AnticiposDto, Anticipos>().ReverseMap();
      CreateMap<AutorizadoDto, Autorizado>().ReverseMap();
      CreateMap<BeneficiarioSaludDto, BeneficiarioSalud>().ReverseMap();
      CreateMap<CargosDescuentosDto, CargosDescuentos>().ReverseMap();
      CreateMap<ClienteDto, Cliente>().ReverseMap();
      CreateMap<CondicionDePagoDto, CondicionDePago>().ReverseMap();
      CreateMap<CoordenadasDto, Coordenadas>().ReverseMap();
      CreateMap<DatosDelTransportistaDto, DatosDelTransportista>().ReverseMap();
      CreateMap<DatosPacienteSaludDto, DatosPacienteSalud>().ReverseMap();
      CreateMap<DestinatarioDto, Destinatario>().ReverseMap();
      CreateMap<DireccionBaseReferenciaDto, Dominio.Entidad.Documentos.DireccionBase>().ReverseMap();
      CreateMap<DocumentoReferenciadoDto, DocumentoReferenciado>().ReverseMap();
      CreateMap<EntregaDto, Entrega>().ReverseMap();
      CreateMap<ExtensibleReferenciaDto, Dominio.Entidad.Documentos.Extensible>().ReverseMap();
      CreateMap<ExtrasDto, Extras>().ReverseMap();
      CreateMap<FacturaDetalleDto, FacturaDetalle>().ReverseMap();
      CreateMap<FacturaImpuestosDto, FacturaImpuestos>().ReverseMap();
      CreateMap<GeneralSaludDto, GeneralSalud>().ReverseMap();
      CreateMap<ImpuestosTotalesDto, ImpuestosTotales>().ReverseMap();
      CreateMap<InformacionLegalDto, InformacionLegal>().ReverseMap();
      CreateMap<LineaInformacionAdicionalDto, LineaInformacionAdicional>().ReverseMap();
      CreateMap<MediosDePagoDto, MediosDePago>().ReverseMap();
      CreateMap<ObligacionesBaseDto, ObligacionesBase>().ReverseMap();
      CreateMap<OrdenDeCompraDto, OrdenDeCompra>().ReverseMap();
      CreateMap<SectorSaludDto, SectorSalud>().ReverseMap();
      CreateMap<TasaDeCambioAlternativaDto, TasaDeCambioAlternativa>().ReverseMap();
      CreateMap<TasaDeCambioBaseDto, TasaDeCambioBase>().ReverseMap();
      CreateMap<TerminosDeEntregaDto, TerminosDeEntrega>().ReverseMap();
      CreateMap<TributosDto, Tributos>().ReverseMap();


      CreateMap<SolicitudValidarClaveSecretaDto, SolicitudValidarClaveSecreta>().ReverseMap();
      CreateMap<RespuestaValidarClaveSecretaDto, RespuestaValidarClaveSecreta>().ReverseMap();

      CreateMap<FiltrosTotalDocumentos, FiltrosTotalDocumentosDto>().ReverseMap();


      CreateMap<RespuestaAgendarReporteProgramadoFacturacion, RespuestaAgendarReporteProgramadoFacturacionDto>().ReverseMap();

      CreateMap<RespuestaConsultarMontoFacturaPos, RespuestaConsultarMontoFacturaPosDto>().ReverseMap();

      CreateMap<DispositivoDto, Dispositivo>().ReverseMap();

      CreateMap<RespuestaSeleccionarEstablecimiento, RespuestaSeleccionarEstablecimientoDto>().ReverseMap();
      CreateMap<SolicitudSeleccionarEstablecimiento, SolicitudSeleccionarEstablecimientoDto>().ReverseMap();

      CreateMap<RespuestaSeleccionarSecuencial, RespuestaSeleccionarSecuencialDto>().ReverseMap();
      CreateMap<SolicitudSeleccionarSecuencial, SolicitudSeleccionarSecuencialDto>().ReverseMap();
      CreateMap<SolicitudAsociarAlias, SolicitudAsociarAliasDto>().ReverseMap();
    }
  }
}