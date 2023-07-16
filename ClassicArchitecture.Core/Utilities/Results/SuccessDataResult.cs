using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, RESULTTYPE.SUCCESS, message)
        {

        }

        public SuccessDataResult(T data) : base(data, RESULTTYPE.SUCCESS)
        {

        }

        public SuccessDataResult(string message) : base(default, RESULTTYPE.SUCCESS, message)
        {

        }

    }
}
