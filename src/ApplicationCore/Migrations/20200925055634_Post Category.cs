using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Migrations
{
    public partial class PostCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId",
                table: "Post",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Categories_CategoryId",
                table: "Post",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Categories_CategoryId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_CategoryId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Post");
        }
    }
}
