namespace XafDevexpress.Blazor.Server.BusinessClass
{
    public class FlowDiagramChart
    {
        public List<DiagramNode> Nodes { get; set; }
        public List<DiagramLink> Links { get; set; }
    }

    public class DiagramNode
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }

    public class DiagramLink
    {
        public string Source { get; set; }

        public string SourcePort { get; set; }

        public string Target { get; set; }

        public string TargetPort { get; set; }

        public bool IsReject { get; set; }
    }
}
