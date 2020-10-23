﻿// <auto-generated />
using System;
using Actors.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Actors.Migrations
{
    [DbContext(typeof(LocalContext))]
    partial class LocalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Actors.Models.LocalDbModels.Actor", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("GatewayGuid")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GatewayGuid");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.ActorConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("ActorId")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("ConfigurationJson")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedOn")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ActorId")
                        .IsUnique();

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.Gateway", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SiteName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Gateways");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("ActorId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MessageBodyJson")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.Actor", b =>
                {
                    b.HasOne("Actors.Models.LocalDbModels.Gateway", "Gateway")
                        .WithMany("Actors")
                        .HasForeignKey("GatewayGuid");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.ActorConfiguration", b =>
                {
                    b.HasOne("Actors.Models.LocalDbModels.Actor", "Actor")
                        .WithOne("Configuration")
                        .HasForeignKey("Actors.Models.LocalDbModels.ActorConfiguration", "ActorId");
                });

            modelBuilder.Entity("Actors.Models.LocalDbModels.Message", b =>
                {
                    b.HasOne("Actors.Models.LocalDbModels.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");
                });
#pragma warning restore 612, 618
        }
    }
}
