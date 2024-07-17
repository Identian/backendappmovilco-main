using Dominio.Entidad.Documentos;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasClientesRepositorioSql
  {
    public Cliente ClienteRegistrado { get; set; }
    public IEnumerable<DireccionBase> DireccionesClienteRegistrado { get; set; }

    public static IEnumerable<Destinatario> ListaDestinatariosClienteRegistrado(string email, string canalDeEntrega)
    {
      return (new List<Destinatario>()
      {
        new()
        {
          CanalDeEntrega = canalDeEntrega,
          Email = new List<string>()
          {
            email
          }
        }
      });
    }

    private void InicializarRespuestas()
    {
      DireccionBase direccionClienteRegistrado = new()
      {
        Tipo = "1",
        Ciudad = "1",
        CodigoDepartamento = "1",
        Departamento = "1",
        Direccion = "1",
        Lenguaje = "1",
        Municipio = "1",
        Pais = "1",
        ZonaPostal = "1",
      };

      DireccionBase direccionFiscalClienteRegistrado = new()
      {
        Tipo = "2",
        Ciudad = "2",
        CodigoDepartamento = "2",
        Departamento = "2",
        Direccion = "2",
        Lenguaje = "2",
        Municipio = "2",
        Pais = "2",
        ZonaPostal = "2",
      };

      DireccionesClienteRegistrado = new List<DireccionBase>()
      {
        direccionClienteRegistrado,
        direccionFiscalClienteRegistrado
      };

      ClienteRegistrado = new()
      {
        NombreRegistroRut = "The Factory HKA",
        NumeroIdentificacion = "900390126",
        NumeroIdentificacionDv = "6",
        TipoIdentificacion = "31",
        DireccionCliente = direccionClienteRegistrado,
        DireccionFiscal = direccionFiscalClienteRegistrado
      };

      InformacionLegal informacionLegalClienteRegistrado = new()
      {
        NombreRegistroRut = ClienteRegistrado.NombreRegistroRut,
        NumeroIdentificacion = ClienteRegistrado.NumeroIdentificacion,
        NumeroIdentificacionDv = ClienteRegistrado.NumeroIdentificacionDv,
        TipoIdentificacion = ClienteRegistrado.TipoIdentificacion
      };

      ClienteRegistrado.InformacionLegalCliente = informacionLegalClienteRegistrado;
    }

    public RespuestasCompartidasClientesRepositorioSql()
    {
      InicializarRespuestas();
    }
  }
}
