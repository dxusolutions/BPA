using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using Microsoft.AspNetCore.Components;
using XafDevexpress.Blazor.Server.Pages;
using XafDevexpress.Module.BusinessObjects;

namespace XafDevexpress.Blazor.Server
{
	public class FlowProcessComponentAdapter : IComponentAdapter, IComplexControl
	{
		private XafApplication application;
		private IObjectSpace objectSpace;
		private RenderFragment component;

		public RenderFragment ComponentContent
		{
			get
			{
				if (component == null)
				{
					component = builder => {
                        builder.OpenComponent<Pages.FlowProcessComponent>(0);
						//builder.AddAttribute(1, nameof(FlowProcessComponent.ObjectSpace), objectSpace);
						//builder.AddAttribute(2, nameof(FlowProcessComponent.FlowDiagramDetailId), "");
						builder.CloseComponent();
                    };
                }
				return component;
			}
		}

		public void Refresh() { }

		public void Setup(IObjectSpace objectSpace, XafApplication application)
		{
			this.application = application;
			this.objectSpace = objectSpace;
        }

		public object GetValue()
		{
			return null;
		}

		public void SetValue(object value)
		{

		}

		public event EventHandler ValueChanged;
	}
}