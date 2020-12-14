using System;
using IotHubGatewayDaemon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IotHubGatewayDaemon.Contexts
{
    public class LocalContext : DbContext
    {
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorConfiguration> Configurations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TemperatureAndHumidity> Temperatures { get; set; }
        public DbSet<Weather> WeatherReadings { get; set; }
        public DbSet<QueueItemLocal> Queue { get; set; }

        #region Connection Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"/Properties/"))
                .AddJsonFile("dbcontextsettings.json")
                .Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("RpiDbConnection"), builder =>
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
        }
        #endregion
    }
}
