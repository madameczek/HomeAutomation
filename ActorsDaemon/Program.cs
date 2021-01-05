using DataLayer;
using GsmModem;
using ImgwApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading.Tasks;
using Relay;
using Relay.Interfaces;
using Serilog.Configuration;
using Shared;
using TemperatureSensor;

namespace ActorsDaemon
{
    public static class Program
    {
        public static void Main()
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
                var host = CreateHostBuilder().Build();
                var configuration = host.Services.GetRequiredService<IConfiguration>();
                Environment.SetEnvironmentVariable("PROGRAMDATA", AppDomain.CurrentDomain.BaseDirectory);

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

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((config)=>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory + @"/Properties/");
                    config.AddJsonFile("gsmmodem.json");
                })
                .ConfigureServices((services) =>
                {
                    services.AddHttpClient<ISunriseSunsetService, SunriseSunsetService>();
                    services.AddHttpClient<IService, ImgwService>();
                    services.AddHostedService<TemperatureSensorLauncher>();
                    services.AddHostedService<ImgwLauncher>();
                    services.AddHostedService<GsmModemLauncher>();
                    services.AddHostedService<RelaysLauncher>();
                    services.AddScoped<LocalQueue>();
                    services.AddSingleton<ITemperatureSensorService, TemperatureSensorService>();
                    services.AddSingleton<IService, ImgwService>();
                    services.AddSingleton<IGsmModemService, GsmModemService>();
                    services.AddDbContext<LocalContext>();
                    services.AddTransient<IPortProvider, PortProvider>();
                    services.AddTransient<IModemDevice, ModemDevice>();
                    services.AddScoped<IRelayService, RelayService>();
                    services.AddSingleton<ISunriseSunsetService, SunriseSunsetService>();
                    services.AddSingleton<GpioService>();
                    services.AddTransient<IRelayDevice, RelayDevice>();
                })
                .UseSerilog();
        }
    }
}
