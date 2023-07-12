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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class FlowStep : BaseObject
    {
        public static IList<FieldNonPersistent> _GetAllType;

        public FlowStep()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        public virtual string Name { get; set; }

        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<FlowField> AllFields { get; set; } = new ObservableCollection<FlowField>();

        [Browsable(false)]
        public IList<FieldNonPersistent> GetAllType
        {
            get
            {
                if (_GetAllType.IsNullOrEmpty())
                {
                    _GetAllType = new List<FieldNonPersistent>();
                    _GetAllType.Add(new FieldNonPersistent() { Name = typeof(string).Name, TypeString = typeof(string).FullName });
                    _GetAllType.Add(new FieldNonPersistent() { Name = typeof(int).Name, TypeString = typeof(int).FullName });
                    _GetAllType.Add(new FieldNonPersistent() { Name = typeof(double).Name, TypeString = typeof(double).FullName });
                    _GetAllType.Add(new FieldNonPersistent() { Name = typeof(DateTime).Name, TypeString = typeof(DateTime).FullName });
                    _GetAllType.Add(new FieldNonPersistent() { Name = typeof(bool).Name, TypeString = typeof(bool).FullName });
                }
                return _GetAllType;
            }
        } 

        public override void OnLoaded()
        {
            base.OnLoaded();
        }

        public override void OnSaving()
        {
            base.OnSaving();
        }
    }
}