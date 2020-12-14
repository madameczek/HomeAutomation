using IotHubGatewayDaemon.Contexts;

namespace IotHubGatewayDaemon.Services
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
