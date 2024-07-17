using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudValidarClaveSecretaDtoValidate : AbstractValidator<SolicitudValidarClaveSecretaDto>
  {
    public SolicitudValidarClaveSecretaDtoValidate()
    {
      RuleFor(c => c.ClaveSecreta).Cascade(CascadeMode.Stop)
     .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
     .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
     .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
    }
  }
}
