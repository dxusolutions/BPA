using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Newtonsoft.Json;
using XafDevexpress.Module.BusinessObjects;
using Diagrams = Blazor.Diagrams;
using Blazor.Diagrams.Algorithms;
using XafDevexpress.Blazor.Server.BusinessClass;
using DevExpress.ExpressApp.Model;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using System.Linq;
using System.Security.AccessControl;
using DevExpress.ExpressApp.Editors;
using static System.Net.Mime.MediaTypeNames;
using DevExpress.Map.Native;
using DevExpress.ExpressApp.Blazor.Components;

namespace XafDevexpress.Blazor.Server.Model
{
    public class FlowDiagramModel : ComponentModelBase
    {
        public Diagram Diagram
        {
            get => GetPropertyValue<Diagram>();
            set => SetPropertyValue(value);
        }

        public XafApplication Application
        {
            get => GetPropertyValue<XafApplication>();
            set => SetPropertyValue(value);
        }

        public Guid FlowDiagramID
        {
            get => GetPropertyValue<Guid>();
            set => SetPropertyValue(value);
        }

        public IList<FlowDiagramDetail> FlowDiagramDetail
        {
            get => GetPropertyValue<IList<FlowDiagramDetail>>();
            set => SetPropertyValue(value);
        }

        public IList<FlowDiagramLink> FlowDiagramLink
        {
            get => GetPropertyValue<IList<FlowDiagramLink>>();
            set => SetPropertyValue(value);
        }

        public void InitDiagram()
        {
            var options = new DiagramOptions
            {
                DeleteKey = "Delete", // What key deletes the selected nodes/links
                DefaultNodeComponent = null, // Default component for nodes
                AllowMultiSelection = true, // Whether to allow multi selection using CTRL
                Links = new DiagramLinkOptions
                {

                },
                Zoom = new DiagramZoomOptions
                {
                    Minimum = 0.5, // Minimum zoom value
                    Inverse = false, // Whether to inverse the direction of the zoom when using the wheel
                    Enabled = false
                },
            };
            Diagram = new Diagram(options);

            Diagram.Links.Added += Links_Added;
            Diagram.MouseDoubleClick += Diagram_MouseDoubleClick;
        }

        private void Diagram_MouseDoubleClick(Diagrams.Core.Models.Base.Model arg1, Microsoft.AspNetCore.Components.Web.MouseEventArgs arg2)
        {
            if (arg1 is NodeModel nodeModel)
            {
                var objectSpace = Application.CreateObjectSpace(typeof(FlowDiagramDetail));
                FlowDiagramDetail currentObject = objectSpace.GetObjectByKey<FlowDiagramDetail>(new Guid(nodeModel.Id));
                var detailView = Application.CreateDetailView(objectSpace, currentObject, false);
                detailView.ViewEditMode = ViewEditMode.Edit;
                Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
                {
                    var item = FlowDiagramDetail.FirstOrDefault(x => x.ID.ToString() == currentObject.ID.ToString());
                    item.FlowStep = currentObject.FlowStep;
                    item.Name = currentObject.Name;
                });
            }
        }

        public void NewStep()
        {
            var objectSpace = Application.CreateObjectSpace(typeof(FlowDiagramDetail));
            FlowDiagramDetail currentObject = objectSpace.CreateObject(typeof(FlowDiagramDetail)) as FlowDiagramDetail;
            currentObject.FlowDiagram = objectSpace.GetObjectByKey<FlowDiagram>(FlowDiagramID);
            var detailView = Application.CreateDetailView(objectSpace, currentObject, false);
            detailView.ViewEditMode = ViewEditMode.Edit;
            Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
            {
                FlowDiagramDetail.Add(currentObject);
                //
                var node1 = NewNode(currentObject.ID.ToString(), 20, 20);
                node1.Title = currentObject.Name;
                Diagram.Nodes.Add(node1);
            });
        }

        public void InitFlow()
        {
            FlowDiagramDetail = new List<FlowDiagramDetail>();
            FlowDiagramLink = new List<FlowDiagramLink>();

            var objectSpace = Application.CreateObjectSpace(typeof(FlowDiagram));
            var flowDiagram = objectSpace.GetObjectByKey<FlowDiagram>(FlowDiagramID);
            foreach (var detail in flowDiagram.FlowDiagramDetails)
            {
                var node1 = NewNode(detail.ID.ToString(), detail.X, detail.Y);
                node1.Title = detail.Name;
                Diagram.Nodes.Add(node1);

                FlowDiagramDetail.Add(detail);
            }

            foreach (var link in flowDiagram.FlowDiagramLinks)
            {
                var source = Diagram.Nodes.FirstOrDefault(x => x.Id == link.Source.ID.ToString());
                var target = Diagram.Nodes.FirstOrDefault(x => x.Id == link.Target.ID.ToString());
                if (source != null && target != null)
                {
                    Diagram.Links.Add(new LinkModel(link.ID.ToString(), source.Ports.FirstOrDefault(x => x.Alignment.ToString() == link.SourcePortAlignment),
                            target.Ports.FirstOrDefault(x => x.Alignment.ToString() == link.TargetPortAlignment)));

                    var newLink = new FlowDiagramLink();
                    newLink.Source = link.Source;
                    newLink.SourcePortAlignment = link.SourcePortAlignment;
                    newLink.Target = link.Target;
                    newLink.TargetPortAlignment = link.TargetPortAlignment;
                    newLink.Status = link.Status;
                    FlowDiagramLink.Add(newLink);
                }
            }
        }

        private void Links_Added(Diagrams.Core.Models.Base.BaseLinkModel link)
        {
            link.SourceMarker = LinkMarker.Square;
            link.TargetMarker = LinkMarker.Arrow;
            link.Router = Routers.Normal;
            link.PathGenerator = PathGenerators.Smooth;
            if (link.SourceNode.AllLinks.Count(x => x.SourceNode.Id == link.SourceNode.Id) == 2
                || link.SourceNode.Links.Count(x => x.SourceNode.Id == link.SourceNode.Id) == 2)
            {
                link.Labels = new List<LinkLabelModel>()
            {
                new LinkLabelModel(link, "Reject")
            };
            }
            else
            {
                link.Labels = new List<LinkLabelModel>()
                {
                    new LinkLabelModel(link, "Submit")
                };
            }
        }

        public void ProcessSave()
        {
            int i = 0;
            while (i < FlowDiagramDetail.Count)
            {
                var node = FlowDiagramDetail[i];
                if (Diagram.Nodes.Any(x => x.Id == node.ID.ToString()) == false)
                {
                    FlowDiagramDetail.Remove(node);
                    continue;
                }
                i++;
            }

            foreach (var node in Diagram.Nodes)
            {
                var item = FlowDiagramDetail.FirstOrDefault(x => x.ID.ToString() == node.Id);
                item.X = node.Position.X;
                item.Y = node.Position.Y;
            }

                        i = 0;
            while (i < FlowDiagramLink.Count)
            {
                var link = FlowDiagramLink[i];
                if (Diagram.Links.Any(x => x.Id == link.ID.ToString()) == false)
                {
                    FlowDiagramLink.Remove(link);
                    continue;
                }
                i++;
            }

            foreach (var link in Diagram.Links)
            {
                var item = FlowDiagramLink.FirstOrDefault(x => x.ID.ToString() == link.Id);
                if (item == null)
                {
                    item = new FlowDiagramLink();
                    FlowDiagramLink.Add(item);
                }
                item.Source = FlowDiagramDetail.FirstOrDefault(x => x.ID.ToString() == link.SourceNode.Id);
                item.SourcePortAlignment = link.SourcePort.Alignment.ToString();
                item.Target = FlowDiagramDetail.FirstOrDefault(x => x.ID.ToString() == link.TargetNode.Id);
                item.TargetPortAlignment = link.TargetPort.Alignment.ToString();
                item.Status = link.Labels.FirstOrDefault().Content.Contains("Reject") ? FlowStatus.Reject : FlowStatus.Submit;
            }
        }

        private NodeModel NewNode(string id, double x, double y)
        {
            var node = new NodeModel(id, new Point(x, y));
            node.AddPort(PortAlignment.Bottom);
            node.AddPort(PortAlignment.Top);
            node.AddPort(PortAlignment.Left);
            node.AddPort(PortAlignment.Right);
            return node;
        }
    }
}
