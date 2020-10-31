using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class ChangeQueueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueItems");

            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    MessageBody = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queue_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "888782a7-f068-4454-a1ad-9256ffcd4e17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a63ac1a0-ce42-4cf6-931b-a830cd25fcff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "3db1ce9c-cc80-44bd-b375-8d994a62d983");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 861, DateTimeKind.Unspecified).AddTicks(6104), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2037), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2154), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2194), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2229), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 38, 11, 864, DateTimeKind.Unspecified).AddTicks(2301), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Queue_ActorId",
                table: "Queue",
                column: "ActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.CreateTable(
                name: "QueueItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueItems_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ebd2929a-5747-42bc-bd72-5fa37e565cf8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "16c35c77-da40-473b-943d-3a59f4a474b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "7347201e-40e5-4aee-9451-4dc5e35d1a20");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 329, DateTimeKind.Unspecified).AddTicks(5699), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 332, DateTimeKind.Unspecified).AddTicks(816), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 332, DateTimeKind.Unspecified).AddTicks(916), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 332, DateTimeKind.Unspecified).AddTicks(952), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 332, DateTimeKind.Unspecified).AddTicks(983), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTimeOffset(new DateTime(2020, 10, 31, 9, 16, 31, 332, DateTimeKind.Unspecified).AddTicks(1048), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_ActorId",
                table: "QueueItems",
                column: "ActorId");
        }
    }
}
