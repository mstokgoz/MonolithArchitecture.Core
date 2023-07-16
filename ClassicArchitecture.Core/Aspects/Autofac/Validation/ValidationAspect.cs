using Castle.DynamicProxy;
using ClassicArchitecture.Core.CrossCuttingConcerns.Validation;
using ClassicArchitecture.Core.Utilities.Interceptors;
using ClassicArchitecture.Core.Utilities.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                throw new System.Exception();
            _validatorType = validatorType;
        }

        public override async void Intercept(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                var result = typeof(ValidationTool).GetMethod("Validate")?.ReturnType;

                if (invocation.Method.ReturnType == result)
                {
                    var validationResult = await Task.FromResult(ValidationTool.ValidateAsync(validator, entity));
                    if (validationResult.Result.Success.Equals(RESULTTYPE.ERROR))
                    {
                        invocation.ReturnValue = validationResult;
                        return;
                    }
                    invocation.Proceed();
                }
            }
        }

    }
}
