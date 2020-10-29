using Actors.Models.LocalDbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.Contexts
{
    public class LocalContext : DbContext
    {
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorConfiguration> Configurations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public TemeratureAndHumidity TemeratureAndHumidity {get; set;}
        public Weather WeatherReadings { get; set; }


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gateway>().HasData
             (new Gateway
             {
                 Id = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                 Name = "Raspberry Pi"
             });
            modelBuilder.Entity<Actor>().HasData
              (new Actor
              {
                  Id = new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432"),
                  Type = "GsmModem",
                  GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c")
              });
            modelBuilder.Entity<Actor>().HasData
              (new Actor
              {
                  Id = new Guid("429060a5-7e97-4227-aa44-25999f13536f"),
                  Type = "Relay",
                  GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c")
              });
            modelBuilder.Entity<Actor>().HasData
              (new Actor
              {
                  Id = new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"),
                  Type = "Relay",
                  GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c")
              });
            modelBuilder.Entity<Actor>().HasData
              (new Actor
              {
                  Id = new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"),
                  Type = "TemperatureSensor",
                  GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c")
              });

            modelBuilder.Entity<ActorConfiguration>().HasData
              (new ActorConfiguration
              {
                  Id = 1,
                  UpdatedOn = DateTime.Now,
                  ConfigurationJson = "",
                  ActorId = new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432")
              });
            modelBuilder.Entity<ActorConfiguration>().HasData
              (new ActorConfiguration
              {
                  Id = 2,
                  UpdatedOn = DateTime.Now,
                  ConfigurationJson = "",
                  ActorId = new Guid("429060a5-7e97-4227-aa44-25999f13536f")
              });
            modelBuilder.Entity<ActorConfiguration>().HasData
              (new ActorConfiguration
              {
                  Id = 3,
                  UpdatedOn = DateTime.Now,
                  ConfigurationJson = "",
                  ActorId = new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8")
              });
            modelBuilder.Entity<ActorConfiguration>().HasData
              (new ActorConfiguration
              {
                  Id = 4,
                  UpdatedOn = DateTime.Now,
                  ConfigurationJson = "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Type\":3,\"Name\":\"TemperatureSensor\",\"Attach\":true,\"Interface\":\"wire-1\",\"ReadInterval\":5000,\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\"}",
                  ActorId = new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2")
              });
        }
    }
}
