using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Relay
{
    public class RelaysLauncher : IHostedService, IDisposable
    {
        private Timer _relayTimer;

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly IRelayService _relayService;
        public RelaysLauncher(ILoggerFactory loggerFactory, IRelayService service)
        {
            _logger = loggerFactory.CreateLogger("Relay Launcher");
            _relayService = service;
        }
        #endregion


        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing resources.");
            _relayTimer?.Dispose();
        }
    }
}
