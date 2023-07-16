using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }
        public DataResult(T data)
        {
            this.Data = data;
        }

        public DataResult(T data, RESULTTYPE success) : base(success)
        {
            this.Data = data;

        }

        public DataResult(T data, RESULTTYPE success, string message) : base(success, message)
        {
            this.Data = data;
        }
    }
}
    