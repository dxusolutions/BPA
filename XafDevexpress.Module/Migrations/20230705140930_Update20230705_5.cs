using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update20230705_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseFlowField",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseFlowID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyleFullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseFlowField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaseFlowField_BaseFlow_BaseFlowID",
                        column: x => x.BaseFlowID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseFlowField_BaseFlowID",
                table: "BaseFlowField",
                column: "BaseFlowID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseFlowField");
        }
    }
}
