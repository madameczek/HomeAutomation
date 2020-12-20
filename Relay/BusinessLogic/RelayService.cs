using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        public RelayService(IConfiguration configuration, ILoggerFactory logger, IServiceProvider services)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Relay service");
            _services = services;
        }
        #endregion
        
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "Relay";
        private List<RelayHwSettings> _hwSettingsList;

        public IHwSettings GetSettings() => throw new NotImplementedException();

        IEnumerable<IHwSettings> IRelayService.GetSettings()
        {
            return _hwSettingsList = _configuration
                .GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}")
                .Get<List<RelayHwSettings>>();
        }

        public Task ConfigureService(CancellationToken ct)
        {
            throw new NotImplementedException();
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
