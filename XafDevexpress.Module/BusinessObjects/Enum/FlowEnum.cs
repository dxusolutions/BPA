using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafDevexpress.Module.BusinessObjects
{
    public enum FlowStatus
	{
		[ImageName("State_Task_InProgress")]
		Inprocess,
		[ImageName("State_Task_Completed")]
		Submit,
		[ImageName("State_Validation_Invalid")]
		Reject,
    }
}
