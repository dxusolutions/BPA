using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.XtraRichEdit.Fields;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using XafDevexpress.Blazor.Server.Editors;
using XafDevexpress.Module.BusinessObjects;
using static XafDevexpress.Blazor.Server.Editors.FlowProcessViewItemBlazor;

namespace XafDevexpress.Blazor.Server.Controllers
{
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
	public partial class FlowProcessViewController : ObjectViewController<DetailView, BaseFlow>
	{
		SimpleAction simpleAction;
		SimpleAction saveAction;
		SimpleAction nextDiagram;
		Guid nextFlowDetail;

		// Use CodeRush to create Controllers and Actions with a few keystrokes.
		// https://docs.devexpress.com/CodeRushForRoslyn/403133/
		public FlowProcessViewController()
		{
			InitializeComponent();
			// Target required Views (via the TargetXXX properties) and create their Actions.

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

			saveAction = new SimpleAction(this, "SaveFlowController", PredefinedCategory.Save);
			saveAction.ImageName = "Action_Save";
			saveAction.ToolTip = "Save";
			saveAction.Execute -= SaveAction_Execute;
			saveAction.Execute += SaveAction_Execute;
		}

		protected override void OnActivated()
		{
			base.OnActivated();
			// Perform various tasks depending on the target View.

			var item = View.FindItem("FlowProcessPage");
			item.ControlCreated -= FlowProcessViewController_ControlCreated;
			item.ControlCreated += FlowProcessViewController_ControlCreated;
		}

		protected override void OnViewControlsCreated()
		{
			base.OnViewControlsCreated();

			var processCurObj = Frame.GetController<ModificationsController>();
			processCurObj.SaveAction.Active[""] = false;
			processCurObj.SaveAndCloseAction.Active[""] = false;
			processCurObj.SaveAndNewAction.Active[""] = false;

			Task.Run(ShowNextAction);
		}

		private void FlowProcessViewController_ControlCreated(object sender, EventArgs e)
		{
			if (this.View.CurrentObject is BaseFlow baseFlow)
			{
				var componentModel = ((FlowProcessHolder)((FlowProcessViewItemBlazor)sender).Control).ComponentModel;
				componentModel.ObjectSpace = this.ObjectSpace;
				if (baseFlow != null && (baseFlow.NextFlow != Guid.Empty || baseFlow.AllFields.Count > 0))
				{
					foreach (var item in baseFlow.AllFields)
					{
                        componentModel.GetFields.Add(new BaseFlowField()
                        {
                            Value = item.Value,
                            ID = item.ID,
                            BaseFlow = baseFlow,
                            Name = item.Name,
                            Type = item.Type,
                            TypeFullName = item.TypeFullName,
                        });
                    }
                }
                else
				{
                    var objectSpaceTemp = Application.CreateObjectSpace(typeof(FlowDiagramDetail));
                    var flowDiagramDetail = objectSpaceTemp.GetObjectByKey<FlowDiagramDetail>(baseFlow.FlowDiagramDetail.ID);
                    foreach (var item in flowDiagramDetail.FlowStep.AllFields)
                    {
						var newField = new BaseFlowField()
						{
							BaseFlow = baseFlow,
							Name = item.Name,
							Type = item.Type,
							TypeFullName = item.TypeFullName,
						};
						componentModel.GetFields.Add(newField);
                    }
                }
            }
		}

		private async void SaveAction_Execute(object sender, SimpleActionExecuteEventArgs e)
		{
			if (this.View.CurrentObject is BaseFlow baseFlow)
			{
                var prevFlow = this.ObjectSpace.GetObjectByKey<BaseFlow>(baseFlow.PrevFlow) as BaseFlow;
				if (prevFlow != null)
				{
					prevFlow.NextFlow = baseFlow.ID;
				}

				var item1 = View.FindItem("FlowProcessPage");
				var componentModel = ((FlowProcessHolder)item1.Control).ComponentModel;
				if (componentModel != null)
				{
					foreach (var item in componentModel.GetFields)
					{
						var field = baseFlow.AllFields.FirstOrDefault(x => x.Name == item.Name
																		&& x.TypeFullName == item.TypeFullName
																		&& x.Type == item.Type);
						if (field == null)
						{
							field = this.ObjectSpace.CreateObject<BaseFlowField>();
							field.Type = item.Type;
							field.TypeFullName = item.TypeFullName;
							field.Name = item.Name;
							field.BaseFlow = baseFlow;
                            baseFlow.AllFields.Add(field);
                        }						
						field.Value = item.Value;
					}
				}

                this.ObjectSpace.CommitChanges();
			}

			await ShowNextAction();
		}

		private void NextDiagram_Execute(object sender, SimpleActionExecuteEventArgs e)
		{
			if (this.View.CurrentObject is BaseFlow baseFlow)
			{
				var objectSpace = Application.CreateObjectSpace(typeof(BaseFlow));
				var flowDiagram = objectSpace.GetObjectByKey<FlowDiagram>(baseFlow.FlowDiagramDetail.FlowDiagram.ID).NextFlowDiagram;
				var first = flowDiagram.FlowDiagramDetails.Where(x => flowDiagram.FlowDiagramLinks.Any(y => y.Target.ID.ToString() == x.ID.ToString()) == false).FirstOrDefault();
				BaseFlow newObj = objectSpace.CreateObject(typeof(BaseFlow)) as BaseFlow;
				newObj.FlowDiagramDetail = first;
				newObj.Name = first.Name;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
			}
		}

		private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
		{
			if (this.View.CurrentObject is BaseFlow baseFlow)
			{
				var objectSpace = Application.CreateObjectSpace(typeof(BaseFlow));
				var next = objectSpace.GetObjectByKey<FlowDiagramDetail>(nextFlowDetail);
				BaseFlow newObj = objectSpace.CreateObject(typeof(BaseFlow)) as BaseFlow;
				newObj.PrevFlow = baseFlow.ID;
				newObj.BeginFlow = baseFlow.BeginFlow;
				newObj.Name = next.Name;
				newObj.FlowDiagramDetail = next;

				if (baseFlow != null && baseFlow.NextFlow != newObj.ID)
				{
					baseFlow.NextFlow = newObj.ID;
				}

				foreach (var item in baseFlow.AllFields)
				{
					if (newObj.AllFields.Any(x => x.TypeFullName == item.TypeFullName
													&& x.Name == item.Name) == false)
					{
						var newField = objectSpace.CreateObject(typeof(BaseFlowField)) as BaseFlowField;
						newField.TypeFullName = item.TypeFullName;
						newField.Type = item.Type;
						newField.Name = item.Name;
						newField.Value = item.Value;

						newObj.AllFields.Add(newField);
					}
				}

				var flowDiagramDetail = objectSpace.GetObjectByKey<FlowDiagramDetail>(next.ID);
				foreach (var item in flowDiagramDetail.FlowStep.AllFields)
				{
					if (newObj.AllFields.Any(x => x.TypeFullName == item.TypeFullName
													&& x.Name == item.Name) == false)
					{
						var newField = objectSpace.CreateObject(typeof(BaseFlowField)) as BaseFlowField;
						newField.TypeFullName = item.TypeFullName;
						newField.Type = item.Type;
						newField.Name = item.Name;

						newObj.AllFields.Add(newField);
					}
				}

				e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newObj);
			}
		}

		private async Task ShowNextAction()
		{
			bool active = false;
			bool activeNextDiagram = false;
			if (this.View.CurrentObject is BaseFlow baseFlow && baseFlow != null && baseFlow.CanProcessNext())
			{
				if (baseFlow.NextFlow == Guid.Empty)
				{
					var objectSpace = Application.CreateObjectSpace(typeof(BaseFlow));
					var dbContext = ((objectSpace as DevExpress.ExpressApp.EFCore.EFCoreObjectSpace).DbContext as XafDevexpressEFCoreDbContext);
					var flowDiagramDetail = await dbContext.FlowDiagramDetail.FirstOrDefaultAsync(x => x.ID == baseFlow.FlowDiagramDetail.ID);

					var next = flowDiagramDetail.FlowDiagram.FlowDiagramLinks
												.Where(x => x.Source.ID.ToString() == flowDiagramDetail.ID.ToString()
														&& x.Status.ToString() == baseFlow.Status.ToString())
												.FirstOrDefault()?.Target;
					if (next != null)
                    {
                        nextFlowDetail = next.ID;
                        var nextFlowName = next.Name;
						if (!string.IsNullOrWhiteSpace(nextFlowName))
						{
							simpleAction.Caption = nextFlowName;
							active = true;
						}
					}
					else
					{
						//Check next diagram
						if (flowDiagramDetail.FlowDiagram.NextFlowDiagram != null)
						{
							activeNextDiagram = true;
							nextDiagram.ToolTip = $"Start process [{flowDiagramDetail.FlowDiagram.NextFlowDiagram.Name}]";
						}
					}
				}
			}
			simpleAction.Active[""] = active;
			nextDiagram.Active[""] = activeNextDiagram;
		}

		protected override void OnDeactivated()
		{
			// Unsubscribe from previously subscribed events and release other references and resources.
			base.OnDeactivated();

			View.FindItem("FlowProcessPage").ControlCreated -= FlowProcessViewController_ControlCreated;

			var processCurObj = Frame.GetController<ModificationsController>();
			processCurObj.SaveAction.Active[""] = true;
			processCurObj.SaveAndCloseAction.Active[""] = true;
			processCurObj.SaveAndNewAction.Active[""] = true;
		}
	}
}
