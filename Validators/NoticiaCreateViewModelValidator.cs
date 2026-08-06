using FluentValidation;
using proyecto_programacion_avanzada.ViewModels.Noticia;

namespace proyecto_programacion_avanzada.Validators
{
    public class NoticiaCreateViewModelValidator : AbstractValidator<NoticiaCreateViewModel>
    {
        public NoticiaCreateViewModelValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(150).WithMessage("El título no puede superar los 150 caracteres.");

            RuleFor(x => x.Contenido)
                .NotEmpty().WithMessage("El contenido es obligatorio.")
                .MaximumLength(2000).WithMessage("El contenido no puede superar los 2000 caracteres.");
        }
    }
}
