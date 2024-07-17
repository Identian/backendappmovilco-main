﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Solicitudes
{
  public class SolicitudConsultarReferenciaDocumentoFacturacion
  {
    public string? IdEmpresa { get; set; }
    public string? Nit { get; set; }
    public string? TokenEmpresa { get; set; }
    public string? TokenClave { get; set; }
    public string? Plataforma { get; set; }
    public string? IdInvoice { get; set; }
    public string? TipoConsulta { get; set; }

  }
}
