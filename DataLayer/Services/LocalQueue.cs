using DataAccessLayer.Contexts;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LocalQueue
    {
        private readonly LocalContext _dbContext;
        private readonly ILogger _logger;
        public LocalQueue(ILogger<LocalQueue> logger, LocalContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task AddMessage(IMessage message, Type targetType)
        {
            // Strange looking manipulations to change type from those used by devices to 'Message' used in DbSet.
            // I.e time zone offset is removed from DateTimeOffest type in original message, 
            // because MariaDb makes unwanted computatiom to UTC. But deserialisation to target type gives back the offset.

            // Serialized object will be used as proto to deserialize onto target type
            // depending on required data type and then target type will be stored in a database.
            string json = JsonConvert.SerializeObject(message);
            var dbMessage = JsonConvert.DeserializeObject(json, targetType);

            var isDuplicate = false;

            if (targetType == typeof(Weather))
            {
                isDuplicate = IsDuplicate(dbMessage as Weather);
                if (!isDuplicate)
                {
                    _dbContext.Add(dbMessage as Weather);
                }
            }
            else if (targetType == typeof(TemperatureAndHumidity))
            {
                _dbContext.Add(dbMessage as TemperatureAndHumidity);
            }
            else
            {
                _logger.LogWarning("Unknow type requested");
                return Task.CompletedTask;
            }

            // In addition to above, all records are stored in 'blob' format with data serializesd to json.
            // This is for future use.
            if (!isDuplicate)
            {
                _dbContext.Add(CreateBlobMessage(message));
            }

            int count;
            try
            {
                count = _dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Local database error.");
                throw;
            }
            if (count > 0)
            {
                _logger.LogDebug("Data of type {type} saved to local database.", targetType.ToString().Split('.').Last());
            }
            return Task.CompletedTask;
        }

        static Message CreateBlobMessage (IMessage message)
        {
            // Create json to be stored as string in MessageBody field of a Message.
            message.Id = null;
            message.IsProcessed = null;
            string jsonData = JsonConvert.SerializeObject(
                message, 
                new JsonSerializerSettings 
                { 
                    NullValueHandling = NullValueHandling.Ignore 
                });
            var blobMessage = JsonConvert.DeserializeObject<Message>(jsonData);
            blobMessage.Id = 0;
            blobMessage.IsProcessed = false;
            blobMessage.MessageBody = jsonData;
            return blobMessage;
        }

        private bool IsDuplicate(Weather message)
        {
            try
            {
                return _dbContext.Messages.Any(m => m.CreatedOn == message.CreatedOn);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Local database Error");
                return false;
            }
        }
    }
}
