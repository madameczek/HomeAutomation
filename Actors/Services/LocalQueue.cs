using Actors.Contexts;
using Actors.Models.LocalDbModels;
using CommonClasses.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.Services
{
    public class LocalQueue : IDisposable
    {
        private readonly LocalContext _dbContext;
        private readonly ILogger _logger;
        public LocalQueue(ILogger<LocalQueue> logger, LocalContext dbContext)
        {
            this._dbContext = dbContext;
            _logger = logger;
        }

        public void AddMessage(IMessage message)
        {
            // This is to change type from those used by devices to 'Message' used in DbSet.
            // Here offset is removed from DateTimeOffest type in original message, because MariaDb makes unwanted computatiom to UTC.
            var _json = JsonConvert.SerializeObject(message);
            Message dbMessage = JsonConvert.DeserializeObject<Message>(_json);

            _dbContext.Add(dbMessage);
            _dbContext.SaveChanges();
            _logger.LogDebug("Data from device saved to local database");
        }

        private int counter = 0;
        public void AddWeatherData(IMessage message)
        {
            ++counter;
            Console.WriteLine($"message {counter}");

            Console.WriteLine(JsonConvert.SerializeObject(message));
            Console.WriteLine(_dbContext.ContextId);
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
