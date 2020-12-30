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

        // to do: handle exception: no gpio found
        GpioController gpio = new GpioController(PinNumberingScheme.Logical);

        public void OpenPin(int pin)
        {
            if (!gpio.IsPinOpen(pin)) gpio.OpenPin(pin, PinMode.Output);
            _logger.LogDebug("Pin set");
        }

        public void SetOn(int pin, PinValue onValue)
        {
            if (gpio.IsPinOpen(pin))
            {
                gpio.Write(pin, onValue);
            }
        }

        public void SetOff(int pin, PinValue onValue)
        {
            if (gpio.IsPinOpen(pin))
            {
                gpio.Write(pin, !onValue);
            }
        }

        public void Toggle(int pin)
        {
            if (gpio.IsPinOpen(pin))            
            {
                gpio.Write(pin, !gpio.Read(pin));
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing GPIO controller");
            gpio.Dispose();
        }
    }
}
