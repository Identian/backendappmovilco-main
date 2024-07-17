using FluentValidation;
using Microsoft.Extensions.Configuration;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{
  public class SolicitudConsultarCatalogoDtoValidator : AbstractValidator<SolicitudConsultarCatalogoDto>
  {
    private static bool CatalogoPermitido(IConfiguration configuracion, string identificador)
    {
      return (UtilidadesCadenas.ListaCatalogosPermitidos(configuracion)!.Contains(identificador));
    }

    public SolicitudConsultarCatalogoDtoValidator(IConfiguration configuracion)
    {
      When(c => c.Identificador != null, () =>
      {
        RuleFor(c => c.Identificador).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(Identificador => CatalogoPermitido(configuracion, Identificador)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });
    }
  }
}
