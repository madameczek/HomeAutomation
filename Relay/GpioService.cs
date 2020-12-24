using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio;
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

        public void SetPin(int pin)
        {
            if (!gpio.IsPinOpen(pin)) gpio.OpenPin(pin);
            _logger.LogInformation("Pin set");
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing GPIO controller");
            gpio.Dispose();
        }
    }
}
