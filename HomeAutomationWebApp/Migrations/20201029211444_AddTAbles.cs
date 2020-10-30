using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class AddTAbles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false),
                    Temperature = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    ActorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperatures_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false),
                    AirTemperature = table.Column<double>(nullable: true),
                    AirPressure = table.Column<double>(nullable: true),
                    Precipitation = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    WindSpeed = table.Column<double>(nullable: true),
                    WindDirection = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: true),
                    StationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReadings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "GatewayId", "Type" },
                values: new object[,]
                {
                    { new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "TemperatureSensor" },
                    { new Guid("5a080659-ccb2-482a-be94-97e668689576"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "WeatherData" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5e838df1-129d-4c1b-922b-170566900d80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d223eca0-feb1-4215-9ef4-91436269502b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1526e297-505c-4009-9b99-22b27e13ec09");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 27, DateTimeKind.Unspecified).AddTicks(4280), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(1844), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(1966), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(2002), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "ActorId", "ConfigurationJson", "UpdatedOn" },
                values: new object[] { 5, new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"), "", new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(2038), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "ActorId", "ConfigurationJson", "UpdatedOn" },
                values: new object[] { 6, new Guid("5a080659-ccb2-482a-be94-97e668689576"), "", new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(2108), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_ActorId",
                table: "Temperatures",
                column: "ActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "WeatherReadings");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("592d93fa-9d3e-42cc-a65f-9adcb77d98e1"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("5a080659-ccb2-482a-be94-97e668689576"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a2aedabb-ec21-417b-82c4-86148d708cf9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c38c0259-fd33-4bb0-bde4-40b2d49e0b8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "83c86112-0139-4f3c-9be5-6fd16b1ad40c");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 25, 20, 22, 38, 30, DateTimeKind.Unspecified).AddTicks(6873), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 25, 20, 22, 38, 33, DateTimeKind.Unspecified).AddTicks(1028), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 25, 20, 22, 38, 33, DateTimeKind.Unspecified).AddTicks(1128), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 25, 20, 22, 38, 33, DateTimeKind.Unspecified).AddTicks(1161), new TimeSpan(0, 1, 0, 0, 0)));
        }
    }
}
