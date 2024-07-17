using Aplicacion.Dto.Solicitudes.Validaciones.Documentos;
using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudEmitirDocumentoDtoValidator : AbstractValidator<SolicitudEmitirDocumentoDto>
  {
    public SolicitudEmitirDocumentoDtoValidator()
    {
      RuleFor(s => s.Factura).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .SetValidator(new FacturaGeneralDtoValidator()!);

      When(s => s.TipoApp != null, () =>
      {
        RuleFor(s => s.TipoApp).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
          .Must(IdUsuario => UtilidadesCadenas.EstaEnRangoEntero(IdUsuario, 1, 2)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(s => s.SerialLogico != null, () =>
      {
        RuleFor(s => s.SerialLogico).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
      });
    }
  }
}
