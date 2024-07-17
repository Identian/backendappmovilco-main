using Aplicacion.Entidad.Documentos;
using FluentValidation;
using Transversal.Comun.Utils;

namespace Aplicacion.Dto.Solicitudes.Validaciones.Documentos
{
  public class FacturaDetalleDtoValidator : AbstractValidator<FacturaDetalleDto>
  {
    public FacturaDetalleDtoValidator()
    {
      When(p => p.IdProducto != null, () =>
      {
        RuleFor(p => p.IdProducto).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(10).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
          .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      When(p => p.IdProducto == null, () =>
      {
        RuleFor(p => p.CodigoProducto).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("'{PropertyName}' es requerido")
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(50).WithMessage("Longitud incorrecta para '{PropertyName}'");

        RuleFor(p => p.Descripcion).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("'{PropertyName}' es requerido")
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(300).WithMessage("Longitud incorrecta para '{PropertyName}'");

        When(p => p.Nota != null, () =>
        {
          RuleFor(p => p.Nota).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
            .MinimumLength(20).WithMessage("Longitud incorrecta para '{PropertyName}'")
            .MaximumLength(5000).WithMessage("Longitud incorrecta para '{PropertyName}'");
        });

        When(p => p.CantidadPorEmpaque != null, () =>
        {
          RuleFor(p => p.CantidadPorEmpaque).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
            .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
            .MaximumLength(3).WithMessage("Longitud incorrecta para '{PropertyName}'")
            .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
            .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");
        });

        When(p => p.EstandarCodigoProducto != null, () =>
        {
          RuleFor(p => p.EstandarCodigoProducto).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
        });

        When(p => p.EstandarCodigo != null, () =>
        {
          RuleFor(p => p.EstandarCodigo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
        });

        RuleFor(p => p.CantidadReal).Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("'{PropertyName}' es requerido")
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .MaximumLength(6).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexEntero).WithMessage("Formato incorrecto para '{PropertyName}'")
          .Must(id => UtilidadesCadenas.EstaEnRangoEntero(id, 1, int.MaxValue)).WithMessage("Valor fuera del rango para '{PropertyName}'");
      });

      RuleFor(p => p.CantidadUnidades).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      RuleFor(p => p.UnidadMedida).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(3).WithMessage("Longitud incorrecta para '{PropertyName}'");

      RuleFor(p => p.PrecioVentaUnitario).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      RuleFor(p => p.PrecioTotalSinImpuestos).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");

      When(p => p.PrecioTotal != null, () =>
      {
        RuleFor(p => p.PrecioTotal).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Matches(UtilidadesCadenas.RegexMontoDecimal).WithMessage("Formato incorrecto para '{PropertyName}'");
      });

      When(p => p.MuestraGratis != null, () =>
      {
        RuleFor(p => p.MuestraGratis).Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
          .Length(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
          .Matches(UtilidadesCadenas.RegexBooleano).WithMessage("Formato incorrecto para '{PropertyName}'");
      });
      When(p => p.ImpuestosDetalles != null, () =>
      {
        RuleFor(p => p.ImpuestosDetalles).Cascade(CascadeMode.Stop)         
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
      RuleForEach(p => p.ImpuestosDetalles).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
        .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos")
        .SetValidator(new FacturaImpuestosDtoValidator()!);
      });
      When(p => p.ImpuestosTotales != null, () =>
      {
        RuleFor(p => p.ImpuestosTotales).Cascade(CascadeMode.Stop)         
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío");
      RuleForEach(p => p.ImpuestosTotales).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Los elementos individuales de '{PropertyName}' son requeridos")
        .NotEmpty().WithMessage("Los elementos individuales de '{PropertyName}' no pueden estar vacíos")
        .SetValidator(new ImpuestosTotalesDtoValidator()!);
      });
      RuleFor(p => p.CantidadRealUnidadMedida).Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("'{PropertyName}' es requerido")
        .NotEmpty().WithMessage("'{PropertyName}' no puede estar vacío")
        .MinimumLength(1).WithMessage("Longitud incorrecta para '{PropertyName}'")
        .MaximumLength(6).WithMessage("Longitud incorrecta para '{PropertyName}'");

      //Campos pendientes por Validaciones
      //Descripcion2
      //Descripcion3
      //Seriales
      //PrecioReferencia
      //CodigoTipoPrecio
      //DescripcionTecnica
      //Marca
      //Modelo
      //SubCodigoProducto
      //CodigoFabricante
      //SubCodigoFabricante
      //NombreFabricante
      //EstandarCodigoID
      //EstandarCodigoNombre
      //EstandarCodigoIdentificador
      //EstandarSubCodigoProducto
      //EstandarOrganizacion
      //CodigoIdentificadorPais
      //MandatorioNumeroIdentificacion
      //MandatorioNumeroIdentificacionDV
      //MandatorioTipoIdentificacion
      //CargosDescuentos
      //InformacionAdicional
      //DocumentosReferenciados
      //Extras
      //TipoAIU
      //IdEsquema
    }
  }
}
