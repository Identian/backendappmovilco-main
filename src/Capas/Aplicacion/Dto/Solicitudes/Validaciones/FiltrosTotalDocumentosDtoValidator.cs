using Aplicacion.Dto.Solicitudes.FiltrosSolicitudes;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class FiltrosTotalDocumentosDtoValidator : AbstractValidator<FiltrosTotalDocumentosDto>
  {
    private static bool ValorPermitidoFiltroTipo(IConfiguration configuracion, string identificador)
    {
      return (UtilidadesCadenas.ListaValoresPermitidosFiltroTipoTotalDocumentos(configuracion)!.Contains(identificador));
    }

    public FiltrosTotalDocumentosDtoValidator(IConfiguration configuracion)
    {
      When(f => f.Tipo != null, () =>
      {
        RuleFor(f => f.Tipo).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(identificador => ValorPermitidoFiltroTipo(configuracion, identificador)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(f => f.Anio != null, () =>
      {
        RuleFor(f => f.Anio).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Length(4).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
          .Must(anio => UtilidadesCadenas.EstaEnRangoEnteroLargo(anio, 2000, 2099)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(f => f.OrigenFacturacion != null, () =>
      {
        RuleFor(f => f.OrigenFacturacion).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

        RuleForEach(f => f.OrigenFacturacion).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
          .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos")
          .Length(1).WithMessage("Longitud incorrecta para '{PropertyValue}' en '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyValue}' en '{PropertyName}'")
          .Must(origen => UtilidadesCadenas.EstaEnRangoEnteroLargo(origen, 1, 3)).WithMessage("Valor fuera del rango para '{PropertyValue}' en '{PropertyName}'");
      });
    }
  }
}
