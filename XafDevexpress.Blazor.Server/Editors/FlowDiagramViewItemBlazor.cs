using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using DevExpress.ExpressApp.Blazor.Components;
using XafDevexpress.Blazor.Server.Model;
using XafDevexpress.Blazor.Server.Pages;

namespace XafDevexpress.Blazor.Server.Editors
{
	public interface IModelFlowDiagramViewItemBlazor : IModelViewItem { }

	[ViewItem(typeof(IModelFlowDiagramViewItemBlazor))]
	public class FlowDiagramViewItemBlazor : ViewItem, IComplexViewItem
	{
		public class FlowDiagramHolder : IComponentContentHolder
		{
			public FlowDiagramHolder(FlowDiagramModel componentModel)
			{
				ComponentModel = componentModel;
			}
			public FlowDiagramModel ComponentModel { get; }
			RenderFragment IComponentContentHolder.ComponentContent => ComponentModelObserver.Create(ComponentModel, FlowDiagramComponent.Create(ComponentModel));
		}
		private XafApplication application;
		public FlowDiagramViewItemBlazor(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }
		void IComplexViewItem.Setup(IObjectSpace objectSpace, XafApplication application)
		{
			this.application = application;
		}
		protected override object CreateControlCore() => new FlowDiagramHolder(new FlowDiagramModel());
		protected override void OnControlCreated()
		{
			base.OnControlCreated();
		}
    }
}