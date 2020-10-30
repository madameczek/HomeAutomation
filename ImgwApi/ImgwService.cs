using CommonClasses;
using CommonClasses.Interfaces;
using ImgwApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImgwApi
{
    public class ImgwService : BaseService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public ImgwService(ILogger<ImgwService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public override string HwSettingsActorSection { get; } = "ImgwApi";
        private HwSettings _hwSettings = new HwSettings();
        Dictionary<string, string> _dataFieldNames;

        private Dictionary<string, string> _rawData = new Dictionary<string, string>();
        IMessage _message = new WeatherData();

        public override void ConfigureService()
        {
            _hwSettings = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).Get<HwSettings>();
            _dataFieldNames = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).GetSection("Fields").Get<Dictionary<string,string>>();
        }

        public override IMessage GetMessage()
        {
            Task.Run(async () => await GetDataAsync()).Wait();
            // Temporary object with readings to be serialized.

            IMessage _tempMessage = new WeatherData()
            {
                Id = 0,
                IsProcessed = false,
                CreatedOn = DateTime.Parse(_rawData[_dataFieldNames["ReadingDate"]]).AddHours(int.Parse(_rawData[_dataFieldNames["ReadingTime"]])),
                ActorId = _hwSettings.DeviceId,
                AirTemperature = double.Parse(_rawData[_dataFieldNames["AirTemperature"]]),
                AirPressure = double.Parse(_rawData[_dataFieldNames["AirPressure"]]),
                Precipitation = double.Parse(_rawData[_dataFieldNames["Precipitation"]]),
                Humidity = double.Parse(_rawData[_dataFieldNames["Humidity"]]),
                WindSpeed = int.Parse(_rawData[_dataFieldNames["WindSpeed"]]),
                WindDirection = int.Parse(_rawData[_dataFieldNames["WindDirection"]]),
                StationId = int.Parse(_rawData[_dataFieldNames["StationId"]]),
                StationName = _rawData[_dataFieldNames["StationName"]]
            };
            return _tempMessage;
        }

        public override IService ReadConfig()
        {
            throw new NotImplementedException();
        }

        public override async Task Run(CancellationToken ct = default)
        {
            return;
#pragma warning disable CS0162 // Unreachable code detected
            var _timer = new System.Timers.Timer(_hwSettings.ReadInterval);
#pragma warning restore CS0162 // Unreachable code detected
                              // This periodically invokes a method reading temperature from a sensor.
            try
            {
                if (!ct.IsCancellationRequested)
                {

                    _timer.Elapsed += async (sender, e) => { await GetDataAsync(ct); _message = GetMessage(); };
                    _timer.AutoReset = true;
                    _timer.Start();
                }
                while (!ct.IsCancellationRequested)
                {
                    await Task.Delay(100000, ct);
                }
            }
            catch (OperationCanceledException) 
            { 
                _timer.Stop(); 
                _timer.Dispose(); 
                _logger.LogDebug("Cancelled in IMGWService.Run."); 
            }
            catch (Exception) { throw; }
            return;
        }

        public override bool Write(IMessage message)
        {
            throw new NotImplementedException();
        }

        private async Task GetDataAsync(CancellationToken ct = default)
        {
            HttpClient _client = new HttpClient();
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add("User-Agent", "Private student's API test");
                string _response = await Task.Run(() => _client.GetStringAsync(_hwSettings.Url + _hwSettings.StationId), ct);
                _rawData = JsonConvert.DeserializeObject<Dictionary<string, string>>(_response);
                
                _logger.LogDebug("Fetched data from IMGW.");
            }
            catch (OperationCanceledException) { _logger.LogDebug("Cancelled in IMGWService.Getting Web data."); }
            catch (Exception e) { _logger.LogCritical(e.Message, "Service ImgwService crashed"); throw; }
        }
    }
}
