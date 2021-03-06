using TestCase.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TestCase.Data;
using TestCase.Interfaces;
using TestCase.Models;
using TestCase.Services.MyBackgroundService;
using TestCase.Services.MyBackgroundService.Wokers;
using TestCase.Services.QueuedBackgroundService;
using System.Reflection;
using System.IO;
using System;

namespace TestCase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddControllers();

            services.AddScoped<OrderService>();

            services.AddScoped<ICrudService<Order>, OrderService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<ILogService, LogService>();

            var logFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.log";
            var logPath = Path.Combine(AppContext.BaseDirectory, logFile);
            services.AddSingleton(_ => new LogServiceArgs { FileName = logPath });
            services.AddSingleton<ILogService, LogService>();

            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddSingleton<IWokerList, WokerList>();
            services.AddSingleton<IOrderJob, OrderJob>();

            services.AddHostedService<QueuedHostedService>();       // фоновый сервис работающий с очередью заданий
            services.AddHostedService<OrdersBackgroundService>();   // фоновый сервис обрабатывающий заказы
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging(); 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
