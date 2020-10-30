using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class ChangeColumnNameInMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageBodyJson",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "MessageBody",
                table: "Messages",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "282026e2-a1f2-4210-af78-bfdf6bcfe7a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "59601d52-5690-4d44-908f-567ad4d60afd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ead9b8eb-3afb-4ef5-8ef8-3675f3d23359");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 848, DateTimeKind.Unspecified).AddTicks(460), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 850, DateTimeKind.Unspecified).AddTicks(6897), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 850, DateTimeKind.Unspecified).AddTicks(7008), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 850, DateTimeKind.Unspecified).AddTicks(7044), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 850, DateTimeKind.Unspecified).AddTicks(7076), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 23, 6, 20, 850, DateTimeKind.Unspecified).AddTicks(7146), new TimeSpan(0, 1, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageBody",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "MessageBodyJson",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(2038), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 29, 22, 14, 44, 30, DateTimeKind.Unspecified).AddTicks(2108), new TimeSpan(0, 1, 0, 0, 0)));
        }
    }
}
