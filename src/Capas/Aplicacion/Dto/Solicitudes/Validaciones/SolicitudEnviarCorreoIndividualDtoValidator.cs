using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudEnviarCorreoIndividualDtoValidator : AbstractValidator<SolicitudEnviarCorreoIndividualDto>
  {
    public SolicitudEnviarCorreoIndividualDtoValidator()
    {
      RuleFor(r => r.Documento).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(50).WithMessage("Longitud incorrecta para '{PropertyName}'");

      RuleFor(r => r.Correo).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(6).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
    }
  }
}
