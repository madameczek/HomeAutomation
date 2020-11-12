using IotHubGateway.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway.Controllers
{
    public class MainController
    {
        IConfiguration _configuration;
        private readonly LocalQueue _localQueue;
        public MainController(LocalQueue localQueue, IConfiguration configuration) 
        {
            _localQueue = _localQueue = localQueue;
            _configuration = configuration;
        }

        private int Delay { get; set; } = 30000;


        public async Task Run(CancellationToken ct = default)
        {
            Delay = int.Parse(_configuration.GetSection("RemoteDb:CopyToRemotePeriod").Value) * 1000;

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // deal with queues: read one/write another

                    await _localQueue.CopyNewLocalMessagesToRemote(ct);
                    await _localQueue.CopyNewLocalTemperaturesToRemote(ct);
                    await _localQueue.CopyNewLocalWeatherReadingsToRemote(ct);
                    await Task.Delay(60000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
