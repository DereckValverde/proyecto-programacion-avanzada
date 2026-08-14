using Condominio.Application.ViewModels.Pago;
using FluentValidation;
using System;

namespace Condominio.Application.Validators
{
    public class PagoCreateViewModelValidator : AbstractValidator<PagoCreateViewModel>
    {
        public PagoCreateViewModelValidator()
        {
            RuleFor(x => x.FechaPago)
                .NotEqual(default(DateTime)).WithMessage("La fecha de pago es obligatoria.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");

            RuleFor(x => x.Periodo)
                .NotEmpty().WithMessage("El periodo es obligatorio.")
                .Matches(@"^\d{4}-(0[1-9]|1[0-2])$")
                    .WithMessage("El periodo debe tener el formato AAAA-MM, por ejemplo 2026-08.");

            RuleFor(x => x.IdVivienda)
                .GreaterThan(0).WithMessage("Debe seleccionar una vivienda.");
        }
    }
}
