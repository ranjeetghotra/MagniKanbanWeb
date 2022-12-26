using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniKanbanWeb.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardsModelId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Cards_CardsModelId",
                        column: x => x.CardsModelId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CardsModelId",
                table: "Comments",
                column: "CardsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CardsModelId",
                table: "Tag",
                column: "CardsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Cards_CardsModelId",
                table: "Comments",
                column: "CardsModelId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Cards_CardsModelId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CardsModelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CardsModelId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "TaskId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
