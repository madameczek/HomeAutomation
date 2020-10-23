using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureSensor
{
    public class TemperatureSensorMessageModel : IMessage
    {
        public Guid Guid { get; set; }
        public Guid DeviceGuid { get; set; }
        public DateTimeOffset MessageTime { get; set; }
        public string Data { get; set; }
        public IDictionary<string, string> DataPairs { get; set; }
    }
}
