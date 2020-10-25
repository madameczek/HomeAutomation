using Actors.Services;
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

        public MainController(LocalQueue localQueue, TemperatureSensorService temperatureSensorService)
        {
            this.temperatureSensorService = temperatureSensorService;
            this.localQueue = localQueue;
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

                    await Task.Delay(30000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
