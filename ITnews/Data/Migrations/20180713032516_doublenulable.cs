using Microsoft.EntityFrameworkCore.Migrations;

namespace ITnews.Data.Migrations
{
    public partial class doublenulable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "UserNews",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "UserNews",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
