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
    public class SunriseSunsetService : ISunriseSunsetService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        public SunriseSunsetService(IConfiguration configuration, ILoggerFactory logger, IServiceProvider services)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Sunset service");
            _services = services;
        }
        #endregion

        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "SunsetApi";
        private IHwSettings _hwSettings;
        private Dictionary<string, string> _dataFieldNames;

        public IHwSettings GetSettings()
        {
            return _hwSettings = _configuration
                .GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}")
                .Get<SunriseSunsetHwSettings>();
        }

        public Task ConfigureService(CancellationToken ct)
        {
            _dataFieldNames = _configuration
                .GetSection(HwSettingsSection)
                .GetSection(HwSettingsCurrentActorSection)
                .GetSection("Fields").Get<Dictionary<string, string>>();
            return Task.CompletedTask;
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
