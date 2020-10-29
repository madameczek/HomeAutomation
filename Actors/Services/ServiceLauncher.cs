using Actors.Controllers;
using Actors.Models.LocalDbModels;
using Actors.Services;
using GsmModem;
using ImgwApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor;

namespace Actors.Services
{
    class ServiceLauncher
    {
        private GsmModemService _gsmModemService;
        private TemperatureSensorService _temperatureSensorService;
        private ImgwService _imgwApiService;
        private MainController _controller;
        private readonly ILogger _logger;

        public ServiceLauncher(
            ILogger<ServiceLauncher> logger, 
            GsmModemService gsmModemService, 
            TemperatureSensorService temperatureSensorService, 
            ImgwService imgwService,
            MainController controller)
        {
            _logger = logger;
            _gsmModemService = gsmModemService;
            _temperatureSensorService = temperatureSensorService;
            _imgwApiService = imgwService;
            _controller = controller;
        }

        // configure services
        public async Task ConfigureServicesAsync(CancellationToken ct = default)
        {
            try
            {
                List<Task> tasks = new List<Task>
                {
                    _controller.ConfigureService(ct),
                    _temperatureSensorService.ConfigureService(ct),
                    _imgwApiService.ConfigureService(ct)
                };
                _logger.LogDebug("Actors services configured.");
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException) { }
        }

        public async Task StartServicesAsync(CancellationToken ct = default)
        {
            try
            {
                List<Task> tasks = new List<Task>
                {
                    _temperatureSensorService.Run(ct),
                    //_imgwApiService.Run(ct),
                    _controller.Run(ct)
                };
                _logger.LogInformation("Actors services started.");
                await Task.WhenAll(tasks);
                await Task.FromCanceled(ct); // This occurs only when cancell occured in any of tasks?
            }
            catch(OperationCanceledException e) { _logger.LogDebug(e.Message, "Cancelled at OperationCanceledException in StartServicesAsync."); }
        }
    }
}
