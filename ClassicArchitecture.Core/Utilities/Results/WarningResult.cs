using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class WarningResult : Result
    {
        public WarningResult(string? message) : base(RESULTTYPE.WARNING, message)
        {

        }

        public WarningResult() : base(RESULTTYPE.WARNING)
        {

        }


    }
}
