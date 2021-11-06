using Microsoft.EntityFrameworkCore.Migrations;

namespace Timecards.Infrastructure.EF.Migrations
{
    public partial class fixtimecardsItmenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimecardsItem_Timecardses_Id",
                table: "TimecardsItem");

            migrationBuilder.CreateIndex(
                name: "IX_TimecardsItem_TimecardsId",
                table: "TimecardsItem",
                column: "TimecardsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimecardsItem_Timecardses_TimecardsId",
                table: "TimecardsItem",
                column: "TimecardsId",
                principalTable: "Timecardses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimecardsItem_Timecardses_TimecardsId",
                table: "TimecardsItem");

            migrationBuilder.DropIndex(
                name: "IX_TimecardsItem_TimecardsId",
                table: "TimecardsItem");

            migrationBuilder.AddForeignKey(
                name: "FK_TimecardsItem_Timecardses_Id",
                table: "TimecardsItem",
                column: "Id",
                principalTable: "Timecardses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
