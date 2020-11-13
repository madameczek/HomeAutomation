using GsmModem.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GsmModem
{
    public class GsmModemLauncher : IHostedService, IDisposable
    {
        private Timer _readModemTimer;
        private Task _modemReadTask;

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private IGsmModemHwSettings _hwSettings = new GsmModemHwSettings()
        {
            ReadInterval = 10,
        };

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
            _hwSettings = (IGsmModemHwSettings)await _gsmService.ConfigureService(cancellationToken);
            try
            {
                if (_hwSettings.Attach)
                {
                    _readModemTimer = new Timer(
                        ReadModem,
                        null,
                        TimeSpan.FromMilliseconds(1000),
                        TimeSpan.FromSeconds(_hwSettings.ReadInterval));
                }
                else
                {
                    _logger.LogDebug("Not attached");
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
            _modemReadTask = _gsmService.ReadDeviceAsync(_stoppingCts.Token);
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
                Task.WhenAll(_modemReadTask).Wait(cancellationToken);
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
