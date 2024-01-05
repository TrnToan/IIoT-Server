using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenamePrototypeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrototypeId",
                table: "Device",
                newName: "ProtocolId");

            migrationBuilder.RenameColumn(
                name: "DevicePrototype",
                table: "Device",
                newName: "DeviceProtocol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProtocolId",
                table: "Device",
                newName: "PrototypeId");

            migrationBuilder.RenameColumn(
                name: "DeviceProtocol",
                table: "Device",
                newName: "DevicePrototype");
        }
    }
}
