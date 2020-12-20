using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.DataAccessLayer.Migrations
{
    public partial class AddTypeFieldToQueueItemLocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Queue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Queue");
        }
    }
}
