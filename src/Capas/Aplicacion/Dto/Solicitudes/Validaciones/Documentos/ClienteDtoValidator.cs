using Aplicacion.Entidad.Documentos;
using FluentValidation;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class ClienteDtoValidator : AbstractValidator<ClienteDto>
  {
    public ClienteDtoValidator()
    {
      RuleFor(r => r.DetallesTributarios).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.NombreRazonSocial).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.Notificar).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.NumeroDocumento).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.ResponsabilidadesRut).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.TipoIdentificacion).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");

      RuleFor(r => r.TipoPersona).Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("'{PropertyName}' es requerido")
       .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
    }
  }
}
