using Actors.Contexts;
using Actors.Controllers;
using Actors.Services;
using GsmModem;
using ImgwApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor;

namespace Actors
{
    class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // uncomment for debuging
            /*for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                await System.Threading.Tasks.Task.Delay(3000);
            }*/
#endif

            var cts = new CancellationTokenSource();
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                cts.Cancel();

            }; 

            IConfiguration configuration = GetConfigurationObject();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.GetSection("Serilog"))
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    AppDomain.CurrentDomain.BaseDirectory + @"/Actors-log-.txt",
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Day)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Error");
            }

            
            #region Finalizing
            Log.Logger.Information("Actor application is exiting...");
            Log.CloseAndFlush();
            cts.Dispose();
            #endregion
        }

        private static IConfiguration GetConfigurationObject()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory + @"/Properties/")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
            return configuration;
        }

        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                {
                    services.AddHostedService<Launcher>();
                    services.AddTransient<LocalQueue>();
                    services.AddSingleton<TemperatureSensorService>();
                    services.AddSingleton<ImgwService>();
                    services.AddSingleton<GsmModemService>();
                    services.AddTransient<MainController>();
                    services.AddTransient<ServiceLauncher>();
                    services.AddDbContext<LocalContext>();
                })
                .UseSerilog();
        }

    }
}
