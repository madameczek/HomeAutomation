using IotHubGateway.Contexts;
using IotHubGateway.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotHubGateway.Services
{
    public class LocalQueue
    {
        private readonly LocalContext _localContext;
        private readonly AzureContext _remoteContext;
        private readonly ILogger _logger;

        public LocalQueue(LocalContext LocalDbContext, AzureContext remoteDbContext, ILogger<LocalQueue> logger)
        {
            _localContext = LocalDbContext;
            _remoteContext = remoteDbContext;
            _logger = logger;
        }

        /// <summary>
        /// Copies unprocessed messages from local database to remote.
        /// If copied successfully, source's property IsProcessed = true.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CopyNewLocalMessagesToRemote(CancellationToken ct = default)
        {
            var _localMessages = _localContext.Messages.Where(x => x.IsProcessed == false).ToList();
            if (_localMessages.Count == 0) return;
            // Clone messages
            var _clonedMessages = new List<Message>();
            _localMessages.ForEach(m => _clonedMessages.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            _clonedMessages.ForEach(x =>
            {
                x.Id = 0;
                var messageBody = JsonConvert.DeserializeAnonymousType(x.MessageBody, new { CreatedOn = "" });
                x.CreatedOn = DateTimeOffset.Parse(messageBody.CreatedOn);
            });
            await _remoteContext.Messages.AddRangeAsync(_clonedMessages, ct);
            //remoteContext.Messages.AddRange(clonedMessages);
            int _savedClonedMessagesCount = 0;
            try
            {
                _savedClonedMessagesCount = await _remoteContext.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Database error");
                throw;
            }
            //var savedClonedMessagesCount = remoteContext.SaveChanges();
            // If copied succesfully, mark local as processed.
            if (_savedClonedMessagesCount == _clonedMessages.Count)
            {
                _localMessages.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(_localMessages);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch (Exception e) { _logger.LogError(e, "Database error while copying messages to remote."); }
            }

            _logger.LogDebug("New Message records added to Azure: {savedClonedMessagesCount}", _savedClonedMessagesCount);
            return;
        }

        public async Task CopyNewLocalTemperaturesToRemote(CancellationToken ct = default)
        {
            var _localTemperatures = _localContext.Temperatures.Where(x => x.IsProcessed == false).ToList();
            if (_localTemperatures.Count == 0) return;
            // Clone messages
            var _clonedTemperatures = new List<TemperatureAndHumidity>();
            _localTemperatures.ForEach(m => _clonedTemperatures.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            _clonedTemperatures.ForEach(x =>
            {
                x.Id = 0;
                x.CreatedOn = new DateTimeOffset(x.CreatedOn.DateTime, new TimeSpan(TimeZoneInfo.Local.BaseUtcOffset.Hours, 0, 0));
            });
            await _remoteContext.Temperatures.AddRangeAsync(_clonedTemperatures, ct);
            var _savedClonedTemperaturesCount = await _remoteContext.SaveChangesAsync(ct);
            // If copied succesfully, mark local as processed.
            if (_savedClonedTemperaturesCount == _clonedTemperatures.Count)
            {
                _localTemperatures.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(_localTemperatures);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch(Exception e) { _logger.LogError(e, "Database error while copying temperatures to remote."); }
            }

            _logger.LogDebug("New Temperature records added to Azure: {savedClonedMessagesCount}", _savedClonedTemperaturesCount);
            return;
        }

        public async Task CopyNewLocalWeatherReadingsToRemote(CancellationToken ct = default)
        {
            var _localWeathers = _localContext.WeatherReadings.Where(x => x.IsProcessed == false).ToList();
            if (_localWeathers.Count == 0) return;
            // Clone messages
            var _clonedWeathers = new List<Weather>();
            _localWeathers.ForEach(m => _clonedWeathers.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            _clonedWeathers.ForEach(x =>
            {
                x.Id = 0;
                x.CreatedOn = new DateTimeOffset(x.CreatedOn.DateTime, new TimeSpan(TimeZoneInfo.Local.BaseUtcOffset.Hours, 0, 0));
            });
            await _remoteContext.WeatherReadings.AddRangeAsync(_clonedWeathers, ct);
            var _savedClonedWeathersCount = await _remoteContext.SaveChangesAsync(ct);
            // If copied succesfully, mark local as processed.
            if (_savedClonedWeathersCount == _clonedWeathers.Count)
            {
                _localWeathers.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(_localWeathers);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch (Exception e) { _logger.LogError(e, "Database error while copying Weathers to remote."); }
            }

            _logger.LogDebug("New Weather records added to Azure: {savedClonedMessagesCount}", _savedClonedWeathersCount);
            return;
        }

        // Temporary helper method
        public void SetLocalUnprocessed(CancellationToken ct)
        {
            var messages = _localContext.Messages.ToList();
            messages.ForEach(x => x.IsProcessed = false);
            _localContext.UpdateRange(messages);
            _localContext.SaveChangesAsync(ct);
        }
    }
}
