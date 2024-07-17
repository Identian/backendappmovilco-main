using BackendAppMovilCOTest.Capas.Infraestructura.Repositorio;
using Dominio.Entidad;
using Dominio.Entidad.Documentos;
using Dominio.Entidad.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAppMovilCOTest.Capas.Compartida
{
  public class RespuestasCompartidasNumeracionAutorizada
  {
    public RespuestaConsultarNumeracionAutorizada DatosExistentesConsultarNumeraciones { get; set; }
    public RespuestaConsultarNumeracionAutorizada DatosIdRangoNumeracionNoExisteConsultarNumeraciones { get; set; }
    

    public void InicializarRespuestas()
    {
      DatosExistentesConsultarNumeraciones = new RespuestaConsultarNumeracionAutorizada()
      {
        Codigo = 200,
        Resultado = "Exitoso",
        Mensaje = "Consulta exitosa",
        Datos = new NumeracionAutorizada()
        {
          IdNumeracion = "1",
          NumeroResolucion = "RES-2023-001",
          FechaResolucion = "2023-06-08",
          Prefijo = "PRF",
          NumeroDesde = "1",
          NumeroHasta = "200",
          NumeroInicial = "1",
          FechaDesde = "2023-06-01",
          FechaHasta = "2023-06-30",
          ClaveTecnica = "CT-001",
          TipoDocumento = "01",
          IdEstablecimiento = "1541",
          TipoServicio = "1",
          Modalidad = "01",
          TipoAmbienteSecuencial = "01",
          TestSetId = "TEST-001",
          EnvioDian = "Enviado",
          Activo = "Sí",
          RangoNumeracion = "PRF-1"
        },
        Errores = null
      };

      DatosIdRangoNumeracionNoExisteConsultarNumeraciones = new RespuestaConsultarNumeracionAutorizada()
      {
        Codigo = 404,
        Resultado = "Error",
        Mensaje = "No se encontró la numeracion autorizada para este cliente",
        Datos = null,
        Errores = null
      };
    }

    public RespuestasCompartidasNumeracionAutorizada()
    {
      InicializarRespuestas();
    }
  }
}



