using IotHubGateway.Contexts;
using IotHubGateway.Models;
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
        /// Copies messagees unprocessed messages from local database to remote.
        /// If copied successfully, source's property IsProcessed = true.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CopyNewLocalMessagesToRemote(CancellationToken ct = default)
        {
            var localMessages = _localContext.Messages.Where(x => x.IsProcessed == false).OrderBy(x=>x.CreatedOn).ToList();
            if (localMessages.Count == 0) return; //Task.CompletedTask;
            // Clone messages
            List<Message> clonedMessages = new List<Message>();
            localMessages.ForEach(m => clonedMessages.Add(m.Clone()));
            // Add cloned messages to remote context and save.
            // Because local database does not support column type of C# DateTimeOffset, 
            // before insert to Azure MSSql, CreatedOn is retrieved from MessageBody field.
            clonedMessages.ForEach(x =>
            {
                x.Id = 0;
                var messageBody = JsonConvert.DeserializeAnonymousType(x.MessageBodyJson, new { CreatedOn = "" });
                x.CreatedOn = DateTimeOffset.Parse(messageBody.CreatedOn);
            });
            await _remoteContext.Messages.AddRangeAsync(clonedMessages, ct);
            //remoteContext.Messages.AddRange(clonedMessages);
            var savedClonedMessagesCount = await _remoteContext.SaveChangesAsync(ct);
            //var savedClonedMessagesCount = remoteContext.SaveChanges();
            // If copied succesfully, mark local processed.
            if (savedClonedMessagesCount == clonedMessages.Count)
            {
                localMessages.ForEach(x => x.IsProcessed = true);
                _localContext.UpdateRange(localMessages);
                var updatedCount = _localContext.SaveChangesAsync(ct).Result;
            }

            _logger.LogDebug("New records added to Azure: {savedClonedMessagesCount}", savedClonedMessagesCount);
            return;
        }

        // Temporary helper method
        public void SetLocalUprocessed(CancellationToken ct)
        {
            var messages = _localContext.Messages.ToList();
            messages.ForEach(x => x.IsProcessed = false);
            _localContext.UpdateRange(messages);
            _localContext.SaveChangesAsync(ct);
        }
    }
}
