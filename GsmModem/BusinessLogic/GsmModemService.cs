using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GsmModem.Models;
using System.Linq;
using Shared.Models;
using Microsoft.Extensions.Logging;
using System.IO.Ports;

namespace GsmModem
{
    public class GsmModemService : IGsmModemService
    {
        private SerialPort _port;

        #region Ctor & Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IPortProvider _portProvider;
        private readonly IModemDevice _device;
        public GsmModemService(ILoggerFactory logger, IConfiguration configuration, IPortProvider portProvider, IModemDevice device)
        {
            _logger = logger.CreateLogger("GSM Service");
            _configuration = configuration;
            _portProvider = portProvider;
            _device = device;
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

        public IHwSettings GetSettings()
        {
            return _hwSettings =_configuration.GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}").Get<GsmModemHwSettings>();
        }

        public async Task<IHwSettings> ConfigureService(CancellationToken ct)
        {
            try
            {
                 _port = await _portProvider.GetPort(_hwSettings);
                await _device.Initialize(_port);
            }
            catch (OperationCanceledException)
            {
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Service configuration error");
            }

            return _hwSettings;
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
