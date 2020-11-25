using IotHubGateway.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway.Services
{
    class ServiceLauncher
    {
        private readonly MainController controller;
        private readonly ILogger _logger;

        public ServiceLauncher(ILogger<ServiceLauncher> logger, MainController controller)
        {
            this.controller = controller;
            _logger = logger;
        }

        // Configure services
        public async Task ConfigureServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                //dummyService.ConfigureService(ct),
            };
            _logger.LogDebug("Gateway services configured");
            await Task.WhenAll(tasks);
        }

        // Start services
        public async Task StartServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                controller.Run(ct)
            };
            _logger.LogInformation("Gateway services started");
            try
            {
                await Task.WhenAll(tasks);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error in Controller");
                throw;
            }
        }
    }
}
