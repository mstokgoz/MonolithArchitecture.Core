using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.CrossCuttingConcerns.Exceptions
{
    public class ValidationException : Exception
    {
        public object[] Errors;
        public ValidationException(string message, params object[] errors) : base(message)
        {
            Errors = errors;
        }
    }
}
