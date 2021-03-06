﻿using System;
using System.Threading;
using System.Threading.Tasks;
using IotHubGatewayDaemon.Contexts;
using IotHubGatewayDaemon.Controllers;
using IotHubGatewayDaemon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace IotHubGatewayDaemon
{
    class Program
    {
        private static async Task<int> Main()
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
                    AppDomain.CurrentDomain.BaseDirectory + @"/Gateway-Log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Month)
#if DEBUG
                .WriteTo.Console(
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}")
#endif
                .CreateLogger();

            IServiceProvider servicesProvider = RegisterServices(logger);

            try
            {
                using (servicesProvider as IDisposable)
                {
                    var serviceLauncher = servicesProvider.GetRequiredService<ServiceLauncher>();
                    try
                    {
                        await serviceLauncher.ConfigureServicesAsync(cts.Token);
                        await serviceLauncher.StartServicesAsync(cts.Token);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            #region Finalizing
            finally
            {
                logger.Information("Gateway is exiting...");
                logger.Dispose();
                cts.Dispose();
            }
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

        private static IServiceProvider RegisterServices(ILogger logger)
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
