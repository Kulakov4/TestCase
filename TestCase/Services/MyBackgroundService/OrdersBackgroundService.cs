using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Interfaces;

namespace TestCase.Services.MyBackgroundService
{
    public class OrdersBackgroundService : BackgroundService
    {
        
        private readonly IOrderJob job;

        public OrdersBackgroundService(IOrderJob job)
        {
            this.job = job;
        }
        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("OrdersBackgroundService is starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                await job.ProcessOrders();    // Обрабатываем заказы
                await Task.Delay(5000);
            }
        }
    }
}
