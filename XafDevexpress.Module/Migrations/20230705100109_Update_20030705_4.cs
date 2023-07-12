using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20030705_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowDiagramDetail_FlowStep_FlowStepID",
                table: "FlowDiagramDetail");

            migrationBuilder.DropTable(
                name: "FlowStep");

            migrationBuilder.DropIndex(
                name: "IX_FlowDiagramDetail_FlowStepID",
                table: "FlowDiagramDetail");

            migrationBuilder.DropColumn(
                name: "FlowStepID",
                table: "FlowDiagramDetail");

            migrationBuilder.CreateTable(
                name: "FlowField",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowDiagramDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyleFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    Output = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlowField_FlowDiagramDetail_FlowDiagramDetailID",
                        column: x => x.FlowDiagramDetailID,
                        principalTable: "FlowDiagramDetail",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowField_FlowDiagramDetailID",
                table: "FlowField",
                column: "FlowDiagramDetailID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowField");

            migrationBuilder.AddColumn<Guid>(
                name: "FlowStepID",
                table: "FlowDiagramDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FlowStep",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Output = table.Column<bool>(type: "bit", nullable: false),
                    Tyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyleFullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStep", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagramDetail_FlowStepID",
                table: "FlowDiagramDetail",
                column: "FlowStepID");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowDiagramDetail_FlowStep_FlowStepID",
                table: "FlowDiagramDetail",
                column: "FlowStepID",
                principalTable: "FlowStep",
                principalColumn: "ID");
        }
    }
}
