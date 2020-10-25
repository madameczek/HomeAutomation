using Actors.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor;

namespace Actors.Controllers
{
    public class MainController
    {
        private readonly TemperatureSensorService temperatureSensorService;
        private readonly LocalQueue localQueue;
        private readonly IConfiguration configuration;

        public MainController(IConfiguration configuration, LocalQueue localQueue, TemperatureSensorService temperatureSensorService)
        {
            this.temperatureSensorService = temperatureSensorService;
            this.localQueue = localQueue;
            this.configuration = configuration;
        }

        // Define section of appsettings.json to parse device config from configuration object
        private string ServicesSection { get; } = "Services";
        private string DatabaseSection { get; } = "DatabasePolling";
        private int temperatureMessagePushPeriod = 30000;

        public async Task ConfigureService(CancellationToken ct = default)
        {
            try
            {
                if (!ct.IsCancellationRequested)
                {
                    var settings = configuration.GetSection(ServicesSection).GetSection(DatabaseSection).Get<Dictionary<string, int>>();
                    if (settings.TryGetValue("TemperatureMessagePushPeriod", out var value))
                    {
                        if (value * 1000 < int.MaxValue)
                        {
                            temperatureMessagePushPeriod = value * 1000;
                        }
                        else
                        {
                            temperatureMessagePushPeriod = 30000;
                        }
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
            await Task.CompletedTask;
        }

        public async Task Run(CancellationToken ct = default)
        {
            // uruchamia metody ReadData() z serwisów i pcha do bazy
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // Wait for first readings from hardware devices
                    await Task.Delay(3000, ct);
                    var message = temperatureSensorService.GetMessage();
                    localQueue.AddMessage(message);

                    await Task.Delay(temperatureMessagePushPeriod, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
