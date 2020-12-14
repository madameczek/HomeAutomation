using System;
using System.Threading;
using System.Threading.Tasks;
using IotHubGatewayDaemon.Services;
using Microsoft.Extensions.Configuration;

namespace IotHubGatewayDaemon.Controllers
{
    public class MainController
    {
        private readonly IConfiguration _configuration;
        private readonly LocalQueue _localQueue;
        public MainController(LocalQueue localQueue, IConfiguration configuration) 
        {
            _localQueue = _localQueue = localQueue;
            _configuration = configuration;
        }

        private int Delay { get; set; } = 30000;

        // This all is to be reworked for using timers
        public async Task Run(CancellationToken ct = default)
        {
            Delay = int.Parse(_configuration.GetSection("RemoteDb:CopyToRemotePeriod").Value) * 1000;

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // deal with queues: read one/write another
                    try 
                    { 
                        await _localQueue.CopyNewLocalMessagesToRemote(ct);
                        await _localQueue.CopyNewLocalTemperaturesToRemote(ct);
                        await _localQueue.CopyNewLocalWeatherReadingsToRemote(ct);
                    }
                    catch { Console.WriteLine("Czekamy na kolejną szansę"); }
                    await Task.Delay(60000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
