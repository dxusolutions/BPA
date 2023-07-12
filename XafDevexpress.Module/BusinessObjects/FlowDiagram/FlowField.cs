using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace XafDevexpress.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<FlowDiagram> FlowDiagrams { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class FlowField : BaseObject
    {
        public FlowField()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        [Browsable(false)]
        public virtual FlowStep FlowStep { get; set; }

        public virtual string Name { get; set; }

        [DataSourceProperty("FlowStep.GetAllType")]
        [NotMapped]
        public virtual FieldNonPersistent TypeDropDown { get; set; }

        [Browsable(false)]
        public virtual string Type { get; set; }
        
        [Browsable(false)]
        public virtual string TypeFullName { get; set; }

        public virtual bool Input { get; set; }

        public virtual bool Output { get; set; }

        // Alternatively, specify more UI options:
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public virtual string Name { get; set; }

        // Collection property:
        //public virtual IList<AssociatedEntityObject> AssociatedEntities { get; set; }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        public override void OnLoaded()
        {
            TypeDropDown = FlowStep.GetAllType.FirstOrDefault(x => x.TypeString == TypeFullName);

            base.OnLoaded();
        }

        public override void OnSaving()
        {
            Type = TypeDropDown.Name;
            TypeFullName = TypeDropDown.TypeString;
            base.OnSaving();
        }
    }
}