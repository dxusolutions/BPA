using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Module.Controllers
{
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
	public partial class CurrentFlowProcessController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CurrentFlowProcessController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(BaseFlow);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            var processCurObj = Frame.GetController<ListViewProcessCurrentObjectController>();
            processCurObj.CustomProcessSelectedItem -= ProcessCurObj_CustomProcessSelectedItem;
            processCurObj.CustomProcessSelectedItem += ProcessCurObj_CustomProcessSelectedItem;
        }

        private void ProcessCurObj_CustomProcessSelectedItem(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            if (View.CurrentObject is BaseFlow baseFlow)
            {
                var objectSpace = Application.CreateObjectSpace(typeof(BaseFlow));
                var detailView = Application.CreateDetailView(objectSpace, objectSpace.GetObjectByKey<BaseFlow>(baseFlow.ID));
                detailView.ViewEditMode = ViewEditMode.Edit;
                Application.MainWindow.SetView(detailView);
                e.Handled = true;
            }
        }
    }
}
