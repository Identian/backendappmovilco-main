using Aplicacion.Entidad.Documentos;
using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaGeneralDtoValidator : AbstractValidator<FacturaGeneralDto>
  {
    public FacturaGeneralDtoValidator()
    {
      RuleFor(f => f.IdRangoNumeracion).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(10).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
        .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");

      When(f => f.Cliente != null, () =>
      {
        When(f => f.Cliente!.IdCliente != null && f.Cliente.IdCliente != "00", () =>
        {
          RuleFor(f => f!.Cliente!.IdCliente).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("'{PropertyName}' es requerido")
            .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
            .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
            .MaximumLength(10).WithMessage("Longitud incorrecta para '{PropertyName}'")
            .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
            .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");
        });

        When(f => f.Cliente!.IdCliente != null && f.Cliente.IdCliente == "00", () =>
        {
          RuleFor(f => f.Cliente).Cascade(CascadeMode.Stop)
             .SetValidator(new ClienteDtoValidator()!);
        });
      });



      RuleFor(f => f!.DetalleDeFactura).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
      RuleForEach(f => f!.DetalleDeFactura).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
        .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos")
        .SetValidator(new FacturaDetalleDtoValidator()!);

      When(p => p.ImpuestosGenerales != null, () =>
      {
        RuleFor(p => p.ImpuestosGenerales).Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
        RuleForEach(p => p.ImpuestosGenerales).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
          .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos");

      });
      When(p => p.ImpuestosTotales != null, () =>
      {
        RuleFor(p => p.ImpuestosTotales).Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
        RuleForEach(p => p.ImpuestosTotales).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
          .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos");

      });
    }


  }
}
