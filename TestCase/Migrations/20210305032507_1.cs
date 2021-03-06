using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCase.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    System_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_number = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Source_order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Converted_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
