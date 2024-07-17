﻿namespace Aplicacion.Dto.Respuestas
{
  public class RespuestaSeleccionarSecuencialDto
  {
    public int Codigo { get; set; }
    public string? Resultado { get; set; }
    public string? Mensaje { get; set; }
    public string? Nit { get; set; }
    public string? IdEmpresa { get; set; }
    public string? IdNumeracion { get; set; }
    public IEnumerable<string>? Errores { get; set; }
  }
}