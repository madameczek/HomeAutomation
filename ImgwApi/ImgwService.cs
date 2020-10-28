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
        
        public override Task ConfigureService(CancellationToken cancellationToken)
        {
            _hwSettings = _configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).Get<HwSettings>();
            return Task.CompletedTask;
        }

        public override IMessage GetMessage()
        {
            // Cut milliseconds for shorter storage in json.
            DateTimeOffset time = DateTimeOffset.Now;
            time = time.AddTicks(-(time.Ticks % TimeSpan.TicksPerSecond));

            // Temporary object with readings to be serialized.
            IMessage _message = new WeatherData()
            {
                CreatedOn = time,
                ActorId = _hwSettings.DeviceId,
                AirTemperature = 23
            };

            // Create json to be stored as string in MessageBody field of a Message.
            var _jsonData = JsonConvert.SerializeObject(_message, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            IMessage message = JsonConvert.DeserializeObject<WeatherMessage>(_jsonData);

            message.Id = 0;
            message.IsProcessed = false;
            message.MessageBodyJson = _jsonData;
            message.CreatedOn = time;
            return message;
        }

        public override IService ReadConfig()
        {
            throw new NotImplementedException();
        }

        public override async Task Run(CancellationToken ct = default)
        {
            // This periodically invokes a method reading temperature from a sensor.
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await GetDataAsync(ct);
                    // ReadInterval in config file is expresesed in minutes
                    await Task.Delay(_hwSettings.ReadInterval * 60 * 1000, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
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

                var stringTask = _client.GetStringAsync(_hwSettings.Url+ _hwSettings.StationId);

                var msg = await stringTask;
                var message = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
                _logger.LogDebug("Imgw api looks working {message}", message);
            }
            catch (OperationCanceledException) { _logger.LogDebug("Cancelled at getting web data."); }
            catch (Exception e) { _logger.LogCritical(e, "Service ImgwService crashed"); throw; }
        }
    }
}
