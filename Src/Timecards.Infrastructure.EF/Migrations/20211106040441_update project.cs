using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timecards.Infrastructure.EF.Migrations
{
    public partial class updateproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Hour",
                table: "TimecardsItem",
                type: "decimal(3,1)",
                precision: 3,
                scale: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProjectId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentProjectId",
                table: "Projects");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hour",
                table: "TimecardsItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldPrecision: 3,
                oldScale: 1);
        }
    }
}
