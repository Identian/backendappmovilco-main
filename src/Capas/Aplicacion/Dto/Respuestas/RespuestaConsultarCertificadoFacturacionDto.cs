﻿namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaConsultarCertificadoFacturacionDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public CertificadoFacturacionDto? Certificado { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}