using FluentValidation;
using proyecto_programacion_avanzada.ViewModels.Login;

namespace proyecto_programacion_avanzada.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("Ingrese un correo electrónico válido.");

            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
