using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text.Json;
using XafDevexpress.Blazor.Server.BusinessClass;
using XafDevexpress.Blazor.Server.Editors;
using XafDevexpress.Blazor.Server.Model;
using XafDevexpress.Module.BusinessObjects;
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
			flowDiagramModel.ProcessSave();
            var flowDiagram = this.View.CurrentObject as FlowDiagram;

            int i = 0;
            while (i < flowDiagram.FlowDiagramDetails.Count)
            {
                var node = flowDiagram.FlowDiagramDetails[i];
                if (flowDiagramModel.FlowDiagramDetail.Any(x => x.ID.ToString() == node.ID.ToString()) == false)
                {
					this.ObjectSpace.Delete(node);
                    flowDiagram.FlowDiagramDetails.Remove(node);
                    continue;
                }
                i++;
            }
            foreach (var detail in flowDiagramModel.FlowDiagramDetail)
			{
				var item = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.ID.ToString());
				if (item == null)
				{
					item = this.ObjectSpace.CreateObject(typeof(FlowDiagramDetail)) as FlowDiagramDetail;
					flowDiagram.FlowDiagramDetails.Add(item);
				}
				item.Name = detail.Name;
				item.X = detail.X;
				item.Y = detail.Y;
				item.FlowDiagram = flowDiagram;
				item.FlowStep = detail.FlowStep;
			}

            i = 0;
            while (i < flowDiagram.FlowDiagramLinks.Count)
            {
                var link = flowDiagram.FlowDiagramLinks[i];
                if (flowDiagramModel.FlowDiagramLink.Any(x => x.ID.ToString() == link.ID.ToString()) == false)
                {
                    this.ObjectSpace.Delete(link);
                    flowDiagram.FlowDiagramLinks.Remove(link);
                    continue;
                }
                i++;
            }
            foreach (var detail in flowDiagramModel.FlowDiagramLink)
            {
                var item = flowDiagram.FlowDiagramLinks.FirstOrDefault(x => x.ID.ToString() == detail.ID.ToString());
                if (item == null)
                {
                    item = this.ObjectSpace.CreateObject(typeof(FlowDiagramLink)) as FlowDiagramLink;
                    flowDiagram.FlowDiagramLinks.Add(item);
                }
                item.Source = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.Source.ID.ToString());
                if (item.Source == null)
                {
                    item.Source = flowDiagram.FlowDiagramDetails
                                    .FirstOrDefault(x => x.FlowStep.Name.ToString() == detail.Source.FlowStep.Name.ToString()
                                                        && x.X == detail.Source.X
                                                        && x.Y == detail.Source.Y);
                }
                item.SourcePortAlignment = detail.SourcePortAlignment;
                item.Target = flowDiagram.FlowDiagramDetails.FirstOrDefault(x => x.ID.ToString() == detail.Target.ID.ToString());
				if (item.Target == null)
				{
					item.Target = flowDiagram.FlowDiagramDetails
									.FirstOrDefault(x => x.FlowStep.Name.ToString() == detail.Target.FlowStep.Name.ToString()
														&& x.X == detail.Target.X
                                                        && x.Y == detail.Target.Y);
				}
                item.TargetPortAlignment = detail.TargetPortAlignment;
                item.Status = detail.Status;
                item.FlowDiagram = flowDiagram;
            }
        }

        private void FlowDiagramViewController_ControlCreated(object sender, EventArgs e)
		{
			if (this.View.CurrentObject is FlowDiagram flowDiagram)
			{
				flowDiagramModel = ((FlowDiagramHolder)((FlowDiagramViewItemBlazor)sender).Control).ComponentModel;
                flowDiagramModel.FlowDiagramID = flowDiagram.ID;
                flowDiagramModel.Application = Application;
                flowDiagramModel.InitDiagram();
                flowDiagramModel.InitFlow();
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
