using DataAccessLayer;
using DataAccessLayer.Contexts;
using GsmModem;
using ImgwApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemperatureSensor;

namespace ActorsDaemon
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // uncomment for remote debuging
            for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                Task.Delay(3000).Wait();
            }
#endif

            try
            {
                var host = CreateHostBuilder(args).Build();
                var configuration = host.Services.GetRequiredService<IConfiguration>();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                Log.Information("Actor application is starting up");

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application error");
            }
            finally
            {
                Log.Logger.Information("Actor application is shutting down");
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((config)=>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory + @"/Properties/");
                    config.AddJsonFile("gsmmodem.json");
                })
                .ConfigureServices((services) =>
                {
                    services.AddHostedService<TemperatureSensorLauncher>();
                    services.AddHostedService<ImgwLauncher>();
                    services.AddHostedService<GsmModemLauncher>();
                    services.AddScoped<LocalQueue>();
                    services.AddSingleton<ITemperatureSensorService, TemperatureSensorService>();
                    services.AddSingleton<IImgwService, ImgwService>();
                    services.AddSingleton<IGsmModemService, GsmModemService>();
                    services.AddDbContext<LocalContext>();
                    services.AddTransient<IPortProvider, PortProvider>();
                    services.AddTransient<IModemDevice, ModemDevice>();
                })
                .UseSerilog();
        }
    }
}
