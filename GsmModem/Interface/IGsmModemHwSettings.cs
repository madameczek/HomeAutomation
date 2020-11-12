using Shared.Models;

namespace GsmModem.Models
{
    public interface IGsmModemHwSettings : IHwSettings
    {
        int BaudRate { get; set; }
        string Handshake { get; set; }
        char NewLineChar { get; set; }
        string PortName { get; set; }
    }
}
