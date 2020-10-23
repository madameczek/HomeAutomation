using CommonClasses.Interfaces;
using Actors.Contexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using GsmModem;
using Actors.Services;
using Actors.Controllers;

namespace Actors
{
    class Program
    {
        static async Task<int> Main()
        {
            var cts = new CancellationTokenSource();
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                cts.Cancel();
            };
#if DEBUG
            // uncomment for debuging
            /*for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                await System.Threading.Tasks.Task.Delay(3000);
            }*/
#endif

            var servicesProvider = RegisterServices();

            IConfiguration configuration = GetConfigurationObject();
            var hwConfigurations = configuration.GetSection("HWSetup").GetSection("TemperatureSensor").Get<TemperatureSensor.Models.HwSettings>();


            using (servicesProvider as IDisposable)
            {
                var mainController = servicesProvider.GetRequiredService<MainController>();
                await mainController.ConfigureServicesAsync(cts.Token);
                await mainController.StartServicesAsync(cts.Token);
            }

            #region Finalizing
            Console.WriteLine("Exiting...");
            cts.Dispose();
            return 0;
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

        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(GetConfigurationObject());
            services.AddTransient<MainController>();
            services.AddDbContext<LocalContext>();
            services.AddSingleton<TemperatureSensorService>();
            services.AddSingleton<GsmModemService>();
            services.AddTransient<LocalQueue>();
            return services.BuildServiceProvider();
        }

    }
}
