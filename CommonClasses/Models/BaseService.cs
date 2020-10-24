using CommonClasses.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace CommonClasses.Models
{
    public enum DeviceType
    {
        Gateway,
        GsmModem,
        Relay,
        TemperatureSensor,
        HumiditySensor,
        Input
    }
    public abstract class BaseService : IService
    {
        public string HwSettingsSection { get; } = "HWSettings";
        public abstract string HwSettingsActorSection { get; }
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public IDictionary<string, string> ConfigurationJson { get; set; }
        public Enum Type { get; set; }

        public abstract IMessage GetMessage();
        public abstract IService Read();

        public abstract IService ReadConfig();

        public abstract Task ConfigureService(CancellationToken cancellationToken);

        public abstract bool Write(IMessage message);

        public abstract Task Run(CancellationToken cancellationToken);

        public bool Serialize()
        {
            try
            {
                Data = JsonConvert.SerializeObject(ConfigurationJson);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Deserialize()
        {
            try
            {
                ConfigurationJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
