using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Relay.Interfaces;
using Shared.Models;

namespace Relay
{
    public class RelaysLauncher : IHostedService, IDisposable
    {
        private Timer _readApiTimer;
        private readonly List<Task> _tasks = new List<Task>();
        private List<Tuple<IRelayService, IHwSettings, Timer>> _relays = new List<Tuple<IRelayService, IHwSettings, Timer>>();

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private IHwSettings _relayHwSettings;
        private IHwSettings _sunsetHwSettings;

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly ISunriseSunsetService _sunsetService;
        private readonly IServiceProvider _serviceProvider;
        public RelaysLauncher(ILoggerFactory loggerFactory, ISunriseSunsetService sunsetService, IServiceProvider provider)
        {
            _logger = loggerFactory.CreateLogger("Relay Launcher");
            _sunsetService = sunsetService;
            _serviceProvider = provider;
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

            IEnumerable<IHwSettings> relaysSettingsList;
            using (var scope = _serviceProvider.CreateScope())
            {
                var relayService = scope.ServiceProvider.GetRequiredService<IRelayService>();
                relaysSettingsList = relayService.GetSettings();
            } 
            foreach (var settings in relaysSettingsList)
            {
                if (settings.Attach)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var relayService = scope.ServiceProvider.GetRequiredService<IRelayService>();
                    await relayService.ConfigureService(settings, cancellationToken);
                    _logger.LogInformation("Relay {Name} configured with read period: {RelayReadPeriod} sec.", settings.Name, settings.ReadInterval);
                    _ = relayService.Run(cancellationToken); // temporary solution
                    var relayTimer = new Timer(
                        Relay,
                        null,
                        TimeSpan.FromMilliseconds(80),
                        TimeSpan.FromSeconds(settings.ReadInterval));
                    _relays.Add(Tuple.Create(relayService, settings, relayTimer));
                }
                else
                {
                    _logger.LogTrace("Relay {Name} not initialized.", settings.Name);
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

        private void Relay(object state)
        {
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _readApiTimer?.Change(Timeout.Infinite, 0);
            _relays.ForEach(t=>t.Item3.Change(Timeout.Infinite, 0));
            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                _logger.LogInformation("Stopping.");
                Task.WhenAll(_tasks).Wait(cancellationToken);
            }
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing resources.");
            _readApiTimer?.Dispose();
            _relays.ForEach(t=>t.Item3?.Dispose());
        }
    }
}
