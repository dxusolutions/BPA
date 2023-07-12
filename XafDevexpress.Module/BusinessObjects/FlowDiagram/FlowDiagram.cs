using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace XafDevexpress.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<FlowDiagram> FlowDiagrams { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class FlowDiagram : BaseObject
    {
        public FlowDiagram()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }

        [Browsable(false)]
        public virtual string DiagramSerialize { get; set; }

        // Alternatively, specify more UI options:
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public virtual string Name { get; set; }

        // Collection property:
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<FlowDiagramDetail> FlowDiagramDetails { get; set; } = new ObservableCollection<FlowDiagramDetail>();

        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<FlowDiagramLink> FlowDiagramLinks { get; set; } = new ObservableCollection<FlowDiagramLink>();

        [NotMapped]
        [VisibleInListView(false)]
        [Editable(false)]
        public virtual IList<BaseFlow> BaseFlows { get; set; } = new ObservableCollection<BaseFlow>();

        [VisibleInListView(false)]
        public virtual FlowDiagram NextFlowDiagram { get; set; }

        public virtual FlowStatus Status { get; set; } = FlowStatus.Inprocess;
    }
}