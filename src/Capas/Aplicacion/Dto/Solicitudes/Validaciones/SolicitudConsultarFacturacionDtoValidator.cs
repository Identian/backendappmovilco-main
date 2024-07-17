using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarFacturacionDtoValidator:AbstractValidator<SolicitudConsultarFacturacionDto>
  {
    public SolicitudConsultarFacturacionDtoValidator() {
      When(s => s.Plataforma != null, () =>
      {
        RuleFor(s => s.Plataforma).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(4).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(5).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(plataforma => UtilidadesCadenas.EsPlataformaValida(plataforma)).WithMessage("Valor fuera del catálogo para '{PropertyName}'");
      });
    }
  }
}
