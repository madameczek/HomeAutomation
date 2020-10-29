using CommonClasses;
using CommonClasses.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TemperatureSensor.Models;

namespace TemperatureSensor
{
    public class TemperatureSensorService : BaseService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public TemperatureSensorService(ILogger<TemperatureSensorService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public override string HwSettingsActorSection { get; } = "TemperatureSensor";
        private HwSettings _hwSettings = new HwSettings();

        private double _temperature;
        
        /*public Guid Guid { get; set; }
        public Guid DeviceGuid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Enum Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Data { get; set; }
        public IDictionary<string, string> DataPairs { get; set; }*/

        public override IMessage GetMessage()
        {
            // Cut milliseconds for shorter storage in json.
            DateTimeOffset _time = DateTimeOffset.Now;
            _time = _time.AddTicks(-(_time.Ticks % TimeSpan.TicksPerSecond));

            // Temporary object with readings to be serialized.
            IMessage _tempMessage = new TemperatureSensorData()
            {
                CreatedOn = _time,
                ActorId = _hwSettings.DeviceId,
                Temperature = _temperature
            };
            
            /*// Create json to be stored as string in MessageBody field of a Message.
            var _jsonData = JsonConvert.SerializeObject(_tempMessage, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            TemperatureSensorData _message = JsonConvert.DeserializeObject<TemperatureSensorData>(_jsonData);

            _message.Id = 0;
            _message.IsProcessed = false;
            _message.MessageBodyJson = _jsonData;
            _message.CreatedOn = _time;
            _message.Humidity = null;*/
            return _tempMessage;
        }

        public override IService ReadConfig()
        {
            throw new NotImplementedException();
        }

        public override async Task ConfigureService(CancellationToken ct = default)
        {
            try
            {
                if(!ct.IsCancellationRequested)
                {
                    _hwSettings = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).Get<HwSettings>();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
            await Task.CompletedTask;
        }

        public override bool Write(IMessage message)
        {
            throw new NotImplementedException();
        }

        public override async Task Run(CancellationToken ct = default)
        {
            // This periodically invokes a method reading temperature from a sensor.
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await ReadTempAsync(ct);
                    await Task.Delay(_hwSettings.ReadInterval, ct);
                }
            }
            catch(OperationCanceledException) { }
            catch(Exception) { throw; }
        }

        private async Task ReadTempAsync(CancellationToken ct = default)
        {
            try
            {
                string data = await Task.Run(() =>
                    File.ReadAllText(_hwSettings.BasePath + _hwSettings.HWSerial + @"/temperature"), ct);
                _temperature = int.Parse(data.Trim()) * 0.001;
            }
            catch (OperationCanceledException) { }
            catch (Exception e) { _logger.LogCritical(e, "Service TemperatureSensorService crashed"); throw; }
        }
    }
}
