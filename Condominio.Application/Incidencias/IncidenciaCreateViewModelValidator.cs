using FluentValidation;
using Condominio.Domain.Incidencias;

namespace Condominio.Application.Incidencias
{
    public class IncidenciaCreateViewModelValidator : AbstractValidator<IncidenciaCreateViewModel>
    {
        public IncidenciaCreateViewModelValidator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

            RuleFor(x => x.Prioridad)
                .IsInEnum().WithMessage("Debe seleccionar una prioridad.");
        }
    }
}
