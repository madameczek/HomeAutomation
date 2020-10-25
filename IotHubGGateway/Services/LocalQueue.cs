using IotHubGateway.Contexts;
using IotHubGateway.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
