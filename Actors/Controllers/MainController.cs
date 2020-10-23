using Actors.Services;
using GsmModem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor;

namespace Actors.Controllers
{
    class MainController
    {
        private GsmModemService gsmModemService;
        private TemperatureSensorService temperatureSensorService;
        private LocalQueue localQueue;

        public MainController(GsmModemService gsmModemService, TemperatureSensorService temperatureSensorService, LocalQueue localQueue)
        {
            this.gsmModemService = gsmModemService;
            this.temperatureSensorService = temperatureSensorService;
            this.localQueue = localQueue;
        }

        // configure services
        public async Task ConfigureServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                temperatureSensorService.ConfigureService(ct),
            };
            await Task.WhenAll(tasks);
        }

        public async Task StartServicesAsync(CancellationToken ct = default)
        {
            List<Task> tasks = new List<Task>
            {
                temperatureSensorService.Run(ct),
                localQueue.Run(ct)
            };
            await Task.WhenAll(tasks);
        }
    }
}
