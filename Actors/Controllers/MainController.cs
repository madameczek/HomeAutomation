using Actors.Contexts;
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
        private TemperatureSensorService temperatureSensorService;

        public MainController(TemperatureSensorService temperatureSensorService)
        {
            this.temperatureSensorService = temperatureSensorService;
        }

        public async Task Run(CancellationToken ct = default)
        {
            // uruchamia metody ReadData() z serwisów i pcha do bazy
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var message = temperatureSensorService.Read();
                    //context.Add(message);
                    //Console.WriteLine(this.context.Database.ProviderName);

                    await Task.Delay(3000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
        }
    }
}
