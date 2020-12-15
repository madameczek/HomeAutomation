using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Models;

namespace GsmModem
{
    public class GsmModemLauncher : IHostedService, IDisposable
    {
        private Timer _readModemTimer;
        private readonly List<Task> _tasks = new List<Task>();
        
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private IHwSettings _hwSettings; 

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly IGsmModemService _gsmService;
        public GsmModemLauncher(ILoggerFactory loggerFactory, IGsmModemService service)
        {
            _logger = loggerFactory.CreateLogger("GSM Launcher");
            _gsmService = service;
        }
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _hwSettings = _gsmService.GetSettings();
            try
            {
                if (_hwSettings.Attach)
                {
                    await _gsmService.ConfigureService(cancellationToken);

                    _readModemTimer = new Timer(
                        ReadModem,
                        null,
                        TimeSpan.FromMilliseconds(1000),
                        TimeSpan.FromSeconds(_hwSettings.ReadInterval));
                }
                else
                {
                    _logger.LogDebug("Service not initialized. Device not configured.");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Cancelled");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Starting service failed");
            }
        }

        private void ReadModem(object state)
        {
            _tasks.Add(_gsmService.ReadDeviceAsync(_stoppingCts.Token));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _readModemTimer?.Change(Timeout.Infinite, 0);
            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                _logger.LogDebug("Stopping");
                Task.WhenAll(_tasks).Wait(cancellationToken);
            }
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing resources.");
            _readModemTimer?.Dispose();
        }
    }
}
