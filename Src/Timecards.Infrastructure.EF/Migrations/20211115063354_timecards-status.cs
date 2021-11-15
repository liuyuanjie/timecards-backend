using Microsoft.EntityFrameworkCore.Migrations;

namespace Timecards.Infrastructure.EF.Migrations
{
    public partial class timecardsstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "StatusType",
                table: "Timecardses",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "Timecardses");
        }
    }
}
