using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniKanbanWeb.Migrations
{
    public partial class tag3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Cards_CardId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CardId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CardId",
                table: "Tags",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Cards_CardId",
                table: "Tags",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }
    }
}
