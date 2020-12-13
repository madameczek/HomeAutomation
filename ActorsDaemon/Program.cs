using DataAccessLayer;
using DataAccessLayer.Contexts;
using GsmModem;
using ImgwApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using TemperatureSensor;

namespace ActorsDaemon
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // uncomment for debuging
            /*for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                Task.Delay(3000).Wait();
            }*/
#endif
            Log.Logger = new LoggerConfiguration()
                //.ReadFrom.Configuration(configuration.GetSection("Serilog"))
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    AppDomain.CurrentDomain.BaseDirectory + @"/Actors-log-.txt",
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Month)
#if DEBUG
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Debug)
#endif
                .CreateLogger();

            try
            {
                Log.Information("Actor application is starting up");
                CreateHostBuilder(args).Build().Run();
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
