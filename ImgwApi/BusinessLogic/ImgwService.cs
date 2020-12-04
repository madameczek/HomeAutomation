using DataAccessLayer;
using DataAccessLayer.Models;
using ImgwApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImgwApi
{
    public class ImgwService : IImgwService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        public ImgwService(ILoggerFactory logger, IConfiguration configuration, IServiceProvider services)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("IMGW service");
            _services = services;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "ImgwApi";
        private IHwSettings _hwSettings = new ImgwHwSettings();
        Dictionary<string, string> _dataFieldNames;

        private Dictionary<string, string> _rawData = new Dictionary<string, string>();
        private static readonly Object WeatherLock = new object();
        private bool _deviceReadingIsValid = false;

        public Task<IHwSettings> ConfigureService(CancellationToken cancellationToken)
        {
            _hwSettings = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsCurrentActorSection).Get<ImgwHwSettings>();
            _dataFieldNames = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsCurrentActorSection).GetSection("Fields").Get<Dictionary<string, string>>();
            return Task.FromResult(_hwSettings);
        }

        public async Task ReadDeviceAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", "Private student's API test");
                string response = await Task.Run(()
                    => client.GetStringAsync((_hwSettings as IImgwHwSettings).Url + (_hwSettings as IImgwHwSettings).StationId), ct);
                lock (WeatherLock)
                {
                    _rawData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                }
                _deviceReadingIsValid = _rawData.ContainsValue((_hwSettings as IImgwHwSettings).StationId.ToString());
                _logger.LogDebug("Fetched data from IMGW.");
            }
            catch (OperationCanceledException) 
            { 
                _logger.LogDebug("Cancelled"); 
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Service crashed");
                _deviceReadingIsValid = false;
                await Task.FromException(e);
            }
            await Task.CompletedTask;
        }

        public IMessage GetMessage()
        {
            // Temporary object with readings to be serialized.
            WeatherData tempMessage;
            var parseCulture = CultureInfo.CreateSpecificCulture("en-US");
            lock (WeatherLock)
            {
                tempMessage = new WeatherData()
                {
                    Id = 0,
                    CreatedOn = DateTime.Parse(_rawData[_dataFieldNames["ReadingDate"]]).AddHours(int.Parse(_rawData[_dataFieldNames["ReadingTime"]])),
                    IsProcessed = false,
                    ActorId = _hwSettings.DeviceId,
                    AirTemperature = double.Parse(_rawData[_dataFieldNames["AirTemperature"]], parseCulture),
                    AirPressure = double.Parse(_rawData[_dataFieldNames["AirPressure"]], parseCulture),
                    Precipitation = double.Parse(_rawData[_dataFieldNames["Precipitation"]], parseCulture),
                    Humidity = double.Parse(_rawData[_dataFieldNames["Humidity"]], parseCulture),
                    WindSpeed = int.Parse(_rawData[_dataFieldNames["WindSpeed"]]),
                    WindDirection = int.Parse(_rawData[_dataFieldNames["WindDirection"]]),
                    StationId = int.Parse(_rawData[_dataFieldNames["StationId"]]),
                    StationName = _rawData[_dataFieldNames["StationName"]]
                };
            }
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
                    //_logger.LogDebug("Scoped service Hash: {LocalQueueHash}", scopedService.GetHashCode());
                    await scopedService.AddMessage(GetMessage(), typeof(Weather));
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
