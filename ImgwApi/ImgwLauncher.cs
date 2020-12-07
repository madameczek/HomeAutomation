﻿using ImgwApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImgwApi
{
    public class ImgwLauncher : IHostedService, IDisposable
    {
        private Timer _readImgwTimer;
        //private Timer _saveReadingToDatabaseTimer;
        //private Task _readImgwTask;
        //private Task _saveReadingToDatabaseTask;
        private readonly List<Task> _tasks = new List<Task>();

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private IHwSettings _hwSettings = new ImgwHwSettings() 
        { 
            ReadInterval = 20 
        };

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly IImgwService _imgwService;
        public ImgwLauncher(ILoggerFactory loggerFactory, IImgwService service)
        {
            _logger = loggerFactory.CreateLogger("IMGW Launcher");
            _imgwService = service;
        }
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _hwSettings = await _imgwService.ConfigureService(cancellationToken);
                if (_hwSettings.Attach)
                {
                    _readImgwTimer = new Timer(
                        FetchAndStoreWeather,
                        null,
                        TimeSpan.FromMilliseconds(100),
                        TimeSpan.FromMinutes(_hwSettings.ReadInterval));
                    _logger.LogDebug("Configured with read&save period: {WeatherReadPeriod}min", _hwSettings.ReadInterval);
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
            catch(Exception e)
            {
                _logger.LogCritical(e, "Starting service failed");
            }
        }

        private void FetchAndStoreWeather(object state)
        {
            var readImgwTask = _imgwService.ReadDeviceAsync(_stoppingCts.Token);
            _tasks.Add(readImgwTask);
            readImgwTask.Wait(_stoppingCts.Token);
            if (readImgwTask.Exception != null) return;
            var saveReadingToDatabaseTask = _imgwService.SaveMessageAsync(_stoppingCts.Token);
            _tasks.Add(readImgwTask);
            saveReadingToDatabaseTask.Wait(_stoppingCts.Token);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _readImgwTimer?.Change(Timeout.Infinite, 0);
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
            _readImgwTimer.Dispose();
        }
    }
}
