using Aplicacion.Entidad.Documentos;
using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class ImpuestosTotalesDtoValidator : AbstractValidator<ImpuestosTotalesDto>
  {
    public ImpuestosTotalesDtoValidator()
    {
      RuleFor(i => i.CodigoTOTALImp).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Length(2).WithMessage("Longitud incorrecta para '{PropertyName}'");

      RuleFor(i => i.MontoTotal).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      When(p => p.RedondeoAplicado != null, () =>
      {
        RuleFor(i => i.RedondeoAplicado).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");
      });
    }
  }
}
