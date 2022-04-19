using MoneyManger.Services;
using MoneyManger.Services.Theme;
using MoneyManger.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            RegisterDependencyService();
            MainPage = new AppShell();
        }

        private void RegisterDependencyService()
        {
            DependencyService.Register<EntityDataStore>();
            DependencyService.Register<TransactionDataStore>();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            Theme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            Theme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Theme.SetTheme();
            });
        }
    }
}
