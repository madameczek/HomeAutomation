﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor.Models;

namespace TemperatureSensor
{
    public class TemperatureSensorLauncher : IHostedService, IDisposable
    {
        private Timer _readSensorTimer;
        private Timer _saveReadingToDatabaseTimer;
        //private Task _readSensorTask;
        //private Task _saveReadingToDatabaseTask;
        private readonly List<Task> _tasks = new List<Task>();

        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private ITemperatureSensorHwSettings _hwSettings = new TemperatureSensorHwSettings() 
        { 
            ReadInterval = 30, 
            DatabasePushPeriod = 1200 
        };

        #region Ctor & Dependency Injection
        private readonly ILogger _logger;
        private readonly ITemperatureSensorService _temperatureSensorService;
        public TemperatureSensorLauncher(ILoggerFactory loggerFactory, ITemperatureSensorService service)
        {
            _logger = loggerFactory.CreateLogger("Temperature Sensor Launcher");
            _temperatureSensorService = service;
        }
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _hwSettings = (ITemperatureSensorHwSettings)_temperatureSensorService.GetSettings();
            try
            {
                if (_hwSettings.Attach)
                {
                    _ = (ITemperatureSensorHwSettings)await _temperatureSensorService.ConfigureService(cancellationToken);

                    _readSensorTimer = new Timer(
                        ReadSensor,
                        null,
                        TimeSpan.FromMilliseconds(100),
                        TimeSpan.FromSeconds(_hwSettings.ReadInterval));
                    _saveReadingToDatabaseTimer = new Timer(
                        SaveToDatabase,
                        null,
                        // Wait for sensor data before save to database
                        TimeSpan.FromMilliseconds(3000),
                        TimeSpan.FromSeconds(_hwSettings.DatabasePushPeriod));
                    _logger.LogDebug("Configured with sensor read period: {sensorReadPeriod}sec.", _hwSettings.ReadInterval);
                    _logger.LogDebug("Configured with database save period: {databasePushPeriod}sec", _hwSettings.DatabasePushPeriod);
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

        private void ReadSensor(object state)
        {
            //var readSensorTask = _temperatureSensorService.ReadDeviceAsync(_stoppingCts.Token);
            _tasks.Add(_temperatureSensorService.ReadDeviceAsync(_stoppingCts.Token));
        }

        private void SaveToDatabase(object state)
        {
            //var saveReadingToDatabaseTask = _temperatureSensorService.SaveMessageAsync(_stoppingCts.Token);
            _tasks.Add(_temperatureSensorService.SaveMessageAsync(_stoppingCts.Token));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _readSensorTimer?.Change(Timeout.Infinite, 0);
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
            _readSensorTimer?.Dispose();
            _saveReadingToDatabaseTimer?.Dispose();
        }
    }
}
