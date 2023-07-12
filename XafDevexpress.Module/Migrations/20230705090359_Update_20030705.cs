using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20030705 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackOfficeFlow");

            migrationBuilder.DropTable(
                name: "ConfirmLastDayFlow");

            migrationBuilder.DropTable(
                name: "InterviewFlow");

            migrationBuilder.DropTable(
                name: "RequestLastDayFlow");

            migrationBuilder.DropTable(
                name: "ScanCvFlow");

            migrationBuilder.DropTable(
                name: "SubmitLastDayFlow");

            migrationBuilder.DropColumn(
                name: "CurrentFlow",
                table: "BaseFlow");

            migrationBuilder.CreateTable(
                name: "BaseField",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseFlowID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input = table.Column<bool>(type: "bit", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseField");

            migrationBuilder.AddColumn<string>(
                name: "CurrentFlow",
                table: "BaseFlow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BackOfficeFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteDetail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BackOfficeFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmLastDayFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmLastDayFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConfirmLastDayFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfirmLastDayFlow_Employee_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InterviewFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterviewerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InterviewStatus = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InterviewFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewFlow_Employee_InterviewerID",
                        column: x => x.InterviewerID,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RequestLastDayFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestLastDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLastDayFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestLastDayFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestLastDayFlow_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ScanCvFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkCV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanCvFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScanCvFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmitLastDayFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitLastDayFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubmitLastDayFlow_BaseFlow_ID",
                        column: x => x.ID,
                        principalTable: "BaseFlow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmitLastDayFlow_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubmitLastDayFlow_Employee_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmLastDayFlow_ManagerID",
                table: "ConfirmLastDayFlow",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFlow_InterviewerID",
                table: "InterviewFlow",
                column: "InterviewerID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLastDayFlow_EmployeeID",
                table: "RequestLastDayFlow",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitLastDayFlow_EmployeeID",
                table: "SubmitLastDayFlow",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitLastDayFlow_ManagerID",
                table: "SubmitLastDayFlow",
                column: "ManagerID");
        }
    }
}
