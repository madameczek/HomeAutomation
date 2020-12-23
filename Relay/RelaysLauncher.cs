using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace Relay
{
    public class RelaysLauncher : IHostedService, IDisposable
    {
        private Timer _relayTimer1;
        private Timer _readApiTimer;
        private readonly List<Task> _tasks = new List<Task>();

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private IHwSettings _relayHwSettings;
        private IHwSettings _sunsetHwSettings;

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly IRelayService _relayService;
        private readonly ISunriseSunsetService _sunsetService;
        public RelaysLauncher(ILoggerFactory loggerFactory, IRelayService relayService, ISunriseSunsetService sunsetService)
        {
            _logger = loggerFactory.CreateLogger("Relay Launcher");
            _relayService = relayService;
            _sunsetService = sunsetService;

        }
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _sunsetHwSettings = _sunsetService.GetSettings();
            if (_sunsetHwSettings.Attach)
            {
                await _sunsetService.ConfigureService(cancellationToken);
                _readApiTimer = new Timer(
                    FetchAndStoreSunsetTime,
                    null,
                    TimeSpan.FromMilliseconds(200),
                    TimeSpan.FromHours(_sunsetHwSettings.ReadInterval));
                _logger.LogInformation("Sunset API configured with read&save period: {WeatherReadPeriod} hours.", _sunsetHwSettings.ReadInterval);
            }
            else
            {
                _logger.LogDebug("Sunset API not initialized.");
            }

            var relaysSettingsList  = _relayService.GetSettings();
            foreach (var settings in relaysSettingsList)
            {
                if (settings.Attach)
                {
                    await _relayService.ConfigureService(settings, cancellationToken);
                }
            }
            
        }

        private void FetchAndStoreSunsetTime(object state)
        {
            var readApiTask = _sunsetService.ReadDeviceAsync(_stoppingCts.Token);
            _tasks.Add(readApiTask);
            readApiTask.Wait(_stoppingCts.Token);
            if(readApiTask.Exception != null) return;
            var saveReadingToDatabaseTask = _sunsetService.SaveMessageAsync(_stoppingCts.Token);
            _tasks.Add(saveReadingToDatabaseTask);
            saveReadingToDatabaseTask.Wait(_stoppingCts.Token);

        }

        private static void Timer1(object state)
        {

        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _readApiTimer?.Change(Timeout.Infinite, 0);
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
            _logger.LogInformation("Disposing resources.");
            _relayTimer1?.Dispose();
            _readApiTimer?.Dispose();
        }
    }
}
