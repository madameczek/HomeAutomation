﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; // for using IServiceProvider
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Relay.Models;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Relay
{
    public class SunriseSunsetService : ISunriseSunsetService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _services;
        public SunriseSunsetService(IConfiguration configuration, ILoggerFactory logger, HttpClient httpClient, IServiceProvider services)
        {
            _configuration = configuration;
            _logger = logger.CreateLogger("Sunset service");
            _httpClient = httpClient;
            _services = services;
        }
        #endregion

        // Define section of appsettings.json to parse device config from configuration object
        public string HwSettingsSection { get; } = "HWSettings";
        public string HwSettingsCurrentActorSection { get; } = "SunsetApi";
        private SunriseSunsetHwSettings _hwSettings;
        private Dictionary<string, string> _dataFieldNames;

        private SunriseSunsetApiData _data;
        private static readonly object ApiLock = new object();
        private bool _deviceReadingIsValid;

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

        public async Task ReadDeviceAsync(CancellationToken ct)
        {
            // try find in database first
            if (_data == null || _data.SolarNoon.Date != DateTime.Today)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", "Private student's API test");
                    _hwSettings.Url = _hwSettings.Url
                        .Replace("{Lat}", _hwSettings.Lat.ToString(CultureInfo.InvariantCulture))
                        .Replace("{Lon}", _hwSettings.Lon.ToString(CultureInfo.InvariantCulture));
                    var response = await _httpClient.GetAsync(_hwSettings.Url, ct);
                    if (response.IsSuccessStatusCode)
                    {
                        var settings = new JsonSerializerSettings()
                        {
                            ContractResolver = new CustomContractResolver(_dataFieldNames)
                        };
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var isServiceStatusOk = JObject.Parse(responseBody).Value<string>("status") == "OK";
                        if (JObject.Parse(responseBody).TryGetValue("results", out var sunData) && isServiceStatusOk)
                        {
                            lock (ApiLock)
                            {
                                _data = JsonConvert.DeserializeObject<SunriseSunsetApiData>(sunData?.ToString(), settings);
                            }
                            _logger.LogDebug("Fetched data from Sunset API.");
                            await Task.CompletedTask;
                        }
                    }
                    _logger.LogError("Error fetching data.");
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
            }
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