﻿using Actors.Contexts;
using Actors.Models.LocalDbModels;
using CommonClasses.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void AddMessage(IMessage message)
        {
            // This is to change type from those used by devices to 'Message' used in DbSet.
            var _json = JsonConvert.SerializeObject(message);
            Message dbMessage = JsonConvert.DeserializeObject<Message>(_json);

            _dbContext.Add(dbMessage);
            _dbContext.SaveChanges();
            _logger.LogDebug("Data from device saved to local database");
        }
    }
}
