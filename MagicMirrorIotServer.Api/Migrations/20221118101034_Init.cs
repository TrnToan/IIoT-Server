using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicMirrorIotServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "iotserver");

            migrationBuilder.CreateSequence(
                name: "deviceeq",
                schema: "iotserver",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "eonnodeeq",
                schema: "iotserver",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "tageq",
                schema: "iotserver",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EonNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EonNodes", x => x.Id);
                    table.UniqueConstraint("AK_EonNodes_NodeId", x => x.NodeId);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NodeId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.UniqueConstraint("AK_Device_DeviceId_NodeId", x => new { x.DeviceId, x.NodeId });
                    table.ForeignKey(
                        name: "FK_Device_EonNodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "EonNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TagType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.UniqueConstraint("AK_Tag_TagId_DeviceId", x => new { x.TagId, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_Tag_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_NodeId",
                table: "Device",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_DeviceId",
                table: "Tag",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "EonNodes");

            migrationBuilder.DropSequence(
                name: "deviceeq",
                schema: "iotserver");

            migrationBuilder.DropSequence(
                name: "eonnodeeq",
                schema: "iotserver");

            migrationBuilder.DropSequence(
                name: "tageq",
                schema: "iotserver");
        }
    }
}
