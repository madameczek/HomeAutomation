using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio;
using System.IO.Ports;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Relay
{
    public class GpioService : IDisposable
    {
        private GpioController _gpio;
        private readonly ILogger _logger;
        public GpioService(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("GPIO controller");
        }

        public bool Initialise()
        {
            try
            {
                if (_gpio == null)
                {
                    _gpio = new GpioController(PinNumberingScheme.Logical);
                    return true;
                }
                _logger.LogTrace("GPIO Initialised already.");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error initialising GPIO hardware. ");
                return false;
            }
        }
        
        public void OpenPin(int pin, PinValue onValue)
        {
            if (!_gpio.IsPinOpen(pin)) 
            {
                // There is a flash of low potential on an output between setting pin as output
                // and writing output to high. Sequence below charges internal capatitance before
                // setting pin as output. This minimalise initial low level on output before it is set high.
                // This might be important when active state = PinValue low.
                // To eliminate this completely, passive hardware integrator circuit should help.
                _gpio.OpenPin(pin, PinMode.InputPullUp);
                Task.Delay(10).Wait();
                _gpio.SetPinMode(pin, PinMode.Output);
                _gpio.Write(pin, !onValue);
            }
        }

        public void SetOn(int pin, PinValue onValue)
        {
            if (_gpio.IsPinOpen(pin))
            {
                _gpio.Write(pin, onValue);
            }
        }

        public void SetOff(int pin, PinValue onValue)
        {
            if (_gpio.IsPinOpen(pin))
            {
                _gpio.Write(pin, !onValue);
            }
        }

        public void Toggle(int pin)
        {
            if (_gpio.IsPinOpen(pin))            
            {
                _gpio.Write(pin, !_gpio.Read(pin));
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing GPIO controller.");
            _gpio?.Dispose();
        }
    }
}
