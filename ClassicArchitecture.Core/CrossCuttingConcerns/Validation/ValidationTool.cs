using ClassicArchitecture.Core.Utilities.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        public static async Task<IResult> ValidateAsync(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = await validator.ValidateAsync(context);
            if (!result.IsValid)
                return new ErrorResult(result.Errors.FirstOrDefault()?.ToString());

            return new SuccessResult();
        }

        public static IResult Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
                return new ErrorResult(result.Errors.FirstOrDefault()?.ToString());

            return new SuccessResult();
        }


    }
}
