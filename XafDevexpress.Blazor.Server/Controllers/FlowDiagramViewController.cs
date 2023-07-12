using Blazor.Diagrams.Core.Models;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using DevExpress.XtraReports.Design.ParameterEditor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text.Json;
using XafDevexpress.Blazor.Server.BusinessClass;
using XafDevexpress.Blazor.Server.Editors;
using XafDevexpress.Blazor.Server.Model;
using XafDevexpress.Module.BusinessObjects;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static System.Net.Mime.MediaTypeNames;
using static XafDevexpress.Blazor.Server.Editors.FlowDiagramViewItemBlazor;
using static XafDevexpress.Blazor.Server.Editors.FlowProcessViewItemBlazor;

namespace XafDevexpress.Blazor.Server.Controllers
{
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
	public partial class FlowDiagramViewController : ObjectViewController<DetailView, FlowDiagram>
	{
        FlowDiagramModel flowDiagramModel;

		// Use CodeRush to create Controllers and Actions with a few keystrokes.
		// https://docs.devexpress.com/CodeRushForRoslyn/403133/
		public FlowDiagramViewController()
		{
			InitializeComponent();
			// Target required Views (via the TargetXXX properties) and create their Actions.
		}

		protected override void OnActivated()
		{
			base.OnActivated();
			// Perform various tasks depending on the target View.

			var item = View.FindItem("FlowDiagramPage");
			item.ControlCreated -= FlowDiagramViewController_ControlCreated;
			item.ControlCreated += FlowDiagramViewController_ControlCreated;
		}

		protected override void OnViewControlsCreated()
		{
			base.OnViewControlsCreated();
            View.ObjectSpace.Committing += ObjectSpace_Committing;
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var flowDiagram = View.CurrentObject as FlowDiagram;

            foreach (var detail in flowDiagramModel.Diagram.Nodes)
            {
                var item = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.Id.ToString());
                if (item.IsDeleted)
                {
                    ObjectSpace.Delete(item);
                    continue;
                }
				item.X = detail.Position.X;
				item.Y = detail.Position.Y;
			}

            foreach (var detail in flowDiagramModel.Diagram.Links)
            {
                var item = flowDiagram.FlowDiagramLinks.FirstOrDefault(x => x.ID.ToString() == detail.Id.ToString());
                if (item == null)
                {
                    item = ObjectSpace.CreateObject(typeof(FlowDiagramLink)) as FlowDiagramLink;
                    flowDiagram.FlowDiagramLinks.Add(item);
                }
                item.Source = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.SourceNode.Id.ToString());
                if (item.Source == null)
                {
                    item.Source = flowDiagram.FlowDiagramDetails
                                    .FirstOrDefault(x => x.FlowStep.Name.ToString() == detail.SourceNode.Title.ToString()
                                                        && x.X == detail.SourceNode.Position.X
                                                        && x.Y == detail.SourceNode.Position.Y);
                }
                item.SourcePortAlignment = detail.SourcePort.Alignment.ToString();
                item.Target = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.TargetNode.Id.ToString());
				if (item.Target == null)
				{
					item.Target = flowDiagram.FlowDiagramDetails
									.FirstOrDefault(x => x.FlowStep.Name.ToString() == detail.TargetNode.Title.ToString()
														&& x.X == detail.TargetNode.Position.X
                                                        && x.Y == detail.TargetNode.Position.Y);
				}
                item.TargetPortAlignment = detail.TargetPort.Alignment.ToString();
                if (Enum.TryParse<FlowStatus>(detail.Labels.FirstOrDefault()?.Content, out FlowStatus status))
                {
                    item.Status = status;
                }
                item.FlowDiagram = flowDiagram;
            }
        }

        private void FlowDiagramViewController_ControlCreated(object sender, EventArgs e)
		{
			if (this.View.CurrentObject is FlowDiagram flowDiagram)
			{
				flowDiagramModel = ((FlowDiagramHolder)((FlowDiagramViewItemBlazor)sender).Control).ComponentModel;
                flowDiagramModel.InitDiagram();
                InitFlow(flowDiagram.ID);
                //
                flowDiagramModel.OpenFlowDetailEvent -= FlowDiagramModel_OpenFlowDetailEvent;
                flowDiagramModel.OpenFlowDetailEvent += FlowDiagramModel_OpenFlowDetailEvent;
                flowDiagramModel.DeleteFlowDetailEvent -= FlowDiagramModel_DeleteFlowDetailEvent;
                flowDiagramModel.DeleteFlowDetailEvent += FlowDiagramModel_DeleteFlowDetailEvent;
                flowDiagramModel.RemoveLinkEvent -= FlowDiagramModel_RemoveLinkEvent;
                flowDiagramModel.RemoveLinkEvent += FlowDiagramModel_RemoveLinkEvent;
                flowDiagramModel.OpenLinkEvent -= FlowDiagramModel_OpenLinkEvent;
                flowDiagramModel.OpenLinkEvent += FlowDiagramModel_OpenLinkEvent;
            }
		}

        private void FlowDiagramModel_OpenLinkEvent(string Id, string content)
        {
            if (View.CurrentObject is FlowDiagram flowDiagram)
            {
                var objectSpace = Application.CreateObjectSpace(typeof(FlowDiagramLink));
                var currentObject = objectSpace.CreateObject(typeof(FlowDiagramLink)) as FlowDiagramLink;
                if (Enum.TryParse<FlowStatus>(content, out FlowStatus status))
                {
                    currentObject.Status = status;
                }
                var detailView = Application.CreateDetailView(objectSpace, currentObject, false);
                detailView.ViewEditMode = ViewEditMode.Edit;
                Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
                {
                    objectSpace.Rollback();

                    var link = flowDiagramModel.Diagram.Links.FirstOrDefault(x => x.Id == Id);
                    if (link != null)
                    {
                        if (link.Labels.Count == 0)
                        {
                            link.Labels = new List<LinkLabelModel>()
                            {
                                new LinkLabelModel(link, currentObject.Status.ToString())
                            };
                        }
                        else
                        {
                            link.Labels.FirstOrDefault().Content = currentObject.Status.ToString();
                        }
                    }
                });
            }
        }

        private void FlowDiagramModel_RemoveLinkEvent(global::Blazor.Diagrams.Core.Models.Base.BaseLinkModel link)
        {
            var linkObj = this.ObjectSpace.GetObjectByKey<FlowDiagramLink>(new Guid(link.Id));
            if (linkObj != null)
                ObjectSpace.Delete(linkObj);
        }

        private void FlowDiagramModel_DeleteFlowDetailEvent(string Id)
        {
            if (View.CurrentObject is FlowDiagram flowDiagram)
            {
                var item = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == Id.ToString());
                if (item != null)
                    this.ObjectSpace.Delete(item);
            }
        }

        private void FlowDiagramModel_OpenFlowDetailEvent(string Id)
        {
            if (this.View.CurrentObject is FlowDiagram flowDiagram)
            {
                FlowDiagramDetail currentObject;
                var objectSpace = Application.CreateObjectSpace(typeof(FlowDiagramDetail));
                if (string.IsNullOrWhiteSpace(Id))
                {
                    currentObject = objectSpace.CreateObject(typeof(FlowDiagramDetail)) as FlowDiagramDetail;
                    currentObject.FlowDiagram = objectSpace.GetObjectByKey<FlowDiagram>(flowDiagram.ID);
                    var detailView = Application.CreateDetailView(objectSpace, currentObject, false);
                    detailView.ViewEditMode = ViewEditMode.Edit;
                    Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
                    {
                        currentObject.IsNew = true;
                        flowDiagram.FlowDiagramDetails.Add(currentObject);

                        objectSpace.CommitChanges();

                        var node1 = flowDiagramModel.NewNode(currentObject.ID.ToString(), 20, 20);
                        node1.Title = currentObject.Name;
                        flowDiagramModel.Diagram.Nodes.Add(node1);
                    });
                }
                else
                {
                    currentObject = objectSpace.GetObjectByKey<FlowDiagramDetail>(new Guid(Id));
                    var detailView = Application.CreateDetailView(objectSpace, currentObject, false);
                    detailView.ViewEditMode = ViewEditMode.Edit;
                    Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
                    {
                        var item = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == currentObject.ID.ToString());
                        item.FlowStep = currentObject.FlowStep;
                        item.Name = currentObject.Name;

                        objectSpace.CommitChanges();

                        var node1 = flowDiagramModel.NewNode(currentObject.ID.ToString(), 20, 20);
                        node1.Title = currentObject.Name;
                        flowDiagramModel.Diagram.Nodes.Add(node1);
                    });
                }
            }
        }

        private void InitFlow(Guid Id)
        {
            var objectSpace = this.ObjectSpace;
            var flowDiagram = objectSpace.GetObjectByKey<FlowDiagram>(Id);
            foreach (var detail in flowDiagram?.FlowDiagramDetails)
            {
                var node1 = flowDiagramModel.NewNode(detail.ID.ToString(), detail.X, detail.Y);
                node1.Title = detail.Name;
                flowDiagramModel.Diagram.Nodes.Add(node1);
            }

            foreach (var link in flowDiagram?.FlowDiagramLinks)
            {
                var source = flowDiagramModel.Diagram.Nodes.FirstOrDefault(x => x.Id == link.Source.ID.ToString());
                var target = flowDiagramModel.Diagram.Nodes.FirstOrDefault(x => x.Id == link.Target.ID.ToString());
                if (source != null && target != null)
                {
                    var linkModel = new LinkModel(link.ID.ToString(),
                            source.Ports.FirstOrDefault(x => x.Alignment.ToString() == link.SourcePortAlignment),
                            target.Ports.FirstOrDefault(x => x.Alignment.ToString() == link.TargetPortAlignment));
					linkModel.Labels = new List<LinkLabelModel>()
			        {
				        new LinkLabelModel(linkModel, link.Status.ToString())
			        };
					flowDiagramModel.Diagram.Links.Add(linkModel);
                }
            }
        }

        protected override void OnDeactivated()
		{
			// Unsubscribe from previously subscribed events and release other references and resources.
			base.OnDeactivated();

			View.FindItem("FlowDiagramPage").ControlCreated -= FlowDiagramViewController_ControlCreated;
		}
	}
}
