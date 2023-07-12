using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace XafDevexpress.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();

		//PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == SecurityStrategy.AdministratorRoleName);
		//if (adminRole == null)
		//{
		//	adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
		//	adminRole.Name = SecurityStrategy.AdministratorRoleName;
		//	adminRole.IsAdministrative = true;
		//}
		//PermissionPolicyUser adminUser = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(user => user.UserName == "Admin");
		//if (adminUser == null)
		//{
		//	adminUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
		//	adminUser.UserName = "Admin";
		//	adminUser.SetPassword("123456");
		//	adminUser.Roles.Add(adminRole);
		//}
		//ObjectSpace.CommitChanges();
	}
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
