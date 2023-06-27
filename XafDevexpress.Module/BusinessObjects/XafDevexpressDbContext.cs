using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using XafDevexpress.Module.BusinessObjects.Flow;

namespace XafDevexpress.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class XafDevexpressContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<XafDevexpressEFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new XafDevexpressEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class XafDevexpressDesignTimeDbContextFactory : IDesignTimeDbContextFactory<XafDevexpressEFCoreDbContext> {
	public XafDevexpressEFCoreDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<XafDevexpressEFCoreDbContext>();
        optionsBuilder.UseSqlServer("Integrated Security=SSPI;Pooling=true;MultipleActiveResultSets=true;Data Source=localhost;Initial Catalog=XafDevexpress;TrustServerCertificate=True;TrustServerCertificate=True");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        optionsBuilder.UseAudit();
        return new XafDevexpressEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(XafDevexpressContextInitializer))]
public class XafDevexpressEFCoreDbContext : DbContext
{
	public XafDevexpressEFCoreDbContext(DbContextOptions<XafDevexpressEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<FileData> FileData { get; set; }
	public DbSet<ReportDataV2> ReportDataV2 { get; set; }
	public DbSet<KpiDefinition> KpiDefinition { get; set; }
	public DbSet<KpiInstance> KpiInstance { get; set; }
	public DbSet<KpiHistoryItem> KpiHistoryItem { get; set; }
	public DbSet<KpiScorecard> KpiScorecard { get; set; }
	public DbSet<DashboardData> DashboardData { get; set; }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Analysis> Analysis { get; set; }

    public DbSet<Employee> Employee { get; set; }

    #region Flow
    public DbSet<FlowDiagram> FlowDiagram { get; set; }
    public DbSet<FlowDiagramDetail> FlowDiagramDetail { get; set; }
    public DbSet<BaseFlow> BaseFlow { get; set; }
    public DbSet<BackOfficeFlow> BackOfficeFlow { get; set; }
    public DbSet<InterviewFlow> InterviewFlow { get; set; }
    public DbSet<ScanCvFlow> ScanCvFlow { get; set; }
    public DbSet<RequestLastDayFlow> RequestLastDayFlow { get; set; }
    public DbSet<ConfirmLastDayFlow> ConfirmLastDayFlow { get; set; }
    public DbSet<SubmitLastDayFlow> SubmitLastDayFlow { get; set; }
    #endregion Flow

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);

        modelBuilder.Entity<FlowDiagram>()
            .HasMany(r => r.FlowDiagramDetails)
            .WithOne(x => x.FlowDiagram)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BackOfficeFlow>().ToTable("BackOfficeFlow");
        modelBuilder.Entity<InterviewFlow>().ToTable("InterviewFlow");
        modelBuilder.Entity<ScanCvFlow>().ToTable("ScanCvFlow");
        modelBuilder.Entity<RequestLastDayFlow>().ToTable("RequestLastDayFlow");
        modelBuilder.Entity<SubmitLastDayFlow>().ToTable("SubmitLastDayFlow");
        modelBuilder.Entity<ConfirmLastDayFlow>().ToTable("ConfirmLastDayFlow");
    }
}

public class XafDevexpressAuditingDbContext : DbContext {
    public XafDevexpressAuditingDbContext(DbContextOptions<XafDevexpressAuditingDbContext> options) : base(options) {
    }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);
    }
}
