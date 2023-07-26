using MauiApplication.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApplication.Customize
{
    internal class NavigationCus : RazorClassLibrary.Interface.INavigation
    {
        public async void NavigateNext(NavigationManager manager)
        {
            //manager.NavigateTo("fetchdata");
            await App.Current.MainPage.Navigation.PushAsync(new FetchData());
        }
    }
}
