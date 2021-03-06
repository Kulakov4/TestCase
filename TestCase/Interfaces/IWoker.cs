using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Interfaces
{
    public interface IWoker
    {
        public string Name { get; set; }
        public string Process(string order_json);
    }
}
