using Shared;
using Shared.Models;
using System;

namespace TemperatureSensor.Models
{
    public class TemperatureSensorHwSettings : IHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }

        /// <summary>
        /// Interval between subsequent readings of temperature sensor in seconds.
        /// </summary>
        public int ReadInterval { get; set; }
        public string Interface { get; set; }
        public string BasePath { get; set; }
        public string HwSerial { get; set; }

        /// <summary>
        /// Interval between subsequent inserts of temperature sensor messages into a local database.
        /// Expressed in minutes.
        /// </summary>
        public int DatabasePushPeriod { get; set; }
    }
}
