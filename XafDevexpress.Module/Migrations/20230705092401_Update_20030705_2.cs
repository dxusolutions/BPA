using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20030705_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    Output = table.Column<bool>(type: "bit", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
