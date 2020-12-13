using System.IO.Ports;
using Shared.Models;

namespace GsmModem.Models
{
    public interface IGsmModemHwSettings : IHwSettings
    {
        int BaudRate { get; set; }
        Handshake Handshake { get; set; }
        string NewLine { get; set; }
        string PortName { get; set; }
    }
}
