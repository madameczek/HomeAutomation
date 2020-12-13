using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace GsmModem
{
    public interface IModemDevice
    {
        public Task Initialize(SerialPort port);
    }
}
