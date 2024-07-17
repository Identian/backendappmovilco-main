using Aplicacion.Dto.Solicitudes;
using Aplicacion.Entidad.Documentos;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Solicitudes;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class SolicitudesCompartidasEmitirDocumento
  {
    public SolicitudEmitirDocumentoDto? SolicitudEmitirDocumentoDto { get; set; }
    public SolicitudEmitirDocumentoDto? SolicitudEmitirDocumentoIdProductoNullDto { get; set; }
    public SolicitudEmitirDocumento? SolicitudEmitirDocumento { get; set; }
    public SolicitudEmitirDocumento? SolicitudEmitirDocumentoClienteValido { get; set; }
    public SolicitudEmitirDocumento? SolicitudEmitirDocumentoIdRangoNumeracionNull { get; set; }
    public IEnumerable<FacturaDetalle>? SolicitudDetalleFacturaProductoExistente { get; set; }

    public SolicitudesCompartidasEmitirDocumento()
    {
      InicializarSolicitudes();
    }

    public void InicializarSolicitudes()
    {
      #region Solicitudes Dto
      SolicitudEmitirDocumentoDto = new()
      {
        Factura = new()
        {
          IdRangoNumeracion = "123456789",
          Cliente = new()
          {
            IdCliente = "123456789",
            TipoPersona = "1",
            TipoIdentificacion = "2",
            ResponsabilidadesRut = new List<ObligacionesBaseDto>
            {
              new ObligacionesBaseDto
              {
               Obligaciones = "R-99-PN",
               Regimen = "48"
              }
            },
            NumeroDocumento = "2",
            Notificar = "no",
            NombreRazonSocial = "NN",
            DetallesTributarios = new List<TributosDto>
            { new TributosDto
             {
              CodigoImpuesto = "1"
             }
            }
          },
          DetalleDeFactura = new List<FacturaDetalleDto>()
          {
            new FacturaDetalleDto
            {
              IdProducto = "123456789",
              CantidadUnidades = "2",
              UnidadMedida = "WSD",
              PrecioVentaUnitario = "1300.00",
              PrecioTotalSinImpuestos = "1100.00",
              CantidadRealUnidadMedida = "2P",
              ImpuestosDetalles = new List<FacturaImpuestosDto>
              {
                new FacturaImpuestosDto
                {
                  CodigoTOTALImp ="01",
                  BaseImponibleTOTALImp = "1003.00",
                  ValorTOTALImp = "190.57"
                }
              },
              ImpuestosTotales = new List<ImpuestosTotalesDto>
              {
                new ImpuestosTotalesDto
                {
                  CodigoTOTALImp = "01",
                  MontoTotal = "190.57"
                }
              }
            }
          }
        },
        TipoApp = "1"
      };

      SolicitudEmitirDocumentoIdProductoNullDto = new()
      {
        Factura = new()
        {
          IdRangoNumeracion = "123456789",
          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalleDto>()
          {
            new FacturaDetalleDto
            {
              CodigoProducto = "1",
              Descripcion = "1",
              CantidadReal = "111111",
              CantidadUnidades = "2",
              UnidadMedida = "WSD",
              PrecioVentaUnitario = "1300.00",
              PrecioTotalSinImpuestos = "1100.00",
              CantidadRealUnidadMedida = "2P",
              ImpuestosDetalles = new List<FacturaImpuestosDto>
              {
                new FacturaImpuestosDto
                {
                  CodigoTOTALImp ="01",
                  BaseImponibleTOTALImp = "1003.00",
                  ValorTOTALImp = "190.57"
                }
              },
              ImpuestosTotales = new List<ImpuestosTotalesDto>
              {
                new ImpuestosTotalesDto
                {
                  CodigoTOTALImp = "01",
                  MontoTotal = "190.57"
                }
              }
            }
          }
        }
      };
      #endregion

      #region Solicitudes
      SolicitudEmitirDocumento = new()
      {
        Factura = new()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalle>
          {
            new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePago>()
          {
            new MediosDePago
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null
      };

      SolicitudEmitirDocumentoClienteValido = new()
      {
        Factura = new()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = "1",

          Cliente = new()
          {
            IdCliente = "00",
            TipoPersona = "1",
            TipoIdentificacion = "2",
            ResponsabilidadesRut = new List<ObligacionesBase>
            {
              new ObligacionesBase
              {
               Obligaciones = "R-99-PN",
               Regimen = "48"
              }
            },
            NumeroDocumento = "2",
            Notificar = "no",
            NombreRazonSocial = "NN",
            DetallesTributarios = new List<Tributos>
            { new Tributos
             {
              CodigoImpuesto = "1"
             }
            }
          },
          DetalleDeFactura = new List<FacturaDetalle>
          {
            new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            },
              new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePago>()
          {
            new MediosDePago
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null,
        TipoApp = "1"
      };

      SolicitudEmitirDocumentoIdRangoNumeracionNull = new()
      {
        Factura = new()
        {

          Propina = null,
          CantidadDecimales = "2",
          InformacionAdicional = null,
          IdRangoNumeracion = null,

          Cliente = new()
          {
            IdCliente = "123456789"
          },
          DetalleDeFactura = new List<FacturaDetalle>
          {
            new FacturaDetalle
            {
              IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
              CantidadUnidades = "2",
              MuestraGratis = "1"
            }
          },
          MediosDePago = new List<MediosDePago>()
          {
            new MediosDePago
            {
              MetodoDePago = "2",
              MedioPago = "10",
              FechaDeVencimiento = "2022-11-17"
            }
          },
          TipoOperacion = "10"
        },
        DocumentosAdjuntos = "0",
        IdUsuario = null
      };

      SolicitudDetalleFacturaProductoExistente = new List<FacturaDetalle>
      {
        new FacturaDetalle()
        {
          IdProducto = ConstantesCompartidasFacturacion.IdProductoExistente,
          CantidadUnidades = "2",
          UnidadMedida = "WSD",
          PrecioVentaUnitario = "1300.00",
          PrecioTotalSinImpuestos = "1100.00",
          PrecioTotal = "1300.00",
          MuestraGratis = "0",
          ImpuestosDetalles = new List<FacturaImpuestos>
          {
            new FacturaImpuestos()
            {
              CodigoTOTALImp = "01",
              PorcentajeTOTALImp = "19.00",
              BaseImponibleTOTALImp = "1003.00",
              ValorTOTALImp = "190.57",
              ControlInterno = "",
              UnidadMedidaTributo = "",
              UnidadMedida = "",
              ValorTributoUnidad = "",
              Extras = null
            }
          },
          ImpuestosTotales = new List<ImpuestosTotales>
          {
            new ImpuestosTotales()
            {
              CodigoTOTALImp = "01",
              MontoTotal = "190.57",
              RedondeoAplicado = null,
              Extras = null
            }
          },
          CantidadRealUnidadMedida = "2P"
        }
      };
      #endregion
    }
  }
}
