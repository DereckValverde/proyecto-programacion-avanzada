using Condominio.Domain.Pagos;
using FluentValidation;
using System;

namespace Condominio.Application.Pagos
{
    public class PagoEditViewModelValidator : AbstractValidator<PagoEditViewModel>
    {
        public PagoEditViewModelValidator()
        {
            RuleFor(x => x.FechaPago)
                .NotEqual(default(DateTime)).WithMessage("La fecha de pago es obligatoria.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");

            RuleFor(x => x.Periodo)
                .NotEmpty().WithMessage("El periodo es obligatorio.")
                .Matches(@"^\d{4}-(0[1-9]|1[0-2])$")
                    .WithMessage("El periodo debe tener el formato AAAA-MM, por ejemplo 2026-08.");

            RuleFor(x => x.Estado)
                .IsInEnum().WithMessage("Debe seleccionar un estado válido.");

            RuleFor(x => x.IdVivienda)
                .GreaterThan(0).WithMessage("Debe seleccionar una vivienda.");
        }
    }
}
