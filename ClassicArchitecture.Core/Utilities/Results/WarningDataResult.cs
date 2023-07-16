using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class WarningDataResult<T> : DataResult<T>
    {

        public WarningDataResult(T data, string message) : base(data, RESULTTYPE.ERROR, message)
        {

        }

        public WarningDataResult(T data) : base(data, RESULTTYPE.ERROR)
        {

        }

        public WarningDataResult(string message) : base(default, RESULTTYPE.ERROR, message)
        {

        }
    }
}
