using IotHubGateway.Controllers;
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

        public ServiceLauncher(MainController controller)
        {
            this.controller = controller;
        }

        // configure services
        public async Task ConfigureServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                //dummyService.ConfigureService(ct),
            };
            await Task.WhenAll(tasks);
        }

        public async Task StartServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                controller.Run(ct)
            };
            await Task.WhenAll(tasks);
        }
    }
}
