using MoneyManger.ViewModels;
using MoneyManger.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyManger
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }
    }
}
