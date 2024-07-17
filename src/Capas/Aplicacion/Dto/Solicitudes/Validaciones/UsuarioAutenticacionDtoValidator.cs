using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class UsuarioAutenticacionDtoValidator : AbstractValidator<UsuarioAutenticacionDto>
  {
    public UsuarioAutenticacionDtoValidator()
    {
      RuleFor(u => u.Usuario).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(200).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .Matches(UtilidadesCadenas.RegexEmail).WithMessage("Formato incorrecto para '{PropertyName}'");

      RuleFor(u => u.Contrasena).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(8).WithMessage("Longitud incorrecta para '{PropertyName}'");

      When(u => u.TipoSesion != null, () =>
      {
        RuleFor(u => u.TipoSesion).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(tipoSesion => UtilidadesCadenas.EsTipoSesionValido(tipoSesion)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(u => u.TipoApp != null, () =>
      {
        RuleFor(u => u.TipoApp).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(tipoApp => UtilidadesCadenas.EstaEnRangoEntero(tipoApp, 1, 2)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });
    }
  }
}
