using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GsmModem.Models;
using System.Linq;
using Shared;
using Shared.Models;
using Microsoft.Extensions.Logging;
using System.IO.Ports;

namespace GsmModem
{
    public class GsmModemService : IGsmModemService
    {
        #region Ctor & Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        public GsmModemService(ILoggerFactory logger, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger.CreateLogger("GSM Service");
            _configuration = configuration;
            _services = services;
        }
        #endregion

        // Define variables for data fetched from 'appsettings.json'. Data are used to configure the service.
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "GsmModem";
        private IGsmModemHwSettings _hwSettings = new GsmModemHwSettings();
        private readonly string _serviceSettings = "Services:DatabasePooling";
        private readonly string _messageToDbPushPeriodSeconds = "GsmMessagePushPeriod";

        private Dictionary<string, string> _rawData = new Dictionary<string, string>();
        private static readonly Object _messageLock = new object();
        private static readonly Object _portLock = new object();
        private bool _deviceReadingIsValid = false;

        static SerialPort port;

        public Task<IHwSettings> ConfigureService(CancellationToken ct)
        {
            try
            {
                _hwSettings = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsCurrentActorSection).Get<GsmModemHwSettings>();
                _ = ConfigureComPort(_hwSettings);
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
            return Task.FromResult((IHwSettings)_hwSettings);
        }

        private bool ConfigureComPort(IGsmModemHwSettings gsmModemHwSettings)
        {


            return true;
        }

        public IMessage GetMessage()
        {
            throw new NotImplementedException();
        }

        public Task ReadDeviceAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Task.CompletedTask;
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Cancelled.");
            }
            return Task.CompletedTask;
        }

        public Task SaveMessageAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
