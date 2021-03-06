using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Interfaces
{
    public interface ILogService
    {
        public Task Add(string message);
    }
}
