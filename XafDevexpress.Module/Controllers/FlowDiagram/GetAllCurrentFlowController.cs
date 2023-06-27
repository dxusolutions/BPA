using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GetAllCurrentFlowController : ViewController
    {
        SimpleAction simpleAction;

        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public GetAllCurrentFlowController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(FlowDiagram);

            simpleAction = new SimpleAction(this, "GetAllCurrentFlowController", PredefinedCategory.Export);
            simpleAction.ImageName = "Action_New";
            simpleAction.Caption = "Process";
            simpleAction.ToolTip = "Start new process";
            simpleAction.Execute -= Action_Execute;
            simpleAction.Execute += Action_Execute;
        }

        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (this.View.ObjectSpace.IsModified)
            {
                Application.ShowViewStrategy.ShowMessage("Please save first!");
            }
            else if (e.SelectedObjects[0] is FlowDiagram flowDiagram)
            {
                var first = flowDiagram.FlowDiagramDetails.OrderBy(x => x.Sort).FirstOrDefault();
                var nextFlow = Type.GetType($"XafDevexpress.Module.BusinessObjects.Flow.{first.Name}");
                IObjectSpace objectSpace = Application.CreateObjectSpace(nextFlow);
                BaseFlow newObj = objectSpace.CreateObject(nextFlow) as BaseFlow;
                newObj.FlowDiagramDetail = first;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (this.View.CurrentObject is FlowDiagram flowDiagram)
            {
                Task.Run(() => flowDiagram.GetAllCurrentFlow());
            }
        }
    }
}
