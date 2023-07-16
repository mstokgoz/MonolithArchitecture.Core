using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public interface IResult
    {
        RESULTTYPE Success { get; }
        string? Message { get; }

    }
}
