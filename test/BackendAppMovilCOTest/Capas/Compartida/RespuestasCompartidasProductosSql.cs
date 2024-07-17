using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using System.Xml;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasProductosSql
  {
    public RespuestaConsultarProducto DatosExistentesConsultarProducto { get; set; }
    public RespuestaConsultarProducto DatosNoEncontradosConsultarProducto { get; set; }

    public RespuestasCompartidasProductosSql()
    {
      InicializarRespuestas();
    }

    private void InicializarRespuestas()
    {
      DatosExistentesConsultarProducto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Datos = new()
        {
          CodigoProducto = "P000001",
          Descripcion = "Impresora HKA80",
          UnidadMedida = "WSD",
          PrecioVentaUnitario = "1003.00",
          Nota = "Nota",
          CantidadPorEmpaque = "1",
          Marca = "HKA",
          Modelo = "HKA80",
          EstandarCodigoProducto = "PHKA80",
          EstandarCodigo = "999",
          MandatorioNumeroIdentificacion = "",
          MandatorioNumeroIdentificacionDV = "",
          CantidadReal = "1",
          IdEsquema = "1"
        }
      };

      DatosNoEncontradosConsultarProducto = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoNitNoExiste,
        Resultado = ConstantesCompartidasFacturacion.ResultadoNitNoExiste,
        Mensaje = ConstantesCompartidasFacturacion.MensajeNitNoExiste
      };
    }
  }
}
