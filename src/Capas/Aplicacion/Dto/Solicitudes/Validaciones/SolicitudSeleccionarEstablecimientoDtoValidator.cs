using Aplicacion.Entidad.Solicitudes;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{

  public class SolicitudSeleccionarEstablecimientoDtoValidator : AbstractValidator<SolicitudSeleccionarEstablecimientoDto>
  {


    public SolicitudSeleccionarEstablecimientoDtoValidator()
    {
      RuleFor(r => r.IdEstablecimiento).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("'{PropertyName}' es requerido")
      .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
      .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
      .Must(IdEstablecimiento => UtilidadesCadenas.EstaEnRangoEnteroLargo(IdEstablecimiento, 0, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");


      RuleFor(r => r.Seleccionado).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .Matches(UtilidadesCadenas.RegexBooleano).WithMessage("Formato incorrecto para '{PropertyName}'");




      When(p => p.Referencia != null, () =>
         {
           RuleFor(r => r.Referencia).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'");
         });

    }


    }
}
