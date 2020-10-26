﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.Migrations
{
    public partial class PostText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Posts");
        }
    }
}
