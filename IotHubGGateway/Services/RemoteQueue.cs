using IotHubGateway.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IotHubGateway.Services
{
    class RemoteQueue
    {
        private readonly LocalContext localContext;
        private readonly AzureContext remoteContext;

        public RemoteQueue(LocalContext LocalDbContext, AzureContext remoteDbContext)
        {
            this.localContext = LocalDbContext;
            this.remoteContext = remoteDbContext;
        }
    }
}
