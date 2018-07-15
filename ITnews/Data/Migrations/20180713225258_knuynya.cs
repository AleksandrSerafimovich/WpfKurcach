using Microsoft.EntityFrameworkCore.Migrations;

namespace ITnews.Data.Migrations
{
    public partial class knuynya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_AspNetUsers_ApplicationUserId",
                table: "UserComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_Comments_СommentId",
                table: "UserComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComment",
                table: "UserComment");

            migrationBuilder.RenameTable(
                name: "UserComment",
                newName: "UserComments");

            migrationBuilder.RenameIndex(
                name: "IX_UserComment_ApplicationUserId",
                table: "UserComments",
                newName: "IX_UserComments_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComments",
                table: "UserComments",
                columns: new[] { "СommentId", "ApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_AspNetUsers_ApplicationUserId",
                table: "UserComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_Comments_СommentId",
                table: "UserComments",
                column: "СommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_AspNetUsers_ApplicationUserId",
                table: "UserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_Comments_СommentId",
                table: "UserComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComments",
                table: "UserComments");

            migrationBuilder.RenameTable(
                name: "UserComments",
                newName: "UserComment");

            migrationBuilder.RenameIndex(
                name: "IX_UserComments_ApplicationUserId",
                table: "UserComment",
                newName: "IX_UserComment_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComment",
                table: "UserComment",
                columns: new[] { "СommentId", "ApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_AspNetUsers_ApplicationUserId",
                table: "UserComment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_Comments_СommentId",
                table: "UserComment",
                column: "СommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
