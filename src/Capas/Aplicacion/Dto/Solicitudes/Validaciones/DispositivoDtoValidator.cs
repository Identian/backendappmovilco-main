using Aplicacion.Dto.Dispositivos;
using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class DispositivoDtoValidator : AbstractValidator<DispositivoDto>
  {
    public DispositivoDtoValidator()
    {
      RuleFor(s => s.SerialLogico).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(n => n.Nombre).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(s => s.TipoApp).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
    }
  }
}
