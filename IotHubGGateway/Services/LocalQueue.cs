using IotHubGateway.Contexts;
using IotHubGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway.Services
{
    public class LocalQueue
    {
        private readonly LocalContext localContext;
        private readonly AzureContext remoteContext;

        public LocalQueue(LocalContext LocalDbContext, AzureContext remoteDbContext)
        {
            this.localContext = LocalDbContext;
            this.remoteContext = remoteDbContext;
        }

        public Task CopyNewLocalMessagesToRemoteAsync(CancellationToken ct = default)
        {
            var newMessages = localContext.Messages.Where(x => x.IsProcessed == false).ToList();
            remoteContext.Messages.AddRangeAsync(newMessages, ct);
            var recordsCount = remoteContext.SaveChangesAsync(ct);
            Console.WriteLine(recordsCount);
            return Task.CompletedTask;
        }
    }
}
