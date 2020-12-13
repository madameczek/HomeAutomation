using System.IO.Ports;
using System.Threading.Tasks;
using GsmModem.Models;
using Shared.Models;

namespace GsmModem
{
    public interface IPortProvider
    {
        public Task<SerialPort> GetPort(IGsmModemHwSettings gsmModemHwSettings);
    }
}