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
    public class ZomatoWoker : Woker
    {
        public ZomatoWoker() : base("zomato") { }

        public override string Process(string order_json)
        {
            /*
            zomato - принимает JSON заказа и делит все цены в заказе на количество позиций (price / quantity). 
            Возвраща-ет измененный заказ 
            (Продукты которые нужно обработать содержаться в коллекции products. Цены содер-жаться в поле paidPrice, количество в поле quantity)             
             */
            Log.Information("Zomato woker is running");

            var order = JsonSerializer.Deserialize<OrderDTO>(order_json);
            order.Products.ForEach(product => product.PaidPrice = (Convert.ToDecimal(product.PaidPrice) / Convert.ToDecimal(product.Quantity)).ToString());
            return JsonSerializer.Serialize(order);
        }
    }
}
