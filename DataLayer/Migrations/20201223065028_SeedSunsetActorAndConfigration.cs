using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.DataAccessLayer.Migrations
{
    public partial class SeedSunsetActorAndConfigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StationName",
                table: "WeatherReadings",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "SunriseSunsets",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "GatewayId", "Type" },
                values: new object[] { new Guid("c1226187-9859-4e8e-ac1f-a27a3bfb5030"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "Sun astronomical data API" });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "ActorId", "ConfigurationJson", "UpdatedOn" },
                values: new object[] { 7, new Guid("c1226187-9859-4e8e-ac1f-a27a3bfb5030"), "", new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("c1226187-9859-4e8e-ac1f-a27a3bfb5030"));

            migrationBuilder.AlterColumn<string>(
                name: "StationName",
                table: "WeatherReadings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "SunriseSunsets",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
