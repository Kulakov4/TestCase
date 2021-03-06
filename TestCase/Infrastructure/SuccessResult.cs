using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Infrastructure
{
    public class SuccessResult<T>
    {
        public string Message { get; set; }
        public T Value { get; set; }

        public SuccessResult(T value)
        {
            Value = value;
            Message = "success";
        }
    }
}
