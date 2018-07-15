using Microsoft.EntityFrameworkCore.Migrations;

namespace ITnews.Data.Migrations
{
    public partial class TagsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewTag_News_NewId",
                table: "NewTag");

            migrationBuilder.DropForeignKey(
                name: "FK_NewTag_Tags_TagId",
                table: "NewTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewTag",
                table: "NewTag");

            migrationBuilder.RenameTable(
                name: "NewTag",
                newName: "NewTags");

            migrationBuilder.RenameIndex(
                name: "IX_NewTag_TagId",
                table: "NewTags",
                newName: "IX_NewTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewTags",
                table: "NewTags",
                columns: new[] { "NewId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewTags_News_NewId",
                table: "NewTags",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewTags_Tags_TagId",
                table: "NewTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewTags_News_NewId",
                table: "NewTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NewTags_Tags_TagId",
                table: "NewTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewTags",
                table: "NewTags");

            migrationBuilder.RenameTable(
                name: "NewTags",
                newName: "NewTag");

            migrationBuilder.RenameIndex(
                name: "IX_NewTags_TagId",
                table: "NewTag",
                newName: "IX_NewTag_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewTag",
                table: "NewTag",
                columns: new[] { "NewId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewTag_News_NewId",
                table: "NewTag",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewTag_Tags_TagId",
                table: "NewTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
