using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniKanbanWeb.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Boards",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Boards");
        }
    }
}
