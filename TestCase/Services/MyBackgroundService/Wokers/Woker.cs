using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Interfaces;
using TestCase.Models;

namespace TestCase.Services.MyBackgroundService.Wokers
{
    abstract public class Woker : IWoker
    {
        public string Name { get; set; }
        public Woker(string name)
        {
            Name = name;
        }
        public abstract string Process(string order_json);
    }
}
