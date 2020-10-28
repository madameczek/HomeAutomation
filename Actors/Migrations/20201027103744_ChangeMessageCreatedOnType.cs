using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actors.Migrations
{
    public partial class ChangeMessageCreatedOnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 27, 11, 37, 44, 484, DateTimeKind.Unspecified).AddTicks(4314), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 27, 11, 37, 44, 486, DateTimeKind.Unspecified).AddTicks(8353), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 27, 11, 37, 44, 486, DateTimeKind.Unspecified).AddTicks(8452), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 27, 11, 37, 44, 486, DateTimeKind.Unspecified).AddTicks(8484), new TimeSpan(0, 1, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 24, 13, 10, 57, 127, DateTimeKind.Unspecified).AddTicks(9869), new TimeSpan(0, 2, 0, 0, 0)));
        }
    }
}
