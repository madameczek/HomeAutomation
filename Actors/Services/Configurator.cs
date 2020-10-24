using Actors.Contexts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.Services
{
    public class Configurator
    {
        private LocalContext context;
        IConfiguration configuration;

        public Configurator(LocalContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public static string Serialize(IDictionary<string, string> dataPairs)
        {
            try
            {
                return JsonConvert.SerializeObject(dataPairs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IDictionary<string, string> Deserialize(string configurationJson)
        {
            try
            {
                var dataPairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(configurationJson);
                return dataPairs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
