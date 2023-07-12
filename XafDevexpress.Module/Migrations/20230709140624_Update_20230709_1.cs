using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20230709_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "FlowDiagramDetail");

            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "FlowDiagramDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "FlowDiagramDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FlowDiagram",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FlowDiagramLink",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowDiagramID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourcePortAlignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetPortAlignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowDiagramLink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlowDiagramLink_FlowDiagramDetail_SourceID",
                        column: x => x.SourceID,
                        principalTable: "FlowDiagramDetail",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FlowDiagramLink_FlowDiagramDetail_TargetID",
                        column: x => x.TargetID,
                        principalTable: "FlowDiagramDetail",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FlowDiagramLink_FlowDiagram_FlowDiagramID",
                        column: x => x.FlowDiagramID,
                        principalTable: "FlowDiagram",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagramLink_FlowDiagramID",
                table: "FlowDiagramLink",
                column: "FlowDiagramID");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagramLink_SourceID",
                table: "FlowDiagramLink",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagramLink_TargetID",
                table: "FlowDiagramLink",
                column: "TargetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowDiagramLink");

            migrationBuilder.DropColumn(
                name: "X",
                table: "FlowDiagramDetail");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "FlowDiagramDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FlowDiagram");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "FlowDiagramDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
