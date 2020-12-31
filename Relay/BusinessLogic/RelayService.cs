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
        #region Ctor & Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IRelayDevice _relayDevice;
        private readonly ISunriseSunsetService _sunsetService;
        public RelayService(IConfiguration configuration, ILoggerFactory logger, IRelayDevice relayDevice, ISunriseSunsetService sunriseSunsetService)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Relay service");
            _relayDevice = relayDevice;
            _sunsetService = sunriseSunsetService;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "Relay";
        private RelayHwSettings _hwSettings;

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
                switch (_hwSettings.ActivateOn)
                {
                    case "Sunset":
                        _sunsetService.Sunset += OnSunsetActivateEventHandler;
                        break;
                    case "TimeOn":
                        break;
                    default:
                        _logger.LogError("Relay {Name} activation not configured.", _hwSettings.Name);
                        break;
                }
                _logger.LogDebug("Relay {Name} activating configured for {Event}.", _hwSettings.Name, _hwSettings.ActivateOn);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        void OnSunsetActivateEventHandler(object sender, EventArgs e)
        {
            _relayDevice.SetOn();
            _logger.LogDebug("OnSunsetActivate event handler invoked.");
        }

        public async Task Run(CancellationToken ct)
        {
            // zamiast tego eventy od zegara.
            // Przygotowac handlery, które będą dopinane case-switch.
            // Uwazac na odpalanie eventow. w zadadzie nalezaloby sprawdzic
            // czy nie nastapil warunek wylaczenia i dopiero wtedy sprawdzać, czy jest spelniony warunek wlaczenia.
            var timeOn = _hwSettings.TimeOn;
            var timeOff = _hwSettings.TimeOff;
            while (!ct.IsCancellationRequested)
            {
                if (DateTime.Now.TimeOfDay > timeOn && DateTime.Now.TimeOfDay < timeOff)
                {
                    //_ = _relayDevice.SetOn();
                    //_logger.LogTrace("Relay {RelayName} is ON.", _hwSettings.Name);
                }
                else
                {
                    _ = _relayDevice.SetOff();
                    _logger.LogTrace("Relay {RelayName} is OFF.", _hwSettings.Name);
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
