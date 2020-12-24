using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;
using Microsoft.Extensions.Logging;
using Relay.Interfaces;

namespace Relay
{
    public class RelayDevice : IRelayDevice
    {
        // zainicjalizować device i trzymac bool isInitialized
        // można w serwisie trzymac zmienną _device w usingu
        // pomyslec, jak uzywac różne device z jednym kontrollerem gpio
        private readonly GpioService _gpio;
        private readonly ILogger _logger;
        public RelayDevice(GpioService gpio, ILoggerFactory logger)
        {
            _gpio = gpio;
            _logger = logger.CreateLogger("Relay device");

        }

        public void SetPin(int pin)
        { 
            _gpio.SetPin(pin);
        }

    }
}
