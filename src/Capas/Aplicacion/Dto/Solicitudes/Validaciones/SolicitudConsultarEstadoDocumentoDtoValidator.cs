using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarEstadoDocumentoDtoValidator : AbstractValidator<SolicitudConsultarEstadoDocumentoFacturacionDto>
  {
    public SolicitudConsultarEstadoDocumentoDtoValidator() {
      RuleFor(r => r.Consecutivo).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("'{PropertyName}' es requerido")
      .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");  
   
      RuleFor(r => r.Plataforma).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(4).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(5).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .Must(plataforma => UtilidadesCadenas.EsPlataformaValida(plataforma)).WithMessage("Valor fuera del catálogo para '{PropertyName}'");
    }



  }
}
