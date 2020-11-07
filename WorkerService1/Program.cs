using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WorkerService1
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + @"/LogFile.txt")
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Error");
            }
            finally
            {
                Log.Logger.Information("Cancelled at Program");
                //await Task.CompletedTask;
            }
            //await Task.CompletedTask;
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                {
                    //services.AddHostedService<Worker>();
                    services.AddHostedService<Launcher>();
                    services.AddSingleton<Service1BusinessLayer>(); // nawet przy ustawieniu Transient zwrace zawsze ten sam obiekt
                    services.AddScoped<IScopedDataAccessLayer1, DataLayer>();
                    services.AddDbContext<Context>(opts => opts.UseSqlServer(
                        @"Data Source=.\SQLEXPRESS;Initial Catalog=PlanFoodTemp;Integrated Security=True;Connect Timeout=30"
                        ));
                })
                .UseSerilog();
        }
    }
}
