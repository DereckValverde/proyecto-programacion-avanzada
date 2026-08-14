using FluentValidation;
using Condominio.Domain.Residentes;
using Condominio.Domain.Usuarios;

namespace Condominio.Application.Usuarios
{
    public class UsuarioEditViewModelValidator : AbstractValidator<UsuarioEditViewModel>
    {
        public UsuarioEditViewModelValidator()
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

            RuleFor(x => x.Rol)
                .IsInEnum().WithMessage("Seleccione un rol válido.");

            RuleFor(x => x.Estado)
                .IsInEnum().WithMessage("Seleccione un estado válido.");

            RuleFor(x => x.FechaIngreso)
                .NotNull().WithMessage("La fecha de ingreso es obligatoria para un residente.")
                .When(x => x.Rol == RolUsuario.Residente);

            RuleFor(x => x.IdVivienda)
                .NotNull().WithMessage("Debe seleccionar una vivienda.")
                .When(x => x.Rol == RolUsuario.Residente);
        }
    }
}
