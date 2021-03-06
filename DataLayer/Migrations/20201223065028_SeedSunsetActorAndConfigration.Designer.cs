﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.DataAccessLayer.Migrations
{
    [DbContext(typeof(LocalContext))]
    [Migration("20201223065028_SeedSunsetActorAndConfigration")]
    partial class SeedSunsetActorAndConfigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataLayer.Models.Actor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "GsmModem"
                        },
                        new
                        {
                            Id = new Guid("429060a5-7e97-4227-aa44-25999f13536f"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "Relay"
                        },
                        new
                        {
                            Id = new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "Relay"
                        },
                        new
                        {
                            Id = new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "TemperatureSensor"
                        },
                        new
                        {
                            Id = new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "TemperatureSensor"
                        },
                        new
                        {
                            Id = new Guid("5a080659-ccb2-482a-be94-97e668689576"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "WeatherData"
                        },
                        new
                        {
                            Id = new Guid("c1226187-9859-4e8e-ac1f-a27a3bfb5030"),
                            GatewayId = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Type = "Sun astronomical data API"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.ActorConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConfigurationJson")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ActorId")
                        .IsUnique();

                    b.ToTable("Configurations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActorId = new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 2,
                            ActorId = new Guid("429060a5-7e97-4227-aa44-25999f13536f"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 3,
                            ActorId = new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 4,
                            ActorId = new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"),
                            ConfigurationJson = "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Type\":3,\"Name\":\"TemperatureSensor\",\"Attach\":true,\"Interface\":\"wire-1\",\"ReadInterval\":5000,\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\"}",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 5,
                            ActorId = new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 6,
                            ActorId = new Guid("5a080659-ccb2-482a-be94-97e668689576"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 7,
                            ActorId = new Guid("c1226187-9859-4e8e-ac1f-a27a3bfb5030"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Gateway", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SiteName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Gateways");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            Name = "Raspberry Pi"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MessageBody")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DataLayer.Models.QueueItemLocal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid?>("ActorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MessageBody")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Queue");
                });

            modelBuilder.Entity("DataLayer.Models.SunriseSunset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AstronomicalTwilightBegin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("AstronomicalTwilightEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CivilTwilightBegin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CivilTwilightEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("DayLength")
                        .HasColumnType("time(6)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<DateTime>("NauticalTwilightBegin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NauticalTwilightEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("SolarNoon")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Sunrise")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Sunset")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("SunriseSunsets");
                });

            modelBuilder.Entity("DataLayer.Models.TemperatureAndHumidity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("Humidity")
                        .HasColumnType("double");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<double?>("Temperature")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("DataLayer.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("AirPressure")
                        .HasColumnType("double");

                    b.Property<double?>("AirTemperature")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("Humidity")
                        .HasColumnType("double");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<double?>("Precipitation")
                        .HasColumnType("double");

                    b.Property<int?>("StationId")
                        .HasColumnType("int");

                    b.Property<string>("StationName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int?>("WindDirection")
                        .HasColumnType("int");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("WeatherReadings");
                });

            modelBuilder.Entity("DataLayer.Models.Actor", b =>
                {
                    b.HasOne("DataLayer.Models.Gateway", null)
                        .WithMany("Actors")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.ActorConfiguration", b =>
                {
                    b.HasOne("DataLayer.Models.Actor", null)
                        .WithOne("Configuration")
                        .HasForeignKey("DataLayer.Models.ActorConfiguration", "ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Message", b =>
                {
                    b.HasOne("DataLayer.Models.Actor", null)
                        .WithMany("Messages")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.QueueItemLocal", b =>
                {
                    b.HasOne("DataLayer.Models.Actor", null)
                        .WithMany("QueueItems")
                        .HasForeignKey("ActorId");
                });

            modelBuilder.Entity("DataLayer.Models.TemperatureAndHumidity", b =>
                {
                    b.HasOne("DataLayer.Models.Actor", null)
                        .WithMany("Temperatures")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
