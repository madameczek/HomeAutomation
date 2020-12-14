using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor.Models;

namespace TemperatureSensor
{
    public class TemperatureSensorService : ITemperatureSensorService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        public TemperatureSensorService(ILoggerFactory logger, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger.CreateLogger("Temperature Sensor Service");
            _configuration = configuration;
            _services = services;
        }
        #endregion

        // Define variables for data fetched from 'appsettings.json'. Data are used to configure the service.
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "TemperatureSensor";
        private ITemperatureSensorHwSettings _hwSettings = new TemperatureSensorHwSettings();
        private readonly string _serviceSettings = "Services:DatabasePooling";
        private readonly string _messageToDbPushPeriodSeconds = "TemperatureMessagePushPeriod";

        private static readonly Object _temperatureLock = new object();
        private double _temperature;
        private bool _deviceReadingIsValid;

        public IHwSettings GetSettings()
        {
            return _hwSettings = _configuration.GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}").Get<TemperatureSensorHwSettings>();
        }

        public Task<IHwSettings> ConfigureService(CancellationToken cancellationToken)
        {
            try
            {
                _hwSettings = _configuration
                    .GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}").Get<TemperatureSensorHwSettings>(); // redundant
                var serviceSettings = _configuration.GetSection(_serviceSettings).Get<Dictionary<string, int>>();
                if (serviceSettings.TryGetValue(_messageToDbPushPeriodSeconds, out var value))
                {
                    if (value * 1000 < int.MaxValue)
                    {
                        (_hwSettings).DatabasePushPeriod = value;
                    }
                }
            }
            catch (Exception) { throw; }
            return Task.FromResult((IHwSettings)_hwSettings);
        }

        public async Task ReadDeviceAsync(CancellationToken ct)
        {
            try
            {
                string data = await Task.Run(() =>
                    File.ReadAllText(_hwSettings.BasePath + _hwSettings.HWSerial + @"/temperature"), ct);
                if (data != null)
                {
                    var result = int.TryParse(data.Trim(), out int _tempReading);
                    if (result)
                    {
                        lock (_temperatureLock)
                        {
                            _temperature = _tempReading * 0.001;
                        }
                        _deviceReadingIsValid = true;
                    }
                }
                else
                {
                    _logger.LogWarning("Temperature sensor error.");
                    _deviceReadingIsValid = false;
                }
            }
            catch (OperationCanceledException) { }
            catch (IOException) 
            {
                _logger.LogCritical(
                    "Can't find device directory {tempSensorDirectory}", 
                    (_hwSettings.BasePath + _hwSettings.HWSerial).ToString());
                _deviceReadingIsValid = false;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Service crashed");
                _deviceReadingIsValid = false;
                throw;
            }
        }

        public IMessage GetMessage()
        {
            // Cut milliseconds for shorter storage in message body as json.
            var time = DateTime.Now;
            time = time.AddTicks(-(time.Ticks % TimeSpan.TicksPerSecond));

            // Temporary object with readings to be serialized.
            IMessage tempMessage = new TemperatureSensorData()
            {
                Id = 0,
                CreatedOn = time,
                IsProcessed = false,
                ActorId = _hwSettings.DeviceId,
                Temperature = _temperature
            };
            return tempMessage;
        }

        public async Task SaveMessageAsync(CancellationToken ct)
        {
            if (_deviceReadingIsValid)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var scopedService = scope.ServiceProvider.GetRequiredService<LocalQueue>();
                    //_logger.LogDebug("Scoped service Hash: {LocalQueueHash}", scopedService.GetHashCode());
                    await scopedService.AddMessage(GetMessage(), typeof(TemperatureAndHumidity));
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Cancelled");
                }
            }
        }
    }
}
