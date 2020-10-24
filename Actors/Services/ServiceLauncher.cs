using Actors.Controllers;
using Actors.Models.LocalDbModels;
using Actors.Services;
using CommonClasses.Models;
using GsmModem;
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
        private GsmModemService gsmModemService;
        private TemperatureSensorService temperatureSensorService;
        private MainController controller;

        public ServiceLauncher(GsmModemService gsmModemService, TemperatureSensorService temperatureSensorService, MainController localQueue)
        {
            this.gsmModemService = gsmModemService;
            this.temperatureSensorService = temperatureSensorService;
            this.controller = localQueue;
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
                controller.Run(ct)
            };
            await Task.WhenAll(tasks);
        }
    }
}
