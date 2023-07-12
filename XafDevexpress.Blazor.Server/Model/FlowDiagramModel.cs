using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using XafDevexpress.Module.BusinessObjects;
using Diagrams = Blazor.Diagrams;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using System.Reflection;
using Blazor.Diagrams.Core.Models.Base;

namespace XafDevexpress.Blazor.Server.Model
{
    public class FlowDiagramModel : ComponentModelBase
    {
        public delegate void RemoveLink(Diagrams.Core.Models.Base.BaseLinkModel link);
        public event RemoveLink RemoveLinkEvent;

        public delegate void OpenLink(string Id, string status);
        public event OpenLink OpenLinkEvent;

        public delegate void OpenFlowDetail(string Id);
        public event OpenFlowDetail OpenFlowDetailEvent;

        public delegate void DeleteFlowDetail(string Id);
        public event DeleteFlowDetail DeleteFlowDetailEvent;

		public Diagram Diagram
        {
            get => GetPropertyValue<Diagram>();
            set => SetPropertyValue(value);
        }

        public Guid FlowDiagramID
        {
            get => GetPropertyValue<Guid>();
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
            Diagram.Links.Removed += Links_Removed;
            Diagram.MouseDoubleClick += Diagram_MouseDoubleClick;
            Diagram.Nodes.Removed += Nodes_Removed;
        }

        private void Links_Removed(Diagrams.Core.Models.Base.BaseLinkModel obj)
        {
            RemoveLinkEvent.Invoke(obj);
        }

        private void Nodes_Removed(NodeModel obj)
        {
            DeleteFlowDetailEvent.Invoke(obj.Id);
        }

        private void Diagram_MouseDoubleClick(Diagrams.Core.Models.Base.Model arg1, Microsoft.AspNetCore.Components.Web.MouseEventArgs arg2)
        {
            if (arg1 is NodeModel nodeModel)
            {
                OpenFlowDetailEvent.Invoke(nodeModel.Id);
            }
            else if (arg1 is BaseLinkModel link)
            {
                OpenLinkEvent.Invoke(link.Id, link.Labels.FirstOrDefault()?.Content);
            }
        }

        public void NewStep()
        {
            OpenFlowDetailEvent.Invoke(string.Empty);
        }

        private void Links_Added(Diagrams.Core.Models.Base.BaseLinkModel link)
        {
            link.SourceMarker = LinkMarker.Square;
            link.TargetMarker = LinkMarker.Arrow;
            link.Router = Routers.Normal;
            link.PathGenerator = PathGenerators.Smooth;
        }

        public NodeModel NewNode(string id, double x, double y)
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
