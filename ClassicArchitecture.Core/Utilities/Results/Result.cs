using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class Result : IResult
    {
        public RESULTTYPE Success { get; }

        public string? Message { get; }

        public Result(RESULTTYPE success, string? message) : this(success)
        {
            this.Message = message;
        }
        public Result(RESULTTYPE success)
        {
            this.Success = success;
        }

        public Result()
        {

        }

    }
}
