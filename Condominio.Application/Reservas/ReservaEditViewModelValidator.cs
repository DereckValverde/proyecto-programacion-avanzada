using FluentValidation;
using Condominio.Domain.Reservas;

namespace Condominio.Application.Reservas
{
    public class ReservaEditViewModelValidator : AbstractValidator<ReservaEditViewModel>
    {
        public ReservaEditViewModelValidator()
        {
            RuleFor(x => x.FechaReserva)
                .NotEmpty().WithMessage("La fecha de la reserva es obligatoria.");

            RuleFor(x => x.HoraInicio)
                .NotEmpty().WithMessage("La hora de inicio es obligatoria.");

            RuleFor(x => x.HoraFin)
                .NotEmpty().WithMessage("La hora de fin es obligatoria.")
                .GreaterThan(x => x.HoraInicio).WithMessage("La hora de fin debe ser mayor a la hora de inicio.");

            RuleFor(x => x.IdVivienda)
                .GreaterThan(0).WithMessage("Debe seleccionar una vivienda.");

            RuleFor(x => x.IdArea)
                .GreaterThan(0).WithMessage("Debe seleccionar un área común.");
        }
    }
}