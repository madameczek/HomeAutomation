using IotHubGateway.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IotHubGateway.Contexts
{
    public class AzureContext : DbContext
    {
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorConfiguration> Configurations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TemperatureAndHumidity> Temperatures { get; set; }
        public DbSet<Weather> WeatherReadings { get; set; }
        public DbSet<QueueItemRemote> Queue { get; set; }

        #region Connection Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory + @"/Properties/")
                .AddJsonFile("dbcontextsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AzureDbConnection"));
        }
        #endregion
    }
}
