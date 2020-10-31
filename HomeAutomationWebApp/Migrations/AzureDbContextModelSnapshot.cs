﻿// <auto-generated />
using System;
using HomeAutomationWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeAutomationWebApp.Migrations
{
    [DbContext(typeof(AzureDbContext))]
    partial class AzureDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Actor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        });
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.ActorConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConfigurationJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

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
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 861, DateTimeKind.Unspecified).AddTicks(6104), new TimeSpan(0, 1, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            ActorId = new Guid("429060a5-7e97-4227-aa44-25999f13536f"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2037), new TimeSpan(0, 1, 0, 0, 0))
                        },
                        new
                        {
                            Id = 3,
                            ActorId = new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2154), new TimeSpan(0, 1, 0, 0, 0))
                        },
                        new
                        {
                            Id = 4,
                            ActorId = new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"),
                            ConfigurationJson = "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Type\":3,\"Name\":\"TemperatureSensor\",\"Attach\":true,\"Interface\":\"wire-1\",\"ReadInterval\":5000,\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\"}",
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2194), new TimeSpan(0, 1, 0, 0, 0))
                        },
                        new
                        {
                            Id = 5,
                            ActorId = new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2229), new TimeSpan(0, 1, 0, 0, 0))
                        },
                        new
                        {
                            Id = 6,
                            ActorId = new Guid("5a080659-ccb2-482a-be94-97e668689576"),
                            ConfigurationJson = "",
                            UpdatedOn = new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2301), new TimeSpan(0, 1, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Gateway", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IotUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IotUserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IotUserId1");

                    b.ToTable("Gateways");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"),
                            IotUserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Raspberry Pi"
                        });
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("MessageBody")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.QueueItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("MessageBody")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Queue");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.TemperatureAndHumidity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Humidity")
                        .HasColumnType("float");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<double?>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("AirPressure")
                        .HasColumnType("float");

                    b.Property<double?>("AirTemperature")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Humidity")
                        .HasColumnType("float");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<double?>("Precipitation")
                        .HasColumnType("float");

                    b.Property<int?>("StationId")
                        .HasColumnType("int");

                    b.Property<string>("StationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WindDirection")
                        .HasColumnType("int");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeatherReadings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "888782a7-f068-4454-a1ad-9256ffcd4e17",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "a63ac1a0-ce42-4cf6-931b-a830cd25fcff",
                            Name = "SiteManager",
                            NormalizedName = "SITEMANAGER"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "3db1ce9c-cc80-44bd-b375-8d994a62d983",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.IotUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("IotUser");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Actor", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.Gateway", null)
                        .WithMany("Actors")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.ActorConfiguration", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.Actor", null)
                        .WithOne("Configuration")
                        .HasForeignKey("HomeAutomationWebApp.Models.DbModels.ActorConfiguration", "ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Gateway", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.IotUser", null)
                        .WithMany("Gateways")
                        .HasForeignKey("IotUserId1");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.Message", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.Actor", null)
                        .WithMany("Messages")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.QueueItem", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.Actor", null)
                        .WithMany("QueueItems")
                        .HasForeignKey("ActorId");
                });

            modelBuilder.Entity("HomeAutomationWebApp.Models.DbModels.TemperatureAndHumidity", b =>
                {
                    b.HasOne("HomeAutomationWebApp.Models.DbModels.Actor", null)
                        .WithMany("Temperatures")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
