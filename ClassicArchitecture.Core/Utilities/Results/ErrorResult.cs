using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string? message) : base(RESULTTYPE.ERROR, message)
        {

        }

        public ErrorResult() : base(RESULTTYPE.ERROR)
        {

        }


    }
}
