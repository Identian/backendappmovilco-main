using Aplicacion.Entidad.Documentos;
using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaImpuestosDtoValidator : AbstractValidator<FacturaImpuestosDto>
  {
    public FacturaImpuestosDtoValidator()
    {
      RuleFor(i => i.CodigoTOTALImp).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Length(2).WithMessage("Longitud incorrecta para '{PropertyName}'");

      When(p => p.PorcentajeTOTALImp != null, () =>
      {
        RuleFor(i => i.PorcentajeTOTALImp).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Matches(UtilidadesCadenas.RegexValidacionPorcentaje).WithMessage("Formato incorrecto para '{PropertyName}'");
      });

      RuleFor(i => i.BaseImponibleTOTALImp).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      RuleFor(i => i.ValorTOTALImp).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      When(p => p.ControlInterno != null, () =>
      {
        RuleFor(i => i.ControlInterno).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Length(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
          .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(p => p.UnidadMedidaTributo != null, () =>
      {
        RuleFor(i => i.UnidadMedidaTributo).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");
      });

      When(p => p.UnidadMedida != null, () =>
      {
        RuleFor(i => i.UnidadMedida).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(3).WithMessage("Longitud incorrecta para '{PropertyName}'");
      });

      When(p => p.ValorTributoUnidad != null, () =>
      {
        RuleFor(i => i.ValorTributoUnidad).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");
      });
    }
  }
}
