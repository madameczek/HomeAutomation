using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class ChangeIotUserFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Firstname");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b91fe005-a409-4f68-8761-e496a5aab3be");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6c2c292e-d44f-49dd-9590-9d931f9a64fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c5be0dd8-bde3-416a-a063-d3a3172c9083");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 37, DateTimeKind.Local).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 40, DateTimeKind.Local).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 40, DateTimeKind.Local).AddTicks(1093));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 40, DateTimeKind.Local).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 40, DateTimeKind.Local).AddTicks(1151));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 12, 14, 17, 5, 56, 40, DateTimeKind.Local).AddTicks(1181));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "AspNetUsers",
                newName: "FirstName");

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
    }
}
