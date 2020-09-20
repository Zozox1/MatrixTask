using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoorsApi.Migrations
{
    public partial class firstmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Energy = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    LastUsed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Doors",
                columns: new[] { "Id", "Available", "Energy", "LastUsed" },
                values: new object[,]
                {
                    { 1, true, 30, null },
                    { 2, true, 40, null },
                    { 3, true, 20, null },
                    { 4, true, 15, null },
                    { 5, true, 25, null },
                    { 6, true, 10, null },
                    { 7, true, 20, null },
                    { 8, true, 30, null },
                    { 9, true, 10, null },
                    { 10, true, 15, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doors");
        }
    }
}
