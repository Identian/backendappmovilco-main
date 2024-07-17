using Aplicacion.Dto.Solicitudes.Dispositivos;
using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Dispositivos
{
  public class SolicitudConsultarSuscripcionDispositivoDtoValidator : AbstractValidator<SolicitudConsultarSuscripcionDispositivoDto>
  {
    public SolicitudConsultarSuscripcionDispositivoDtoValidator()
    {
      RuleFor(s => s.SerialLogico).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
    }
  }
}
