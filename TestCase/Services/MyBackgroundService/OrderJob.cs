using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Extensions;
using TestCase.Interfaces;
using TestCase.Models;

namespace TestCase.Services.MyBackgroundService
{
    public class OrderJob: IOrderJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWokerList workerList;
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly ILogService logService;

        public OrderJob(IServiceScopeFactory serviceScopeFactory, IWokerList workerList, IBackgroundTaskQueue taskQueue, ILogService logService)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this.workerList = workerList;
            this.taskQueue = taskQueue;     // очередь заданий
            this.logService = logService;   // сервис лога
        }
    
        public async Task ProcessOrders() 
        {

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var orders = orderService.GetNotWorkedOrders().Select(o=> new { Id = o.Id, System_type = o.System_type, Source_order = o.Source_order }).ToList();

                foreach (var o in orders)
                {
                    var woker = workerList.Wokers.FirstOrDefault(woker => string.Equals(woker.Name, o.System_type, StringComparison.OrdinalIgnoreCase));
                    if (woker == null)
                    {
                        Log.Error($"Не найден обработчик заказа {o.System_type}");
                        continue;
                    }
                    try
                    {
                        var converted_order_json = woker.Process(o.Source_order);
                        var order = await orderService.Get(o.Id);
                        order.Converted_order = converted_order_json;
                        await orderService.Save();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        // создаём задание для лога
                        Func<CancellationToken, Task> workItem = async (token) => {
                            if (token.IsCancellationRequested)
                                return;

                            Log.Information("Queued background log task is starting");
                            await logService.Add(ex.Message);
                            Log.Information("Queued background log task is complete");
                        };

                        // добавляем задание в очередь заданий
                        taskQueue.QueueBackgroundWorkItem(workItem);
                    }
                }
            }
        }
    }
}
