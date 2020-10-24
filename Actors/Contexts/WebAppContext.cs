using Actors.Models.LocalDbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Actors.Contexts
{
    public class WebAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory + @"/Properties/")
                .AddJsonFile("dbcontextsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LocalSqlExpressConnection"));
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorConfiguration> Configurations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
