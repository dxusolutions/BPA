using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20030705_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseField");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseField",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseFlowID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Output = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaseField_BaseFlow_BaseFlowID",
                        column: x => x.BaseFlowID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseField_BaseFlowID",
                table: "BaseField",
                column: "BaseFlowID");
        }
    }
}
