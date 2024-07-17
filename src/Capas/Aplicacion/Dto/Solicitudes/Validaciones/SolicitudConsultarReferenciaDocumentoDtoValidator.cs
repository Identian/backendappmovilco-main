using Aplicacion.Entidad.Solicitudes;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones
{

  public class SolicitudConsultarReferenciaDocumentoDtoValidator : AbstractValidator<SolicitudConsultarReferenciaDocumentoFacturacionDto>
  {

    private static bool TipoDeConsultaPermitida(IConfiguration configuracion, string identificador)
    {
      return (UtilidadesCadenas.ListaTipoDeConsultaPermitidos(configuracion)!.Contains(identificador));
    }

    public SolicitudConsultarReferenciaDocumentoDtoValidator(IConfiguration configuracion)
    {
      RuleFor(r => r.IdInvoice).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("'{PropertyName}' es requerido")
      .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
      .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'");


      RuleFor(r => r.Plataforma).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(4).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(5).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .Must(plataforma => UtilidadesCadenas.EsPlataformaValida(plataforma)).WithMessage("Valor fuera del catálogo para '{PropertyName}'");


      When(c => c.TipoConsulta != null, () =>
      {
        RuleFor(c => c.TipoConsulta).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(255).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Must(TipoConsulta => TipoDeConsultaPermitida(configuracion, TipoConsulta)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

    }



  }
}
