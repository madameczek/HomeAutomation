﻿using System;
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
                _logger.LogDebug("Sunset API configured with read&save period: {WeatherReadPeriod} hours.", _sunsetHwSettings.ReadInterval);
            }
            else
            {
                _logger.LogDebug("Sunset API not initialized.");
            }

            var a  = _relayService.GetSettings();
            //if (_relayHwSettings.Attach)
            {
                //await _relayService.ConfigureService(cancellationToken);

            }
        }

        private static void FetchAndStoreSunsetTime(object state)
        {

        }

        private static void Timer1(object state)
        {

        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing resources.");
            _relayTimer1?.Dispose();
        }
    }
}
