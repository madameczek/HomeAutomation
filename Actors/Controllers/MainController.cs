using Actors.Services;
using CommonClasses.Interfaces;
using ImgwApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly TemperatureSensorService _temperatureSensorService;
        private readonly ImgwService _imgwService;
        private readonly LocalQueue _localQueue;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public MainController(ILogger<MainController> logger, IConfiguration configuration, LocalQueue localQueue, TemperatureSensorService temperatureSensorService, ImgwService imgwService)
        {
            _localQueue = localQueue;
            _temperatureSensorService = temperatureSensorService;
            _configuration = configuration;
            _logger = logger;
            _imgwService = imgwService;
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
                    var settings = _configuration.GetSection(ServicesSection).GetSection(DatabaseSection).Get<Dictionary<string, int>>();
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
            catch (Exception e) { _logger.LogError(e.Message, "Error at configuring Controller. Are values in configuration file correcr?"); throw; }
            await Task.CompletedTask;
        }

        public async Task Run(CancellationToken ct = default)
        {
            // Create messages periodically and store in local database.
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // Wait for first readings from hardware devices
                    await Task.Delay(3000, ct);
                    IMessage message = _temperatureSensorService.GetMessage();
                    _localQueue.AddMessage(message);
                    _localQueue.AddWeatherData(_imgwService.GetMessage());

                    await Task.Delay(temperatureMessagePushPeriod, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
