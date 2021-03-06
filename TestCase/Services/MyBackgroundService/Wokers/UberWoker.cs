using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Services.MyBackgroundService.Wokers
{
    public class UberWoker : Woker
    {
        public UberWoker() : base("uber") { }

        public override string Process(string order_json)
        {
            /*
            uber - принимает JSON заказа и выбрасывает исключение          
             */
            Log.Information("Uber woker is running");

            throw new Exception("Uber woker exception");
        }
    }
}
