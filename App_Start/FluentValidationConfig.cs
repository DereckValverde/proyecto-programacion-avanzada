using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Mvc;
using Condominio.Application.Login;

namespace Condominio.Web.App_Start
{
    public class FluentValidationConfig
    {
        public static void RegisterFluentValidation()
        {
            FluentValidationModelValidatorProvider.Configure(config =>
            {
                config.ValidatorFactory = new FluentValidatorFactory();
                config.AddImplicitRequiredValidator = false;
            });
        }
    }

    public class FluentValidatorFactory : IValidatorFactory
    {
        private static Assembly ApplicationAssembly = typeof(LoginViewModelValidator).Assembly;

        private static Type ObtenerTipoValidator(string nombreTipo)
        {
            return ApplicationAssembly.GetTypes()
                .FirstOrDefault(t => t.Name == nombreTipo && typeof(IValidator).IsAssignableFrom(t));
        }

        public IValidator<T> GetValidator<T>()
        {
            var validatorType = ObtenerTipoValidator(typeof(T).Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator<T>)Activator.CreateInstance(validatorType);
        }

        public IValidator GetValidator(Type type)
        {
            var validatorType = ObtenerTipoValidator(type.Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator)Activator.CreateInstance(validatorType);
        }
    }
}
