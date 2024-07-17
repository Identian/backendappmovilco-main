using Aplicacion.Dto.Solicitudes.Dispositivos;
using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos
{
  public class SolicitudAsociarAliasDtoValidator : AbstractValidator<SolicitudAsociarAliasDto>
  {
    public SolicitudAsociarAliasDtoValidator()
    {
      RuleFor(s => s.SerialLogico).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");

      RuleFor(s => s.Alias).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
    }
  }
}
