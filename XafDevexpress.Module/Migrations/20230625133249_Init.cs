using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionPropertiesString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PivotGridSettingsContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ChartSettingsContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuditEFCoreWeakReference",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefaultString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEFCoreWeakReference", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DashboardData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SynchronizeTitle = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirhDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllDay = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RecurrenceInfoXml = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RecurrencePatternID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReminderInfoXml = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RemindInSeconds = table.Column<int>(type: "int", nullable: false),
                    AlarmTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPostponed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Event_Event_RecurrencePatternID",
                        column: x => x.RecurrencePatternID,
                        principalTable: "Event",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FileData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FlowDiagram",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextFlowDiagramID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowDiagram", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlowDiagram_FlowDiagram_NextFlowDiagramID",
                        column: x => x.NextFlowDiagramID,
                        principalTable: "FlowDiagram",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "KpiDefinition",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TargetObjectTypeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GreenZone = table.Column<float>(type: "real", nullable: false),
                    RedZone = table.Column<float>(type: "real", nullable: false),
                    Compare = table.Column<bool>(type: "bit", nullable: false),
                    RangeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangeToCompareName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementFrequency = table.Column<int>(type: "int", nullable: false),
                    MeasurementMode = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuppressedSeries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableCustomizeRepresentation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiDefinition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KpiScorecard",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiScorecard", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportDataV2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInplaceReport = table.Column<bool>(type: "bit", nullable: false),
                    PredefinedReportTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParametersObjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDataV2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_Int = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AuditData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditedObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OldObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuditData_AuditEFCoreWeakReference_AuditedObjectID",
                        column: x => x.AuditedObjectID,
                        principalTable: "AuditEFCoreWeakReference",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AuditData_AuditEFCoreWeakReference_NewObjectID",
                        column: x => x.NewObjectID,
                        principalTable: "AuditEFCoreWeakReference",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AuditData_AuditEFCoreWeakReference_OldObjectID",
                        column: x => x.OldObjectID,
                        principalTable: "AuditEFCoreWeakReference",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AuditData_AuditEFCoreWeakReference_UserObjectID",
                        column: x => x.UserObjectID,
                        principalTable: "AuditEFCoreWeakReference",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FlowDiagramDetail",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowDiagramID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowDiagramDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlowDiagramDetail_FlowDiagram_FlowDiagramID",
                        column: x => x.FlowDiagramID,
                        principalTable: "FlowDiagram",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpiInstance",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpiDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForceMeasurementDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Settings = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiInstance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KpiInstance_KpiDefinition_KpiDefinitionID",
                        column: x => x.KpiDefinitionID,
                        principalTable: "KpiDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventResource",
                columns: table => new
                {
                    EventsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourcesKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResource", x => new { x.EventsID, x.ResourcesKey });
                    table.ForeignKey(
                        name: "FK_EventResource_Event_EventsID",
                        column: x => x.EventsID,
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventResource_Resource_ResourcesKey",
                        column: x => x.ResourcesKey,
                        principalTable: "Resource",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseFlow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevFlow = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextFlow = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginFlow = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FlowDiagramDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentFlow = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseFlow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaseFlow_FlowDiagramDetail_FlowDiagramDetailID",
                        column: x => x.FlowDiagramDetailID,
                        principalTable: "FlowDiagramDetail",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "KpiHistoryItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpiInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RangeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RangeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiHistoryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KpiHistoryItem_KpiInstance_KpiInstanceID",
                        column: x => x.KpiInstanceID,
                        principalTable: "KpiInstance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpiInstanceKpiScorecard",
                columns: table => new
                {
                    IndicatorsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScorecardsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiInstanceKpiScorecard", x => new { x.IndicatorsID, x.ScorecardsID });
                    table.ForeignKey(
                        name: "FK_KpiInstanceKpiScorecard_KpiInstance_IndicatorsID",
                        column: x => x.IndicatorsID,
                        principalTable: "KpiInstance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpiInstanceKpiScorecard_KpiScorecard_ScorecardsID",
                        column: x => x.ScorecardsID,
                        principalTable: "KpiScorecard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterviewerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InterviewStatus = table.Column<int>(type: "int", nullable: false)
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
                    RequestLastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "IX_AuditData_AuditedObjectID",
                table: "AuditData",
                column: "AuditedObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditData_NewObjectID",
                table: "AuditData",
                column: "NewObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditData_OldObjectID",
                table: "AuditData",
                column: "OldObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditData_UserObjectID",
                table: "AuditData",
                column: "UserObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEFCoreWeakReference_Key_TypeName",
                table: "AuditEFCoreWeakReference",
                columns: new[] { "Key", "TypeName" });

            migrationBuilder.CreateIndex(
                name: "IX_BaseFlow_FlowDiagramDetailID",
                table: "BaseFlow",
                column: "FlowDiagramDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmLastDayFlow_ManagerID",
                table: "ConfirmLastDayFlow",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_RecurrencePatternID",
                table: "Event",
                column: "RecurrencePatternID");

            migrationBuilder.CreateIndex(
                name: "IX_EventResource_ResourcesKey",
                table: "EventResource",
                column: "ResourcesKey");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagram_NextFlowDiagramID",
                table: "FlowDiagram",
                column: "NextFlowDiagramID");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDiagramDetail_FlowDiagramID",
                table: "FlowDiagramDetail",
                column: "FlowDiagramID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFlow_InterviewerID",
                table: "InterviewFlow",
                column: "InterviewerID");

            migrationBuilder.CreateIndex(
                name: "IX_KpiHistoryItem_KpiInstanceID",
                table: "KpiHistoryItem",
                column: "KpiInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_KpiInstance_KpiDefinitionID",
                table: "KpiInstance",
                column: "KpiDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_KpiInstanceKpiScorecard_ScorecardsID",
                table: "KpiInstanceKpiScorecard",
                column: "ScorecardsID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analysis");

            migrationBuilder.DropTable(
                name: "AuditData");

            migrationBuilder.DropTable(
                name: "BackOfficeFlow");

            migrationBuilder.DropTable(
                name: "ConfirmLastDayFlow");

            migrationBuilder.DropTable(
                name: "DashboardData");

            migrationBuilder.DropTable(
                name: "EventResource");

            migrationBuilder.DropTable(
                name: "FileData");

            migrationBuilder.DropTable(
                name: "InterviewFlow");

            migrationBuilder.DropTable(
                name: "KpiHistoryItem");

            migrationBuilder.DropTable(
                name: "KpiInstanceKpiScorecard");

            migrationBuilder.DropTable(
                name: "ReportDataV2");

            migrationBuilder.DropTable(
                name: "RequestLastDayFlow");

            migrationBuilder.DropTable(
                name: "ScanCvFlow");

            migrationBuilder.DropTable(
                name: "SubmitLastDayFlow");

            migrationBuilder.DropTable(
                name: "AuditEFCoreWeakReference");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "KpiInstance");

            migrationBuilder.DropTable(
                name: "KpiScorecard");

            migrationBuilder.DropTable(
                name: "BaseFlow");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "KpiDefinition");

            migrationBuilder.DropTable(
                name: "FlowDiagramDetail");

            migrationBuilder.DropTable(
                name: "FlowDiagram");
        }
    }
}
