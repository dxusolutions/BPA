using DevExpress.Charts.Native;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Identity.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XafDevexpress.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<BaseFlow> BaseFlows { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class BaseFlow : BaseObject
    {
        public BaseFlow()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        // You can use the regular Code First syntax.
        // Property change notifications will be created automatically.
        // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details)
        public virtual string Name { get; set; }

        [VisibleInListView(false)]
        public virtual string Note { get; set; }

        [Browsable(false)]
        public virtual Guid PrevFlow { get; set; }

        [Browsable(false)]
        public virtual Guid NextFlow { get; set; }

        [Browsable(false)]
        public virtual Guid BeginFlow { get; set; }

        public virtual FlowStatus Status { get; set; } = FlowStatus.Inprocess;

        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDashboards(false)]
        public virtual FlowDiagramDetail FlowDiagramDetail { get; set; }

        public override void OnSaving()
        {
            if (PrevFlow == Guid.Empty)
                BeginFlow = this.ID;

            base.OnSaving();
        }

        public virtual bool CanProcessNext()
        {
            return Status == FlowStatus.Done;
        }
    }
}