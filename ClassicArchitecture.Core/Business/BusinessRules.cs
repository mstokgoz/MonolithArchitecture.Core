using ClassicArchitecture.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Business
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            IResult warningOrSuccessResult = null;

            foreach (var result in logics)
            {
                var type = result.GetType();

                switch (type)
                {
                    case not null when type == typeof(ErrorResult):
                        return result;

                    case not null when type == typeof(WarningResult):
                        warningOrSuccessResult = new WarningResult(result.Message);
                        break;
                }
            }
            if (warningOrSuccessResult != null)
                return warningOrSuccessResult;

            return new SuccessResult();
        }

    }
}
