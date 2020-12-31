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
        private bool _isGpioInitialised;

        public Task SetOn()
        {
            try
            {
                if (_pin == 0) 
                    throw new Exception("GPIO pin is not configured");
                if (_isGpioInitialised)
                {
                    _gpio.SetOn(_pin, _activeState);
                    return Task.CompletedTask;
                }
                throw new InvalidOperationException("GPIO not initialised.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("{Message}", e.Message);
                return Task.FromException(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Set ON pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public Task SetOff()
        {
            try
            {
                if (_pin == 0) 
                    throw new Exception("GPIO pin not configured");
                if (_isGpioInitialised)
                {
                    _gpio.SetOn(_pin, !_activeState);
                    return Task.CompletedTask;
                }
                throw new InvalidOperationException("GPIO not initialised.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("{Message}", e.Message);
                return Task.FromException(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Set Off pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public Task Toggle()
        {
            try
            {
                if (_pin == 0) 
                    throw new Exception("GPIO pin not configured");
                if (_isGpioInitialised)
                {
                    _gpio.Toggle(_pin);
                    return Task.CompletedTask;
                }
                throw new InvalidOperationException("GPIO not initialised.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("{Message}", e.Message);
                return Task.FromException(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Toggle pin {Pin} failed", _pin);
                return Task.FromException(e);
            }
        }

        public bool Configure(RelayHwSettings settings)
        {
            _pin = settings.GpioPin;
            // First use of the method initialises GPIO controller with hardware
            _isGpioInitialised = _gpio.Initialise();
            // This sets which output state (high or low) is configured as 'active' state.
            _activeState = settings.ActiveState;
            try
            {
                if (_isGpioInitialised)
                {
                    _gpio.OpenPin(settings.GpioPin, _activeState);
                    return true;
                }
                _logger.LogError("GPIO not initialised.");
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Open pin {Pin} failed", _pin);
                return false;
            }
        }
    }
}
