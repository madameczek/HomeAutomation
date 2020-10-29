using IotHubGateway.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway.Controllers
{
    public class MainController
    {
        private readonly LocalQueue _localQueue;
        public MainController(LocalQueue localQueue) => _localQueue = localQueue;

        public async Task Run(CancellationToken ct = default)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // deal with queues: read one/write another

                    _ = _localQueue.CopyNewLocalMessagesToRemote(ct);
                    await Task.Delay(60000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
