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
        public static IList<FlowNonPersistent> _getAllFlow;

        public FlowDiagram()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }

        // Alternatively, specify more UI options:
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public virtual string Name { get; set; }

        // Collection property:
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<FlowDiagramDetail> FlowDiagramDetails { get; set; } = new ObservableCollection<FlowDiagramDetail>();

        [NotMapped]
        [VisibleInListView(false)]
        [Editable(false)]
        public virtual IList<BaseFlow> BaseFlows { get; set; } = new ObservableCollection<BaseFlow>();

        [VisibleInListView(false)]
        public virtual FlowDiagram NextFlowDiagram { get; set; }

        [Browsable(false)]
        public IList<FlowNonPersistent> GetAllFlow
        {
            get
            {
                if (_getAllFlow.IsNullOrEmpty())
                {
                    _getAllFlow = new ObservableCollection<FlowNonPersistent>();
                    foreach (var item in Assembly.GetExecutingAssembly().GetTypes()
                                                            .Where(t => String.Equals(t.Namespace,
                                                                                    "XafDevexpress.Module.BusinessObjects.Flow",
                                                                                    StringComparison.Ordinal))
                                                            .Select(x => x.Name))
                    {
                        _getAllFlow.Add(new FlowNonPersistent { Name = item });
                    }
                }
                return _getAllFlow;
            }
        }

        public void GetAllCurrentFlow()
        {
            if (BaseFlows.Count == 0)
            {
                string empty = Guid.Empty.ToString();

                var r = this.ObjectSpace.GetObjects<BaseFlow>(CriteriaOperator.FromLambda<BaseFlow>(x => x.NextFlow.ToString() == empty))
                                        .Where(x => x.FlowDiagramDetail.FlowDiagram.ID == this.ID);
                if (r.Count() > 0)
                {
                    BaseFlows = new ObservableCollection<BaseFlow>(r);
                }
            }
        }
    }
}