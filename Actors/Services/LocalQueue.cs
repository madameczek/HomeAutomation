using Actors.Contexts;
using Actors.Models.LocalDbModels;
using CommonClasses.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.Services
{
    public class LocalQueue
    {
        private readonly LocalContext dbContext;

        public LocalQueue(LocalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddMessage(IMessage message)
        {
            var _json = JsonConvert.SerializeObject(message);
            Message dbMessage = JsonConvert.DeserializeObject<Message>(_json);
            dbContext.Add(dbMessage);
            dbContext.SaveChanges();
            //remove for production
            Console.WriteLine($"{message.CreatedOn}, Temp: {message.MessageBodyJson}");
        }
    }
}
