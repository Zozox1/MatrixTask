using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkdayManagement_API.Migrations
{
    public partial class intmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(nullable: false),
                    DailyDoors = table.Column<int>(nullable: false),
                    AccomplishDate = table.Column<DateTime>(nullable: false),
                    AccomplishGoals = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(nullable: false),
                    DeplatedDoors = table.Column<string>(nullable: true),
                    DateOfWork = table.Column<DateTime>(nullable: false),
                    WorkerEnergyUnits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Work");
        }
    }
}
