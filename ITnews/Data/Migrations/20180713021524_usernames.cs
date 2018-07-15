using Microsoft.EntityFrameworkCore.Migrations;

namespace ITnews.Data.Migrations
{
    public partial class usernames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNew_AspNetUsers_ApplicationUserId",
                table: "UserNew");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNew_News_NewId",
                table: "UserNew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNew",
                table: "UserNew");

            migrationBuilder.RenameTable(
                name: "UserNew",
                newName: "UserNews");

            migrationBuilder.RenameIndex(
                name: "IX_UserNew_ApplicationUserId",
                table: "UserNews",
                newName: "IX_UserNews_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNews",
                table: "UserNews",
                columns: new[] { "NewId", "ApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNews_AspNetUsers_ApplicationUserId",
                table: "UserNews",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNews_News_NewId",
                table: "UserNews",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNews_AspNetUsers_ApplicationUserId",
                table: "UserNews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNews_News_NewId",
                table: "UserNews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNews",
                table: "UserNews");

            migrationBuilder.RenameTable(
                name: "UserNews",
                newName: "UserNew");

            migrationBuilder.RenameIndex(
                name: "IX_UserNews_ApplicationUserId",
                table: "UserNew",
                newName: "IX_UserNew_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNew",
                table: "UserNew",
                columns: new[] { "NewId", "ApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNew_AspNetUsers_ApplicationUserId",
                table: "UserNew",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNew_News_NewId",
                table: "UserNew",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
