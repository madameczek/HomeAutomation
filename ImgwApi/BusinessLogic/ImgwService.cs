using DataLayer;
using DataLayer.Models;
using ImgwApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImgwApi
{
    public class ImgwService : IService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _services;
        public ImgwService(ILoggerFactory logger, IConfiguration configuration, HttpClient httpClient, IServiceProvider services)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("IMGW service");
            _httpClient = httpClient;
            _services = services;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "ImgwApi";
        private ImgwHwSettings _hwSettings;
        private Dictionary<string, string> _dataFieldNames;

        private Dictionary<string, string> _rawData = new Dictionary<string, string>();
        private static readonly object WeatherLock = new object();
        private bool _deviceReadingIsValid;

        public IHwSettings GetSettings()
        {
            return _hwSettings = _configuration
                .GetSection($"{HwSettingsSection}:{HwSettingsCurrentActorSection}")
                .Get<ImgwHwSettings>();
        }

        public Task ConfigureService(CancellationToken cancellationToken)
        {
            _dataFieldNames = _configuration
                .GetSection(HwSettingsSection) 
                .GetSection(HwSettingsCurrentActorSection)
                .GetSection("Fields").Get<Dictionary<string, string>>();
            return Task.CompletedTask;
        }

        public async Task ReadDeviceAsync(CancellationToken ct)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Private student's API test");
                string response = await Task.Run(()
                    => _httpClient.GetStringAsync($"{_hwSettings.Url}{_hwSettings.StationId}"), ct);
                lock (WeatherLock)
                {
                    _rawData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                }
                _deviceReadingIsValid = _rawData.ContainsValue(_hwSettings.StationId.ToString());
                _logger.LogInformation("Fetched data from IMGW.");
            }
            catch (OperationCanceledException) 
            { 
                _logger.LogDebug("Cancelled."); 
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Service crashed.");
                _deviceReadingIsValid = false;
                await Task.FromException(e);
            }
            await Task.CompletedTask;
        }

        public IMessage GetMessage()
        {
            // Temporary object with readings to be serialized.
            WeatherData tempMessage;
            lock (WeatherLock)
            {
                tempMessage = new WeatherData()
                {
                    Id = 0,
                    CreatedOn = DateTime.Parse(_rawData[_dataFieldNames["ReadingDate"]]).AddHours(int.Parse(_rawData[_dataFieldNames["ReadingTime"]])),
                    IsProcessed = false,
                    ActorId = _hwSettings.DeviceId,
                    AirTemperature = double.Parse(_rawData[_dataFieldNames["AirTemperature"]], CultureInfo.InvariantCulture),
                    AirPressure = double.Parse(_rawData[_dataFieldNames["AirPressure"]], CultureInfo.InvariantCulture),
                    Precipitation = double.Parse(_rawData[_dataFieldNames["Precipitation"]], CultureInfo.InvariantCulture),
                    Humidity = double.Parse(_rawData[_dataFieldNames["Humidity"]], CultureInfo.InvariantCulture),
                    WindSpeed = int.Parse(_rawData[_dataFieldNames["WindSpeed"]]),
                    WindDirection = int.Parse(_rawData[_dataFieldNames["WindDirection"]]),
                    StationId = int.Parse(_rawData[_dataFieldNames["StationId"]]),
                    StationName = _rawData[_dataFieldNames["StationName"]]
                };
            }
            tempMessage.CreatedOn = DateTime.SpecifyKind(tempMessage.CreatedOn, DateTimeKind.Local);
            return tempMessage;
        }

        public async Task SaveMessageAsync(CancellationToken ct)
        {
            if (_deviceReadingIsValid)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var scopedService = scope.ServiceProvider.GetRequiredService<LocalQueue>();
                    await scopedService.AddMessage(GetMessage(), typeof(Weather), ct);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogDebug("Cancelled");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error saving message.");
                }
            }
        }
    }
}
