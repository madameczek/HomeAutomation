using System;
using System.Collections.Generic;
using System.Text;
using Shared.Models;

namespace Relay.Models
{
    internal class SunriseSunsetHwSettings : IHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }
        public string Interface { get; set; }

        /// <summary>
        /// Interval between subsequent readings of API in hours.
        /// </summary>
        public int ReadInterval { get; set; }
        public string Url { get; set; }
        public string Location { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
