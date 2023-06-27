using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GoToNexFlowController : ViewController
    {
        SimpleAction simpleAction;
        SimpleAction nextDiagram;
        FlowDiagramDetail nextFlowDetail;

        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public GoToNexFlowController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(BaseFlow);

            simpleAction = new SimpleAction(this, "GoToNexFlowController", PredefinedCategory.Export);
            simpleAction.ImageName = "Action_Change_State";
            simpleAction.ToolTip = "Go to next step";
            simpleAction.Execute -= Action_Execute;
            simpleAction.Execute += Action_Execute;

            nextDiagram = new SimpleAction(this, "GoToNexDiagramController", PredefinedCategory.Export);
            nextDiagram.ImageName = "Action_New";
            nextDiagram.Caption = "Process";
            nextDiagram.Execute -= NextDiagram_Execute;
            nextDiagram.Execute += NextDiagram_Execute;
        }

        private void NextDiagram_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (this.View.ObjectSpace.IsModified)
            {
                Application.ShowViewStrategy.ShowMessage("Please save first!");
            }
            else if (this.View.CurrentObject is BaseFlow baseFlow)
            {
                var flowDiagram = baseFlow.FlowDiagramDetail.FlowDiagram.NextFlowDiagram;
                var first = flowDiagram.FlowDiagramDetails.OrderBy(x => x.Sort).FirstOrDefault();
                var nextFlow = Type.GetType($"XafDevexpress.Module.BusinessObjects.Flow.{first.Name}");
                IObjectSpace objectSpace = Application.CreateObjectSpace(nextFlow);
                BaseFlow newObj = objectSpace.CreateObject(nextFlow) as BaseFlow;
                newObj.FlowDiagramDetail = first;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
            }
        }

        private async void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (sender is IObjectSpace objSpace && objSpace.IsModified)
            {
                await ShowNextAction();
            }
        }

        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (this.View.ObjectSpace.IsModified)
            {
                Application.ShowViewStrategy.ShowMessage("Please save first!");
            }
            else if (e.CurrentObject is BaseFlow baseFlow)
            {
                var temp = this.View.ObjectTypeInfo.ToString().Split('.');
                var nextFlow = Type.GetType($"{string.Join(".", temp.SkipLast(1))}.{nextFlowDetail.Name}");

                IObjectSpace objectSpace = Application.CreateObjectSpace(nextFlow);
                BaseFlow newObj = objectSpace.CreateObject(nextFlow) as BaseFlow;
                newObj.PrevFlow = baseFlow.ID;
                newObj.BeginFlow = baseFlow.BeginFlow;
                newObj.Name = baseFlow.Name;
                newObj.FlowDiagramDetail = nextFlowDetail;

                var obj = objectSpace.GetObjectByKey(typeof(BaseFlow), baseFlow.ID) as BaseFlow;
                if (obj != null && obj.NextFlow != newObj.ID)
                {
                    obj.NextFlow = newObj.ID;
                }

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
            }
        }

        private async Task ShowNextAction()
        {
            bool active = false;
            bool activeNextDiagram = false;
            if (this.View.CurrentObject is BaseFlow baseFlow && baseFlow.CanProcessNext())
            {
                if (baseFlow.NextFlow == Guid.Empty)
                {
                    var dbContext = ((this.ObjectSpace as DevExpress.ExpressApp.EFCore.EFCoreObjectSpace).DbContext as XafDevexpressEFCoreDbContext);
                    nextFlowDetail = (await dbContext.FlowDiagramDetail
                                                .Where(x => x.FlowDiagram == baseFlow.FlowDiagramDetail.FlowDiagram && x.Sort > baseFlow.FlowDiagramDetail.Sort)
                                                .OrderBy(x => x.Sort)
                                                .FirstOrDefaultAsync());
                    if (nextFlowDetail != null)
                    {
                        var nextFlowName = nextFlowDetail.Name;
                        if (!string.IsNullOrWhiteSpace(nextFlowName))
                        {
                            simpleAction.Caption = nextFlowName;
                            active = true;
                        }
                    }
                    else
                    {
                        //Check next diagram
                        if (baseFlow.FlowDiagramDetail.FlowDiagram.NextFlowDiagram != null)
                        {
                            activeNextDiagram = true;
                            nextDiagram.ToolTip = $"Start process [{baseFlow.FlowDiagramDetail.FlowDiagram.NextFlowDiagram.Name}]";
                        }
                    }
                }
            }
            simpleAction.Active[""] = active;
            nextDiagram.Active[""] = activeNextDiagram;
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            this.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            this.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;

            Task.Run(ShowNextAction);
        }
    }
}
