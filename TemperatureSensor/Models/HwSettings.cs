using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureSensor.Models
{
    public class HwSettings
    {
        public string DeviceId { get; set; }
        public string Interface { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string BasePath { get; set; }
        public string HWSerial { get; set; }
        public int ReadInterval { get; set; }
        public bool Attach { get; set; }
    }
}
