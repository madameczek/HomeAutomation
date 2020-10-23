using CommonClasses.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace CommonClasses
{
    public enum Type
    {
        Gateway,
        GsmModem,
        Relay,
        TempSensor,
        HumiditySensor,
        Input
    }
    public abstract class BaseActor : IActor
    {
        public string HwSettingsSection { get; } = "HWSettings";
        public abstract string HwSettingsActorSection { get; }
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public IDictionary<string, string> DataPairs { get; set; }
        public Enum Type { get; set; }

        public abstract IMessage GetMessage();
        public abstract IActor Read();

        public abstract IActor ReadConfig();

        public abstract Task ConfigureService(CancellationToken cancellationToken);

        public abstract bool Write(IMessage message);

        public abstract Task Run(CancellationToken cancellationToken);

        public bool Serialize()
        {
            try
            {
                Data = JsonConvert.SerializeObject(DataPairs);
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
                DataPairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
