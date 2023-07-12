using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using XafDevexpress.Blazor.Server.Services;

namespace XafDevexpress.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => {
            builder.UseApplication<XafDevexpressBlazorApplication>();
            builder.Modules
                .AddAuditTrailEFCore()
                .AddConditionalAppearance()
                .AddDashboards(options =>
                {
                    options.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.EF.DashboardData);
                })
                .AddFileAttachments()
                .AddOffice()
                .AddReports(options =>
                {
                    options.EnableInplaceReports = true;
                    options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
                    options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                    options.ShowAdditionalNavigation = true;
                })
                .AddScheduler()
                .AddValidation(options =>
                {
                    options.IgnoreWarningAndInformationRules = true;
                })
                .AddViewVariants(options =>
                {
                    options.ShowAdditionalNavigation = true;
                })
                .Add<XafDevexpress.Module.XafDevexpressModule>()
                .Add<XafDevexpressBlazorModule>();
			builder.ObjectSpaceProviders
                .AddEFCore(options => options.PreFetchReferenceProperties())
                    .WithAuditedDbContext(contexts => {
                        contexts.Configure<XafDevexpress.Module.BusinessObjects.XafDevexpressEFCoreDbContext, XafDevexpress.Module.BusinessObjects.XafDevexpressAuditingDbContext>(
                            (serviceProvider, businessObjectDbContextOptions) => {
                                // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                                // Do not use this code in production environment to avoid data loss.
                                // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                                //businessObjectDbContextOptions.UseInMemoryDatabase("InMemory");
                                string connectionString = null;
                                if(Configuration.GetConnectionString("ConnectionString") != null) {
                                    connectionString = Configuration.GetConnectionString("ConnectionString");
                                }
#if EASYTEST
                                if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                                    connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                                }
#endif
                                ArgumentNullException.ThrowIfNull(connectionString);
                                businessObjectDbContextOptions.UseSqlServer(connectionString);
                                businessObjectDbContextOptions.UseChangeTrackingProxies();
                                businessObjectDbContextOptions.UseObjectSpaceLinkProxies();
                                businessObjectDbContextOptions.UseLazyLoadingProxies();
                            },
                            (serviceProvider, auditHistoryDbContextOptions) => {
                                string connectionString = null;
                                if(Configuration.GetConnectionString("ConnectionString") != null) {
                                    connectionString = Configuration.GetConnectionString("ConnectionString");
                                }
#if EASYTEST
                                if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                                    connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                                }
#endif
                                ArgumentNullException.ThrowIfNull(connectionString);
                                auditHistoryDbContextOptions.UseSqlServer(connectionString);
                                auditHistoryDbContextOptions.UseChangeTrackingProxies();
                                auditHistoryDbContextOptions.UseObjectSpaceLinkProxies();
                                auditHistoryDbContextOptions.UseLazyLoadingProxies();
                            },
                            auditTrailOptions => {
                                auditTrailOptions.AuditUserProviderType = typeof(DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEmptyUserProvider);
                            });
                    })
                .AddNonPersistent();
		});
	}

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
	}
}
