using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_EonNodes_NodeId",
                table: "Device");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EonNodes_NodeId",
                table: "EonNodes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Device_DeviceId_NodeId",
                table: "Device");

            migrationBuilder.RenameColumn(
                name: "NodeName",
                table: "EonNodes",
                newName: "EonNodeName");

            migrationBuilder.RenameColumn(
                name: "NodeId",
                table: "EonNodes",
                newName: "EonNodeId");

            migrationBuilder.RenameColumn(
                name: "NodeId",
                table: "Device",
                newName: "EonNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_NodeId",
                table: "Device",
                newName: "IX_Device_EonNodeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EonNodes_EonNodeId",
                table: "EonNodes",
                column: "EonNodeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Device_DeviceId_EonNodeId",
                table: "Device",
                columns: new[] { "DeviceId", "EonNodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Device_EonNodes_EonNodeId",
                table: "Device",
                column: "EonNodeId",
                principalTable: "EonNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_EonNodes_EonNodeId",
                table: "Device");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EonNodes_EonNodeId",
                table: "EonNodes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Device_DeviceId_EonNodeId",
                table: "Device");

            migrationBuilder.RenameColumn(
                name: "EonNodeName",
                table: "EonNodes",
                newName: "NodeName");

            migrationBuilder.RenameColumn(
                name: "EonNodeId",
                table: "EonNodes",
                newName: "NodeId");

            migrationBuilder.RenameColumn(
                name: "EonNodeId",
                table: "Device",
                newName: "NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_EonNodeId",
                table: "Device",
                newName: "IX_Device_NodeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EonNodes_NodeId",
                table: "EonNodes",
                column: "NodeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Device_DeviceId_NodeId",
                table: "Device",
                columns: new[] { "DeviceId", "NodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Device_EonNodes_NodeId",
                table: "Device",
                column: "NodeId",
                principalTable: "EonNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
