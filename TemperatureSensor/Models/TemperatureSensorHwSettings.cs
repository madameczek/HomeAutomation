using Shared;
using Shared.Models;
using System;

namespace TemperatureSensor.Models
{
    public class TemperatureSensorHwSettings : ITemperatureSensorHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceType Type { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }

        /// <summary>
        /// Interval between subsequent readings of temperature sensor in seconds.
        /// If can't be read from config file, default value is applied.
        /// Default value is set in service's launcher object.
        /// </summary>
        public int ReadInterval { get; set; }
        public string Interface { get; set; }
        public string BasePath { get; set; }
        public string HWSerial { get; set; }
        public int DatabasePushPeriod { get; set; }
    }
}
