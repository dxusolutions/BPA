using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Interface
{
    public interface INavigation
    {
        public void NavigateNext(NavigationManager manager);
    }
}