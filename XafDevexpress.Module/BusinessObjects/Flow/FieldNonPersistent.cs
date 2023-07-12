using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace XafDevexpress.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NotMapped]
    [NavigationItem(false)]
    [DisplayProperty(nameof(Name))]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class FieldNonPersistent : NonPersistentBaseObject
    {
        public virtual string Name { get; set; }

        public virtual string TypeString { get; set; }
    }
}