using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GsmModem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GsmModem
{
    public class ModemDevice : IModemDevice
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public ModemDevice(IConfiguration configuration, ILoggerFactory logger)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Modem Device");
        }

        public async Task Initialize(SerialPort port)
        {
            var initializationCommands = _configuration
                .GetSection("ModemCommands")
                .Get<GsmModemCommands>()
                .InitCommands
                .Where(c => c.Key.Contains("Init"))
                .OrderBy(c => c.Key)
                .Select(c => c.Value)
                .ToList();
            try
            {
                port.WriteLine("AT"); // needed to synchronise after uart speed change
                await Task.Delay(300);
                foreach (var command in initializationCommands)
                {
                    port.WriteLine(command);
                    await Task.Delay(200);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Modem initialization error");
            }
        }
    }
}
