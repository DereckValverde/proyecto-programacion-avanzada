using FluentValidation;
using proyecto_programacion_avanzada.Common.Enums;
using proyecto_programacion_avanzada.ViewModels.Usuario;

namespace proyecto_programacion_avanzada.Validators
{
    public class UsuarioCreateViewModelValidator : AbstractValidator<UsuarioCreateViewModel>
    {
        public UsuarioCreateViewModelValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("Ingrese un correo electrónico válido.")
                .MaximumLength(150).WithMessage("El correo no puede superar los 150 caracteres.");

            RuleFor(x => x.Telefono)
                .MaximumLength(20).WithMessage("El teléfono no puede superar los 20 caracteres.")
                .Matches(@"^[0-9+\- ]+$").WithMessage("Ingrese un teléfono válido.")
                .When(x => !string.IsNullOrWhiteSpace(x.Telefono));

            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .MaximumLength(255).WithMessage("La contraseña no puede superar los 255 caracteres.");

            RuleFor(x => x.Rol)
                .IsInEnum().WithMessage("Seleccione un rol válido.");

            RuleFor(x => x.Estado)
                .IsInEnum().WithMessage("Seleccione un estado válido.");

            RuleFor(x => x.FechaIngreso)
                .NotNull().WithMessage("La fecha de ingreso es obligatoria para un residente.")
                .When(x => x.Rol == RolUsuario.Residente);

            RuleFor(x => x.IdVivienda)
                .NotNull().WithMessage("Debe seleccionar la vivienda del residente.")
                .When(x => x.Rol == RolUsuario.Residente);
        }
    }
}
