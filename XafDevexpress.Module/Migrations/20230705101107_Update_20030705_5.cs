using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20030705_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowField_FlowDiagramDetail_FlowDiagramDetailID",
                table: "FlowField");

            migrationBuilder.RenameColumn(
                name: "FlowDiagramDetailID",
                table: "FlowField",
                newName: "FlowStepID");

            migrationBuilder.RenameIndex(
                name: "IX_FlowField_FlowDiagramDetailID",
                table: "FlowField",
                newName: "IX_FlowField_FlowStepID");

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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_FlowField_FlowStep_FlowStepID",
                table: "FlowField",
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

            migrationBuilder.DropForeignKey(
                name: "FK_FlowField_FlowStep_FlowStepID",
                table: "FlowField");

            migrationBuilder.DropTable(
                name: "FlowStep");

            migrationBuilder.DropIndex(
                name: "IX_FlowDiagramDetail_FlowStepID",
                table: "FlowDiagramDetail");

            migrationBuilder.DropColumn(
                name: "FlowStepID",
                table: "FlowDiagramDetail");

            migrationBuilder.RenameColumn(
                name: "FlowStepID",
                table: "FlowField",
                newName: "FlowDiagramDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_FlowField_FlowStepID",
                table: "FlowField",
                newName: "IX_FlowField_FlowDiagramDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowField_FlowDiagramDetail_FlowDiagramDetailID",
                table: "FlowField",
                column: "FlowDiagramDetailID",
                principalTable: "FlowDiagramDetail",
                principalColumn: "ID");
        }
    }
}
