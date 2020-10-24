using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actors.Migrations
{
    public partial class AdditionalSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 13, 10, 57, 125, DateTimeKind.Unspecified).AddTicks(1888), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 13, 10, 57, 127, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 13, 10, 57, 127, DateTimeKind.Unspecified).AddTicks(9837), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConfigurationJson", "UpdatedOn" },
                values: new object[] { "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Type\":3,\"Name\":\"TemperatureSensor\",\"Attach\":true,\"Interface\":\"wire-1\",\"ReadInterval\":5000,\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\"}", new DateTimeOffset(new DateTime(2020, 10, 24, 13, 10, 57, 127, DateTimeKind.Unspecified).AddTicks(9869), new TimeSpan(0, 2, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 448, DateTimeKind.Unspecified).AddTicks(7171), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5277), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5388), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConfigurationJson", "UpdatedOn" },
                values: new object[] { "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Interface\":\"wire-1\",\"Type\":\"TemperatureSensor\",\"Name\":\"TemperatureSensor\",\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\",\"ReadInterval\":5000,\"Attach\":true}", new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5422), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
