using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio;
using System.IO.Ports;
using Microsoft.Extensions.Logging;

namespace Relay
{
    public class GpioService : IDisposable
    {
        private readonly ILogger _logger;
        public GpioService(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("GPIO controller");
        }

        GpioController gpio = new GpioController(PinNumberingScheme.Logical);

        public void OpenPin(int pin)
        {
            if (!gpio.IsPinOpen(pin)) gpio.OpenPin(pin, PinMode.Output);
            _logger.LogDebug("Pin set");
        }

        public void SetOn(int pin, PinValue value)
        {
            if (gpio.IsPinOpen(pin))
            {
                gpio.Write(pin, value);
            }
        }

        public void SetOff(int pin, PinValue value)
        {
            if (gpio.IsPinOpen(pin))
            {
                gpio.Write(pin, !value);
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing GPIO controller");
            gpio.Dispose();
        }
    }
}
