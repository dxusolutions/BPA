using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace XafDevexpress.Module.BusinessObjects.Flow
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<RecruitFlow> RecruitFlows { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DisplayCaption")]
    [NavigationItem(false)]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class InterviewFlow : BaseFlow
    {
        public InterviewFlow()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        // You can use the regular Code First syntax.
        // Property change notifications will be created automatically.
        // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details)
        public virtual DateTime StartTime { get; set; }

        public virtual Employee Interviewer { get; set; }

        public virtual InterviewStatus InterviewStatus { get; set; } = InterviewStatus.Pending;

        [NotMapped]

        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public virtual string DisplayCaption
        {
            get
            {
                return $"{Name} - {StartTime.ToString()} ({Interviewer?.Name})";
            }
        }
        // Alternatively, specify more UI options:
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public virtual string Name { get; set; }

        // Collection property:
        //public virtual IList<AssociatedEntityObject> AssociatedEntities { get; set; }

        public override bool CanProcessNext()
        {
            return Status == FlowStatus.Done && InterviewStatus != InterviewStatus.Pending;
        }
    }
}