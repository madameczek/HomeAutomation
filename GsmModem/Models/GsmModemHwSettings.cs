using Shared;
using Shared.Models;
using System;
using System.IO;
using System.IO.Ports;

namespace GsmModem.Models
{
    public class GsmModemHwSettings : IGsmModemHwSettings
    {
        public int ProcessId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceType Type { get; set; }
        public string Name { get; set; }
        public bool Attach { get; set; }
        public string Interface { get; set; }

        /// <summary>
        /// Interval between subsequent attempts to read modem response.
        /// If can't be read from config file, default value is applied.
        /// Default value is set in service's launcher object.
        /// </summary>
        public int ReadInterval { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Handshake Handshake { get; set; }
        public string NewLine { get; set; }
    }
}
