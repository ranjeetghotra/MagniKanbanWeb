using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniKanbanWeb.Migrations
{
    public partial class mi55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Timeline_AspNetUsers_UserId",
                table: "Timeline");

            migrationBuilder.DropForeignKey(
                name: "FK_Timeline_Cards_CardId",
                table: "Timeline");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timeline",
                table: "Timeline");

            migrationBuilder.DropIndex(
                name: "IX_Timeline_UserId",
                table: "Timeline");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Timeline");

            migrationBuilder.RenameTable(
                name: "Timeline",
                newName: "Timelines");

            migrationBuilder.RenameIndex(
                name: "IX_Timeline_CardId",
                table: "Timelines",
                newName: "IX_Timelines_CardId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Timelines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timelines",
                table: "Timelines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelines_Cards_CardId",
                table: "Timelines",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelines_Cards_CardId",
                table: "Timelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timelines",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Timelines");

            migrationBuilder.RenameTable(
                name: "Timelines",
                newName: "Timeline");

            migrationBuilder.RenameIndex(
                name: "IX_Timelines_CardId",
                table: "Timeline",
                newName: "IX_Timeline_CardId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Timeline",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timeline",
                table: "Timeline",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Timeline_UserId",
                table: "Timeline",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timeline_AspNetUsers_UserId",
                table: "Timeline",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timeline_Cards_CardId",
                table: "Timeline",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
