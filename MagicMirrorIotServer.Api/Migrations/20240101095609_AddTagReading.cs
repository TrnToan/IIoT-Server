using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTagReading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoolTagReadings",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoolTagReadings", x => new { x.TagId, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "DoubleTagReadings",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleTagReadings", x => new { x.TagId, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "IntTagReadings",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntTagReadings", x => new { x.TagId, x.Timestamp });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoolTagReadings");

            migrationBuilder.DropTable(
                name: "DoubleTagReadings");

            migrationBuilder.DropTable(
                name: "IntTagReadings");
        }
    }
}
