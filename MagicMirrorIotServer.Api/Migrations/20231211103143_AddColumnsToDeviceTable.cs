using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevicePrototype",
                table: "Device",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrototypeId",
                table: "Device",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevicePrototype",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "PrototypeId",
                table: "Device");
        }
    }
}
