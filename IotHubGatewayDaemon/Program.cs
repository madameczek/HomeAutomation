using IotHubGateway.Contexts;
using IotHubGateway.Controllers;
using IotHubGateway.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Serilog;
using System.Diagnostics;

namespace IotHubGateway
{
    class Program
    {
        static async Task<int> Main()
        {
#if DEBUG
            // Uncomment for debuging.
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

            Logger logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.GetSection("Serilog"))
                .MinimumLevel.Debug()
                .WriteTo.File(
                    AppDomain.CurrentDomain.BaseDirectory + @"/Gateway-log-.txt",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Day)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            IServiceProvider servicesProvider = RegisterServices(logger);

            using (servicesProvider as IDisposable)
            {
                var serviceLauncher = servicesProvider.GetRequiredService<ServiceLauncher>();
                await serviceLauncher.ConfigureServicesAsync(cts.Token);
                await serviceLauncher.StartServicesAsync(cts.Token);
            }

            #region Finalizing
            logger.Information("Gateway is exiting...");
            logger.Dispose();
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

        private static IServiceProvider RegisterServices(Logger logger)
        {
            var services = new ServiceCollection();
            services.AddSingleton(GetConfigurationObject());
            services.AddDbContext<LocalContext>();
            services.AddDbContext<AzureContext>();
            services.AddTransient<LocalQueue>();
            services.AddTransient<MainController>();
            services.AddTransient<ServiceLauncher>();
            services.AddLogging(builder =>
            {
                builder.AddSerilog(logger: logger, dispose: false);
            });
            return services.BuildServiceProvider();
        }
    }
}
