using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITnews.Data.Migrations
{
    public partial class rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "News",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "News",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
