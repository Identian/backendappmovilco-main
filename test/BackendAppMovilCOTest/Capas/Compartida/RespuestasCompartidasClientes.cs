using Aplicacion.Dto.Respuestas;

using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasClientes
  {
 
    public Cliente ClienteDummy { get; set; }
    public Cliente ClienteFinalEscenarioNull { get; set; }
    public RespuestaConsultarCliente DatosExistentesConsultarClienteRegistrado { get; set; }
    public RespuestaConsultarCliente DatosExistentesConsultarClienteRegistradoDestinarioNull { get; set; }
    public RespuestaConsultarCliente DatosExistentesConsultarClienteRegistradoNotificarNo { get; set; }
 

    private void InicializarRespuestas()
    {
 
      ClienteFinalEscenarioNull = new()
      {
        NombreRazonSocial = "consumidor final",
        TipoPersona = "1",
        TipoIdentificacion = "13",
        NumeroDocumento = "222222222222",
        NumeroIdentificacionDv = null,
        Notificar = "NO",
        Destinatario = null,
        ResponsabilidadesRut = new List<ObligacionesBase>
        {
          new ObligacionesBase
          {
            Obligaciones = "R-99-PN"
          }
        },
        DetallesTributarios = new List<Tributos>
        {
          new Tributos
          {
            CodigoImpuesto = "ZZ"
          }
        },
        DireccionCliente = null,
        DireccionFiscal = null,
        InformacionLegalCliente = null
      };

      DatosExistentesConsultarClienteRegistrado = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Datos = ClientePrueba(),
        Errores = null
      };

      DatosExistentesConsultarClienteRegistradoDestinarioNull = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Datos = ClientePrueba(),
        Errores = null
      };
      DatosExistentesConsultarClienteRegistradoDestinarioNull.Datos.Destinatario = null;

      DatosExistentesConsultarClienteRegistradoNotificarNo = new()
      {
        Codigo = ConstantesCompartidasFacturacion.CodigoDatosValidos,
        Resultado = ConstantesCompartidasFacturacion.ResultadoDatosValidos,
        Mensaje = ConstantesCompartidasFacturacion.MensajeDatosValidos,
        Datos = ClientePrueba(),
        Errores = null
      };
      DatosExistentesConsultarClienteRegistradoNotificarNo.Datos.Notificar = "NO";
    }

 
 

    private static Cliente ClientePrueba()
    {
      return new Cliente
      {
        NombreRazonSocial = "The Factory HKA Colombia",
        TipoPersona = "1",
        TipoIdentificacion = "31",
        NumeroDocumento = "901041710",
        NumeroIdentificacionDv = "5",
        Email = "prueba@thefactoryhka.com",
        Notificar = "SI",
        ResponsabilidadesRut = new List<ObligacionesBase>
        {
          new ObligacionesBase
          {
            Obligaciones = "0-14",
            Regimen = "04"
          }
        },
        DetallesTributarios = new List<Tributos>
        {
          new Tributos
          {
            CodigoImpuesto = "01"
          }
        },
        DireccionFiscal = new()
        {
          Ciudad = "MANIZALES",
          CodigoDepartamento = "11",
          Departamento = "Bogotá",
          Direccion = "Direccion",
          Lenguaje = "es",
          Municipio = "11001",
          Pais = "CO",
          ZonaPostal = "110211"
        },
        InformacionLegalCliente = new()
        {
          CodigoEstablecimiento = "00001",
          NombreRegistroRut = "CONSORCIO ALIANZA SAN CRISTOBAL 4",
          NumeroIdentificacion = "901041710",
          NumeroIdentificacionDv = "5",
          TipoIdentificacion = "31"
        },
        Destinatario = new List<Destinatario>()
        {
          new Destinatario
          {
            Email = new List<string>()
            {
              "correo@email.com"
            }
          }
        }
      };
    }

    public RespuestasCompartidasClientes()
    {
      ClienteDummy = ClientePrueba();
      InicializarRespuestas();
 

    }
  }
}
