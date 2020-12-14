using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GsmModem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace GsmModem
{
    public class PortProvider : IPortProvider
    {
        private static SerialPort _port;

        #region Ctor & Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public PortProvider(ILoggerFactory logger, IConfiguration configuration)
        {
            _logger = logger.CreateLogger("Serial port Provider");
            _configuration = configuration;
        }
        #endregion

        public Task<SerialPort> GetPort(IGsmModemHwSettings gsmModemHwSettings)
        {
            _port = new SerialPort()
            {
                PortName = gsmModemHwSettings.PortName,
                BaudRate = gsmModemHwSettings.BaudRate,
                Handshake = gsmModemHwSettings.Handshake,
                NewLine = gsmModemHwSettings.NewLine
            };

            if (SerialPort.GetPortNames().Any(pn => pn == _port.PortName))
            {
                _logger.LogDebug("Serial port configured on {PortName}", gsmModemHwSettings.PortName);
                return Task.FromResult(_port);
            }
            _logger.LogError("Port not configured. Available ports: {Ports}", SerialPort.GetPortNames().ToList());
            return Task.FromException<SerialPort>(new IOException("Requested port not available"));
        }
    }
}
