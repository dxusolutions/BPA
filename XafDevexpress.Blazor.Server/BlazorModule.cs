using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.BaseImpl.EF;
using static System.Net.Mime.MediaTypeNames;

namespace XafDevexpress.Blazor.Server;

[ToolboxItemFilter("Xaf.Platform.Blazor")]
// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XafDevexpressBlazorModule : ModuleBase {
    public XafDevexpressBlazorModule() {
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        return ModuleUpdater.EmptyModuleUpdaters;
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);

		application.LoggedOn += Application_LoggedOn;
	}

	private void Application_LoggedOn(object sender, LogonEventArgs e)
	{
		if (SecuritySystem.CurrentUserName != null)
		{
			//ServiceProvider.GetService<IJSRuntime>().InvokeVoidAsync("RedirectTo", "/EmployeeList");
			((XafApplication)sender).ServiceProvider.GetService<Microsoft.AspNetCore.Components.NavigationManager>().NavigateTo("/FlowProcessPage");
		}
	}
}
