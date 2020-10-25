using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CommonClasses;
using TemperatureSensor.Models;
using CommonClasses.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace TemperatureSensor
{
    public class TemperatureSensorService : BaseService
    {
        #region Dependency Injection
        private readonly IConfiguration configuration;
        public TemperatureSensorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public override string HwSettingsActorSection { get; } = "TemperatureSensor";
        HwSettings hwSettings = new HwSettings();

        private double temperature;
        
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
            // to be simplified
            DateTimeOffset time = DateTimeOffset.Now;
            time = time.AddTicks(-(time.Ticks % TimeSpan.TicksPerSecond));

            IMessage _message = new TemperatureSensorData()
            {
                CreatedOn = time,
                ActorId = hwSettings.DeviceId,
                Temperature = temperature
            };
            var _jsonData = JsonConvert.SerializeObject(_message, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            IMessage message = JsonConvert.DeserializeObject<TemperatureSensorMessage>(_jsonData);
            message.Id = 0;
            message.IsProcessed = false;
            message.MessageBodyJson = _jsonData;
            message.CreatedOn = time;
            return message;
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
                    hwSettings = configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).Get<HwSettings>();
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
            // metoda periodycznie odczytuje DS1820 i aktualizuje właściwość
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await ReadTemp(ct);
                    await Task.Delay(hwSettings.ReadInterval, ct);
                }
            }
            catch(OperationCanceledException) { }
            catch(Exception) { throw; }
        }

        public override IService Read()
        {
            throw new NotImplementedException();
        }

        private async Task ReadTemp(CancellationToken ct = default)
        {
            try
            {
                string data = await Task.Run(() =>
                    File.ReadAllText(hwSettings.BasePath + hwSettings.HWSerial + @"/temperature"), ct);
                temperature = (int.Parse(data.Trim()) * 0.001);
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
        }
    }
}
