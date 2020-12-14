using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IotHubGatewayDaemon.Controllers;
using Microsoft.Extensions.Logging;

namespace IotHubGatewayDaemon.Services
{
    internal class ServiceLauncher
    {
        private readonly MainController _controller;
        private readonly ILogger _logger;

        public ServiceLauncher(ILogger<ServiceLauncher> logger, MainController controller)
        {
            _controller = controller;
            _logger = logger;
        }

        // Configure services
        public async Task ConfigureServicesAsync(CancellationToken ct = default)
        {
            var tasks = new List<Task>
            {
                //dummyService.ConfigureService(ct),
            };
            _logger.LogDebug("Gateway services configured");
            await Task.WhenAll(tasks);
        }

        // Start services
        public async Task StartServicesAsync(CancellationToken ct = default)
        {
            var tasks = new List<Task>
            {
                _controller.Run(ct)
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
