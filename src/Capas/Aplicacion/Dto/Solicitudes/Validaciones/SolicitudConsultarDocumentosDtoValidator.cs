using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarDocumentosDtoValidator : AbstractValidator<SolicitudConsultarDocumentosDto>
  {
    public SolicitudConsultarDocumentosDtoValidator()
    {
      When(s => s.Sistema != null, () =>
      {
        RuleFor(f => f.Sistema).Cascade(CascadeMode.Stop)
        .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
        .Must(Sistema => UtilidadesCadenas.EstaEnRangoEntero(Sistema, 1, 3)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });
      RuleFor(s => s.FormatoRequerido).Cascade(CascadeMode.Stop)
        .Matches(UtilidadesCadenas.RegexFormatoArchivo).WithMessage("Valor fuera del catálogo para '{PropertyName}'");
    }
  }
}
