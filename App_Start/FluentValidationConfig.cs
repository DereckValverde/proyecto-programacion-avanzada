using System;
using FluentValidation;
using FluentValidation.Mvc;

namespace proyecto_programacion_avanzada.App_Start
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
        public IValidator<T> GetValidator<T>()
        {
            var validatorType = typeof(FluentValidatorFactory).Assembly.GetType(
                "proyecto_programacion_avanzada.Validators." + typeof(T).Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator<T>)Activator.CreateInstance(validatorType);
        }

        public IValidator GetValidator(Type type)
        {
            var validatorType = typeof(FluentValidatorFactory).Assembly.GetType(
                "proyecto_programacion_avanzada.Validators." + type.Name + "Validator");

            return validatorType == null
                ? null
                : (IValidator)Activator.CreateInstance(validatorType);
        }
    }
}
