﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XafDevexpress.Module.BusinessObjects;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    [DbContext(typeof(XafDevexpressEFCoreDbContext))]
    [Migration("20230705140930_Update20230705_5")]
    partial class Update20230705_5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Proxies:ChangeTracking", true)
                .HasAnnotation("Proxies:CheckEquality", true)
                .HasAnnotation("Proxies:LazyLoading", false)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Analysis", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ChartSettingsContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DimensionPropertiesString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObjectTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PivotGridSettingsContent")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.DashboardData", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SynchronizeTitle")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("DashboardData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AlarmTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AllDay")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPostponed")
                        .HasColumnType("bit");

                    b.Property<int>("Label")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecurrenceInfoXml")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid?>("RecurrencePatternID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RemindInSeconds")
                        .HasColumnType("int");

                    b.Property<string>("ReminderInfoXml")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("StartOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RecurrencePatternID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.FileData", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Compare")
                        .HasColumnType("bit");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<bool>("EnableCustomizeRepresentation")
                        .HasColumnType("bit");

                    b.Property<string>("Expression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GreenZone")
                        .HasColumnType("real");

                    b.Property<int>("MeasurementFrequency")
                        .HasColumnType("int");

                    b.Property<int>("MeasurementMode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RangeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RangeToCompareName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("RedZone")
                        .HasColumnType("real");

                    b.Property<string>("SuppressedSeries")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetObjectTypeFullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("KpiDefinition");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiHistoryItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KpiInstanceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RangeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RangeStart")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("KpiInstanceID");

                    b.ToTable("KpiHistoryItem");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ForceMeasurementDateTime2")
                        .HasColumnType("datetime2")
                        .HasColumnName("ForceMeasurementDateTime");

                    b.Property<Guid>("KpiDefinitionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Settings")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("KpiDefinitionID");

                    b.ToTable("KpiInstance");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("KpiScorecard");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ReportDataV2", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DataTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInplaceReport")
                        .HasColumnType("bit");

                    b.Property<string>("ParametersObjectTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PredefinedReportTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ReportDataV2");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Resource", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Color_Int")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditDataItemPersistent", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuditedObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NewObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OldObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("AuditedObjectID");

                    b.HasIndex("NewObjectID");

                    b.HasIndex("OldObjectID");

                    b.HasIndex("UserObjectID");

                    b.ToTable("AuditData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefaultString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Key", "TypeName");

                    b.ToTable("AuditEFCoreWeakReference");
                });

            modelBuilder.Entity("EventResource", b =>
                {
                    b.Property<Guid>("EventsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourcesKey")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventsID", "ResourcesKey");

                    b.HasIndex("ResourcesKey");

                    b.ToTable("EventResource");
                });

            modelBuilder.Entity("KpiInstanceKpiScorecard", b =>
                {
                    b.Property<Guid>("IndicatorsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScorecardsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IndicatorsID", "ScorecardsID");

                    b.HasIndex("ScorecardsID");

                    b.ToTable("KpiInstanceKpiScorecard");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.BaseFlow", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeginFlow")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FlowDiagramDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NextFlow")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PrevFlow")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FlowDiagramDetailID");

                    b.ToTable("BaseFlow");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.BaseFlowField", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BaseFlowID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tyle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TyleFullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BaseFlowID");

                    b.ToTable("BaseFlowField");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.Employee", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirhDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowDiagram", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NextFlowDiagramID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("NextFlowDiagramID");

                    b.ToTable("FlowDiagram");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowDiagramDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FlowDiagramID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FlowStepID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FlowDiagramID");

                    b.HasIndex("FlowStepID");

                    b.ToTable("FlowDiagramDetail");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowField", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FlowStepID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Input")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Output")
                        .HasColumnType("bit");

                    b.Property<string>("Tyle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TyleFullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FlowStepID");

                    b.ToTable("FlowField");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowStep", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FlowStep");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Event", "RecurrencePattern")
                        .WithMany("RecurrenceEvents")
                        .HasForeignKey("RecurrencePatternID");

                    b.Navigation("RecurrencePattern");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiHistoryItem", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", "KpiInstance")
                        .WithMany("HistoryItems")
                        .HasForeignKey("KpiInstanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KpiInstance");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", "KpiDefinition")
                        .WithMany("KpiInstances")
                        .HasForeignKey("KpiDefinitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KpiDefinition");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditDataItemPersistent", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "AuditedObject")
                        .WithMany("AuditItems")
                        .HasForeignKey("AuditedObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "NewObject")
                        .WithMany("NewItems")
                        .HasForeignKey("NewObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "OldObject")
                        .WithMany("OldItems")
                        .HasForeignKey("OldObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "UserObject")
                        .WithMany("UserItems")
                        .HasForeignKey("UserObjectID");

                    b.Navigation("AuditedObject");

                    b.Navigation("NewObject");

                    b.Navigation("OldObject");

                    b.Navigation("UserObject");
                });

            modelBuilder.Entity("EventResource", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourcesKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KpiInstanceKpiScorecard", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", null)
                        .WithMany()
                        .HasForeignKey("IndicatorsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard", null)
                        .WithMany()
                        .HasForeignKey("ScorecardsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.BaseFlow", b =>
                {
                    b.HasOne("XafDevexpress.Module.BusinessObjects.FlowDiagramDetail", "FlowDiagramDetail")
                        .WithMany()
                        .HasForeignKey("FlowDiagramDetailID");

                    b.Navigation("FlowDiagramDetail");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.BaseFlowField", b =>
                {
                    b.HasOne("XafDevexpress.Module.BusinessObjects.BaseFlow", "BaseFlow")
                        .WithMany("AllFields")
                        .HasForeignKey("BaseFlowID");

                    b.Navigation("BaseFlow");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowDiagram", b =>
                {
                    b.HasOne("XafDevexpress.Module.BusinessObjects.FlowDiagram", "NextFlowDiagram")
                        .WithMany()
                        .HasForeignKey("NextFlowDiagramID");

                    b.Navigation("NextFlowDiagram");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowDiagramDetail", b =>
                {
                    b.HasOne("XafDevexpress.Module.BusinessObjects.FlowDiagram", "FlowDiagram")
                        .WithMany("FlowDiagramDetails")
                        .HasForeignKey("FlowDiagramID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XafDevexpress.Module.BusinessObjects.FlowStep", "FlowStep")
                        .WithMany()
                        .HasForeignKey("FlowStepID");

                    b.Navigation("FlowDiagram");

                    b.Navigation("FlowStep");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowField", b =>
                {
                    b.HasOne("XafDevexpress.Module.BusinessObjects.FlowStep", "FlowStep")
                        .WithMany("AllFields")
                        .HasForeignKey("FlowStepID");

                    b.Navigation("FlowStep");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.Navigation("RecurrenceEvents");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", b =>
                {
                    b.Navigation("KpiInstances");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.Navigation("HistoryItems");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", b =>
                {
                    b.Navigation("AuditItems");

                    b.Navigation("NewItems");

                    b.Navigation("OldItems");

                    b.Navigation("UserItems");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.BaseFlow", b =>
                {
                    b.Navigation("AllFields");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowDiagram", b =>
                {
                    b.Navigation("FlowDiagramDetails");
                });

            modelBuilder.Entity("XafDevexpress.Module.BusinessObjects.FlowStep", b =>
                {
                    b.Navigation("AllFields");
                });
#pragma warning restore 612, 618
        }
    }
}
