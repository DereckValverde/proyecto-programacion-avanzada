using System;
using System.Reflection;
using FluentValidation;
using FluentValidation.Mvc;
using Condominio.Application.Validators;

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
        private static readonly Assembly ApplicationAssembly = typeof(LoginViewModelValidator).Assembly;

        public IValidator<T> GetValidator<T>()
        {
            var validatorType = ApplicationAssembly.GetType(
                "Condominio.Application.Validators." + typeof(T).Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator<T>)Activator.CreateInstance(validatorType);
        }

        public IValidator GetValidator(Type type)
        {
            var validatorType = ApplicationAssembly.GetType(
                "Condominio.Application.Validators." + type.Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator)Activator.CreateInstance(validatorType);
        }
    }
}
