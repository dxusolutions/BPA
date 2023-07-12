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
	public interface IModelFlowProcessViewItemBlazor : IModelViewItem { }

	[ViewItem(typeof(IModelFlowProcessViewItemBlazor))]
	public class FlowProcessViewItemBlazor : ViewItem, IComplexViewItem
	{
		public class FlowProcessHolder : IComponentContentHolder
		{
			public FlowProcessHolder(FlowProcessModel componentModel)
			{
				ComponentModel = componentModel;
			}
			public FlowProcessModel ComponentModel { get; }
			RenderFragment IComponentContentHolder.ComponentContent => ComponentModelObserver.Create(ComponentModel, FlowProcessComponent.Create(ComponentModel));
		}
		private XafApplication application;
		public FlowProcessViewItemBlazor(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }
		void IComplexViewItem.Setup(IObjectSpace objectSpace, XafApplication application)
		{
			this.application = application;
		}
		protected override object CreateControlCore() => new FlowProcessHolder(new FlowProcessModel());
		protected override void OnControlCreated()
		{
			base.OnControlCreated();
		}
	}
}