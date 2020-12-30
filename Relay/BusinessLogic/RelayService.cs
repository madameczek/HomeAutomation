using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Relay.Interfaces;
using Relay.Models;
using Shared.Models;

namespace Relay
{
    public class RelayService : IRelayService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        private readonly IRelayDevice _relayDevice;
        public RelayService(IConfiguration configuration, ILoggerFactory logger, IServiceProvider services, IRelayDevice relayDevice)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Relay service");
            _services = services;
            _relayDevice = relayDevice;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "Relay";
        private RelayHwSettings _hwSettings;
        private bool _isOn = false;

        public IHwSettings GetSettings() => throw new NotImplementedException();
        public Task ConfigureService(CancellationToken ct) => throw new NotImplementedException();

        IEnumerable<IHwSettings> IRelayService.GetSettings()
        {
            return _configuration
                .GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}")
                .Get<List<RelayHwSettings>>();
        }

        public Task ConfigureService(IHwSettings settings, CancellationToken ct)
        {
            _hwSettings = (RelayHwSettings)settings;
            try
            {
                _relayDevice.Configure(_hwSettings);
                // pobierz sunset;
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public async Task Run(CancellationToken ct)
        {
            var timeOn = _hwSettings.TimeOn;
            var timeOff = _hwSettings.TimeOff;
            while (!ct.IsCancellationRequested)
            {
                if (DateTime.Now.TimeOfDay > timeOn && DateTime.Now.TimeOfDay < timeOff)
                {
                    _ = _relayDevice.SetOn();
                    //_logger.LogDebug("Relay {Name} is ON", _hwSettings.Name);
                }
                else
                {
                    _ = _relayDevice.SetOff();
                    //_logger.LogDebug("Relay {Name} is OFF", _hwSettings.Name);
                    //_ = _relayDevice.Toggle(); // just for testing
                }
                await Task.Delay(TimeSpan.FromSeconds(15), ct);
            }
        }

        public Task ReadDeviceAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public IMessage GetMessage()
        {
            throw new NotImplementedException();
        }

        public Task SaveMessageAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
