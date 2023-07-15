using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(RESULTTYPE.SUCCESS, message)
        {

        }

        public SuccessResult() : base(RESULTTYPE.SUCCESS)
        {

        }

    }
}
