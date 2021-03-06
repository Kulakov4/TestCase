using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestCase.Interfaces;
using TestCase.Models;

namespace TestCase.Services.MyBackgroundService.Wokers
{
    public class TalabatWoker: Woker
    {
        public TalabatWoker() : base("talabat") { }

        public override string Process(string order_json)
        {
            /*
            talabat - принимает JSON заказа и меняет все положительные цены в заказе на отрицательные. 
            Возвращает из-мененный заказ. 
            (Продукты которые нужно обработать содержаться в коллекции products. 
            Цены содержаться в поле paidPrice)
             */

            Log.Information("Talabat woker is running");

            var order = JsonSerializer.Deserialize<OrderDTO>(order_json);
            order.Products.ForEach(product => product.PaidPrice = (-1 * Convert.ToDecimal(product.PaidPrice)).ToString() );
            return JsonSerializer.Serialize(order);
        }
    }
}
