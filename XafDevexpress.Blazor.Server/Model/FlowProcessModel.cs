using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.Internal;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Blazor.Server.Model
{
	public class FlowProcessModel : ComponentModelBase
	{
		public IList<BaseFlowField> GetFields = new List<BaseFlowField>();

		public FlowDiagramDetail FlowDiagramDetail
		{
			get => GetPropertyValue<FlowDiagramDetail>();
			set => SetPropertyValue(value);
		}

		public IObjectSpace ObjectSpace
		{
			get => GetPropertyValue<IObjectSpace>();
			set => SetPropertyValue(value);
		}
	}
}
