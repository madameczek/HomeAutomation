using DataLayer.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    public enum DeviceType
    {
        None,
        ActorConfiguration,
        GsmModem,
        Input,
        Message,
        QueueItemLocal,
        Relay,
        SunriseSunset,
        TemperatureAndHumidity,
        Weather
    }

    public class LocalQueue
    {
        private readonly LocalContext _dbContext;
        private readonly ILogger _logger;
        public LocalQueue(ILoggerFactory logger, LocalContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("Local Queue");
        }

        public Task AddMessage(IMessage message, Type targetType, CancellationToken ct)
        {
            // Json serialization/deserialization is used to change type from those used by devices to 'Message' used in DbSet.
            // Serialized object will be used as proto to deserialize onto target type
            // depending on required data type, and then target type will be stored in a database.
            var json = JsonConvert.SerializeObject(message);
            var dbMessage = JsonConvert.DeserializeObject(json, targetType);

            var isDuplicate = false;

            if (targetType == typeof(Weather))
            {
                isDuplicate = IsDuplicate(dbMessage,  typeof(Weather));
                if (!isDuplicate)
                {
                    _dbContext.Add(dbMessage as Weather);
                }
            }
            else if (targetType == typeof(TemperatureAndHumidity))
            {
                _dbContext.Add(dbMessage as TemperatureAndHumidity);
            }
            else if (targetType == typeof(SunriseSunset))
            {
                isDuplicate = IsDuplicate(dbMessage, typeof(SunriseSunset));
                if (!isDuplicate)
                {
                    _dbContext.Add(dbMessage as SunriseSunset);
                }
            }
            else
            {
                _logger.LogWarning("Unknow type requested.");
                return Task.CompletedTask;
            }

            // In addition to above, all records are stored in 'blob' format with data serializesd to json.
            // This is for future use.
            if (!isDuplicate)
            {
                _dbContext.Add(CreateBlobMessage(message, targetType));
            }

            int count;
            try
            {
                count = _dbContext.SaveChangesAsync(cancellationToken: ct).Result;
            }
            catch (RetryLimitExceededException e)
            {
                _logger.LogWarning("Local database Error. Retry limit exceeded.");
                return Task.FromException(e);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Local database Error.");
                return Task.FromException(e);
            }

            if (count > 0)
            {
                _logger.LogInformation("Data of type {type} saved to local database.", targetType.ToString().Split('.').Last());
            }
            return Task.CompletedTask;
        }

        private static Message CreateBlobMessage (IMessage message, Type targetType)
        {
            // Create json to be stored as string in MessageBody field of a Message.
            message.Id = null;
            message.IsProcessed = null;
            var jsonData = JsonConvert.SerializeObject(
                message, 
                new JsonSerializerSettings 
                { 
                    NullValueHandling = NullValueHandling.Ignore 
                });
            var blobMessage = JsonConvert.DeserializeObject<Message>(jsonData);
            blobMessage.Id = 0;
            blobMessage.IsProcessed = false;
            blobMessage.MessageBody = jsonData;
            blobMessage.Type = SetMessageType(targetType);
            return blobMessage;
        }

        private bool IsDuplicate(object message, Type type)
        {
            try
            {
                if (type == typeof(Weather))
                {
                    // conversion to UTC is connected with conversion defined in Message model
                    return _dbContext.Messages.Where(m=>m.Type == (int)DeviceType.Weather).Any(m =>
                        m.CreatedOn == (message as Weather).CreatedOn.ToUniversalTime());
                }

                if (type == typeof(SunriseSunset))
                {
                    // conversion to UTC is connected with conversion defined in Message model
                    return _dbContext.Messages.Where(m => m.Type == (int)DeviceType.SunriseSunset).Any(m =>
                        m.CreatedOn.Date == (message as SunriseSunset).CreatedOn.ToUniversalTime().Date);
                }

                return false;
            }
            catch (RetryLimitExceededException)
            {
                _logger.LogWarning("Local database Error. Retry limit exceeded.");
                return false;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Local database Error.");
                return false;
            }
        }

        private static int? SetMessageType(Type targetType)
        {
            if (Enum.TryParse(targetType.ToString().Split('.').LastOrDefault(), out DeviceType type))
            {
                return (int)type;
            }
            return null;
        }
    }
}
