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
        private LocalQueue localQueue;

        public MainController(LocalQueue localQueue)
        {
            this.localQueue = localQueue;
        }

        public async Task Run(CancellationToken ct = default)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // deal with queues: read one/write another

                    await localQueue.CopyNewLocalMessagesToRemoteAsync();
                    await Task.Delay(30000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
