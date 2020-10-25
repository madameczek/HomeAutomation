using IotHubGateway.Contexts;
using IotHubGateway.Controllers;
using IotHubGateway.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway
{
    class Program
    {
        static async Task<int> Main()
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
            services.AddDbContext<AzureContext>();
            services.AddTransient<LocalQueue>();
            services.AddTransient<MainController>();
            services.AddTransient<ServiceLauncher>();
            return services.BuildServiceProvider();
        }
    }
}
