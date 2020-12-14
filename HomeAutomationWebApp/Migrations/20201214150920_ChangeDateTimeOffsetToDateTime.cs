using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class ChangeDateTimeOffsetToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "WeatherReadings",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Temperatures",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Queue",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0d9ad7a2-ea0f-4851-94a2-8a9ade314dfb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2e5223ca-be01-4a5c-8ed3-fe838c593e00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "39218674-8a4d-46bb-a5d1-e7c7ba9d2c94");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 622, DateTimeKind.Local).AddTicks(6905));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 625, DateTimeKind.Local).AddTicks(3998));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 625, DateTimeKind.Local).AddTicks(4088));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 625, DateTimeKind.Local).AddTicks(4118));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 625, DateTimeKind.Local).AddTicks(4187));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 16, 9, 19, 625, DateTimeKind.Local).AddTicks(4220));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "WeatherReadings",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Temperatures",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Queue",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Messages",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d495b483-49f8-4c30-ab2e-d0565776708a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "28eb1715-b09d-4424-9f6a-8302f31018ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "eaa4834b-779c-4317-a666-1638ca67bfa0");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 416, DateTimeKind.Local).AddTicks(6944));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 419, DateTimeKind.Local).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 419, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 419, DateTimeKind.Local).AddTicks(3141));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 419, DateTimeKind.Local).AddTicks(3168));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 11, 25, 22, 49, 51, 419, DateTimeKind.Local).AddTicks(3200));
        }
    }
}
