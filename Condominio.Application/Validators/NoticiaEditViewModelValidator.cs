using FluentValidation;
using Condominio.Application.ViewModels.Noticia;

namespace Condominio.Application.Validators
{
    public class NoticiaEditViewModelValidator : AbstractValidator<NoticiaEditViewModel>
    {
        public NoticiaEditViewModelValidator()
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
