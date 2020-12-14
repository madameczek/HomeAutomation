using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IotHubGatewayDaemon.Contexts;
using IotHubGatewayDaemon.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IotHubGatewayDaemon.Services
{
    public class LocalQueue
    {
        private readonly LocalContext _localContext;
        private readonly AzureContext _remoteContext;
        private readonly ILogger _logger;

        public LocalQueue(LocalContext localDbContext, AzureContext remoteDbContext, ILogger<LocalQueue> logger)
        {
            _localContext = localDbContext;
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
            var localMessages = _localContext.Messages.Where(x => x.IsProcessed == false).ToList();
            if (localMessages.Count == 0) return;
            // Clone messages
            var clonedMessages = new List<Message>();
            localMessages.ForEach(m => clonedMessages.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            clonedMessages.ForEach(x =>
            {
                x.Id = 0;
                var messageBody = JsonConvert.DeserializeAnonymousType(x.MessageBody, new { CreatedOn = "" });
                x.CreatedOn = DateTime.Parse(messageBody.CreatedOn);
            });
            await _remoteContext.Messages.AddRangeAsync(clonedMessages, ct);
            //remoteContext.Messages.AddRange(clonedMessages);
            var savedClonedMessagesCount = 0;
            try
            {
                savedClonedMessagesCount = await _remoteContext.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Database error");
                throw;
            }
            //var savedClonedMessagesCount = remoteContext.SaveChanges();
            // If copied succesfully, mark local as processed.
            if (savedClonedMessagesCount == clonedMessages.Count)
            {
                localMessages.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(localMessages);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch (Exception e) { _logger.LogError(e, "Database error while copying messages to remote."); }
            }

            _logger.LogDebug("New Message records added to Azure: {savedClonedMessagesCount}", savedClonedMessagesCount);
            return;
        }

        public async Task CopyNewLocalTemperaturesToRemote(CancellationToken ct = default)
        {
            var localTemperatures = _localContext.Temperatures.Where(x => x.IsProcessed == false).ToList();
            if (localTemperatures.Count == 0) return;
            // Clone messages
            var clonedTemperatures = new List<TemperatureAndHumidity>();
            localTemperatures.ForEach(m => clonedTemperatures.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            clonedTemperatures.ForEach(x =>
            {
                x.Id = 0;
                //x.CreatedOn = new DateTime(x.CreatedOn.DateTime, new TimeSpan(TimeZoneInfo.Local.BaseUtcOffset.Hours, 0, 0));
            });
            await _remoteContext.Temperatures.AddRangeAsync(clonedTemperatures, ct);
            var savedClonedTemperaturesCount = await _remoteContext.SaveChangesAsync(ct);
            // If copied succesfully, mark local as processed.
            if (savedClonedTemperaturesCount == clonedTemperatures.Count)
            {
                localTemperatures.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(localTemperatures);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch(Exception e) { _logger.LogError(e, "Database error while copying temperatures to remote."); }
            }

            _logger.LogDebug("New Temperature records added to Azure: {savedClonedMessagesCount}", savedClonedTemperaturesCount);
            return;
        }

        public async Task CopyNewLocalWeatherReadingsToRemote(CancellationToken ct = default)
        {
            var localWeathers = _localContext.WeatherReadings.Where(x => x.IsProcessed == false).ToList();
            if (localWeathers.Count == 0) return;
            // Clone messages
            var clonedWeathers = new List<Weather>();
            localWeathers.ForEach(m => clonedWeathers.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            clonedWeathers.ForEach(x =>
            {
                x.Id = 0;
                //x.CreatedOn = new DateTimeOffset(x.CreatedOn.DateTime, new TimeSpan(TimeZoneInfo.Local.BaseUtcOffset.Hours, 0, 0));
            });
            await _remoteContext.WeatherReadings.AddRangeAsync(clonedWeathers, ct);
            var savedClonedWeathersCount = await _remoteContext.SaveChangesAsync(ct);
            // If copied succesfully, mark local as processed.
            if (savedClonedWeathersCount == clonedWeathers.Count)
            {
                localWeathers.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(localWeathers);
                try
                {
                    await _localContext.SaveChangesAsync(ct);
                }
                catch (Exception e) { _logger.LogError(e, "Database error while copying Weathers to remote."); }
            }

            _logger.LogDebug("New Weather records added to Azure: {savedClonedMessagesCount}", savedClonedWeathersCount);
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
