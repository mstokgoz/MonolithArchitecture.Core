using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class ErrorDataResult <T> : DataResult<T>
    {

        public ErrorDataResult(T data, string message) : base(data, RESULTTYPE.ERROR, message)
        {

        }

        public ErrorDataResult(T data) : base(data, RESULTTYPE.ERROR)
        {

        }

        public ErrorDataResult(string message) : base(default, RESULTTYPE.ERROR, message)
        {

        }
    }
}
