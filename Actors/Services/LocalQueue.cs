using Actors.Contexts;
using Actors.Models.LocalDbModels;
using CommonClasses.Interfaces;
using TemperatureSensor.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ImgwApi.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Actors.Services
{
    public class LocalQueue
    {
        private readonly LocalContext _dbContext;
        private readonly ILogger _logger;
        public LocalQueue(ILogger<LocalQueue> logger, LocalContext dbContext)
        {
            this._dbContext = dbContext;
            _logger = logger;
        }

        public Task AddMessage(IMessage message)
        {
            // Strange looking manipulations to change type from those used by devices to 'Message' used in DbSet.
            // I.e time zone offset is removed from DateTimeOffest type in original message, 
            // because MariaDb makes unwanted computatiom to UTC.

            // Serialized object will be used as proto to deserialize into target type
            // depending on data type to be stored in database.
            message.Id = 0;
            message.IsProcessed = false;

            var _json = JsonConvert.SerializeObject(message);

            bool _isDuplicate = false;
            object dbMessage;
            if(message is TemperatureSensorData)
            {
                dbMessage = JsonConvert.DeserializeObject<TemperatureAndHumidity>(_json);
                _dbContext.Add(dbMessage as TemperatureAndHumidity);
            }
            if (message is WeatherData)
            {
                dbMessage = JsonConvert.DeserializeObject<Weather>(_json);

                _isDuplicate = IsDuplicate(dbMessage as Weather);
                if (!_isDuplicate)
                {
                    _dbContext.Add(dbMessage as Weather); 
                }
            }

            // In addition to above all date are stored in 'blob' format with data serializesd to json.
            if (!_isDuplicate)
            {
                _dbContext.Add(CreateBlobMessage(message));
            }
            int _count;
            try
            {
                _count = _dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Database error in LocalQueue service.");
                throw;
            }
            if (_count > 0)
            {
                _logger.LogDebug("Data from device saved to local database");
            }
            return Task.CompletedTask;
        }

        Message CreateBlobMessage (IMessage message)
        {
            // Create json to be stored as string in MessageBody field of a Message.
            message.Id = null;
            message.IsProcessed = null;
            var _jsonData = JsonConvert.SerializeObject(message, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var _message = JsonConvert.DeserializeObject<Message>(_jsonData);
            _message.Id = 0;
            _message.IsProcessed = false;
            _message.MessageBody = _jsonData;
            return _message;
        }

        bool IsDuplicate(Weather message)
        {
            return _dbContext.Messages.Where(m => m.CreatedOn == message.CreatedOn).Count() > 0;
        }
    }
}
