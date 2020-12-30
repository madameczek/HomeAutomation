using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Relay.Interfaces;
using Relay.Models;
using Shared.Models;

namespace Relay
{
    public enum State
    {
        On,
        Off
    }

    public class RelayDevice : IRelayDevice
    {
        private readonly GpioService _gpio;
        private readonly ILogger _logger;
        public RelayDevice(GpioService gpio, ILoggerFactory logger)
        {
            _gpio = gpio;
            _logger = logger.CreateLogger("Relay device");
        }

        private int _pin;
        private PinValue _activeState;

        public Task SetOn()
        {
            if (_pin == 0) throw new Exception("GPIO pin is not configured");
            try
            {
                _gpio.SetOn(_pin, _activeState);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Set ON pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public Task SetOff()
        {
            if (_pin == 0) throw new Exception("GPIO pin not configured");
            try
            {
                _gpio.SetOn(_pin, !_activeState);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Set Off pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public Task Toggle()
        {
            if (_pin == 0) throw new Exception("GPIO pin not configured");
            try
            {
                _gpio.Toggle(_pin);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Toggle pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public void Configure(RelayHwSettings settings)
        {
            _pin = settings.GpioPin;
            // This sets which output state (high or low) is configured as 'active' state.
            _activeState = settings.ActiveState;
            _gpio.OpenPin(settings.GpioPin, _activeState);
        }
    }
}
