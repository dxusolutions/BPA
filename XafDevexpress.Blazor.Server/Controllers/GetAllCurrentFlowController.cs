using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Blazor.Server.Controllers
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
                foreach (var detail in flowDiagram.FlowDiagramDetails)
                {
                    if (flowDiagram.FlowDiagramLinks.Any(x => x.Target.ID == detail.ID) == false)
                    {
                        IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(BaseFlow));
                        BaseFlow newObj = objectSpace.CreateObject(typeof(BaseFlow)) as BaseFlow;
                        newObj.FlowDiagramDetail = detail;
                        newObj.Name = detail.Name;

                        e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
                        break;
                    }
                }
            }
		}
    }
}
