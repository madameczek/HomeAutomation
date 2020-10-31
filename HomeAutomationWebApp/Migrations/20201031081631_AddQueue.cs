using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomationWebApp.Migrations
{
    public partial class AddQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueueItems",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueItems");

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
    }
}
