using Microsoft.AspNetCore.Components;
using RazorClassLibrary.Interface;
using RazorClassLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Customize
{
    internal class NavigationCus : RazorClassLibrary.Interface.INavigation
    {
        public void NavigateNext(NavigationManager manager)
        {
            manager.NavigateTo("fetchdata");
        }
    }
}
