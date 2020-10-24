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
#if DEBUG
            // uncomment for debuging
            for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                await System.Threading.Tasks.Task.Delay(3000);
            }
#endif

            var cts = new CancellationTokenSource();
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                cts.Cancel();
            }; 
            
            var servicesProvider = RegisterServices();

            IConfiguration configuration = GetConfigurationObject();

            using (servicesProvider as IDisposable)
            {
                var serviceLauncher = servicesProvider.GetRequiredService<ServiceLauncher>();
                await serviceLauncher.ConfigureServicesAsync(cts.Token);
                await serviceLauncher.StartServicesAsync(cts.Token);
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
            services.AddDbContext<LocalContext>();
            services.AddTransient<LocalQueue>();
            services.AddSingleton<TemperatureSensorService>();
            services.AddSingleton<GsmModemService>();
            services.AddTransient<MainController>();
            services.AddTransient<ServiceLauncher>();
            return services.BuildServiceProvider();
        }

    }
}
