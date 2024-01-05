using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToTagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagValue",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Tag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagValue",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Tag");
        }
    }
}
