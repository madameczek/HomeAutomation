using System;
using System.Collections.Generic;
using System.Text;

namespace GsmModem.Models
{
    public class HwSettings
    {
        public string DeviceId { get; set; }
        public string Interface { get; set; }
        public string Type { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public string Handshake { get; set; }
        public Char NewLineChar { get; set; }
        public int ReadInterval { get; set; }
        public bool Attach { get; set; }
    }
}
