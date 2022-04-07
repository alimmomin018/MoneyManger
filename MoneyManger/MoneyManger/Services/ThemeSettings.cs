using MoneyManger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoneyManger.Services.Theme
{
    public static class Settings
    {
        // 0 = default, 1 = light, 2 = dark
        const int theme = 2;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
    }

    public static class Theme
    {
        public static void SetTheme()
        {
            switch (2)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var e = DependencyService.Get<IEnvironmentService>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.Black, false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.Black;
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                e?.SetStatusBarColor(Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.White;
                    nav.BarTextColor = Color.Black;
                }
            }
        }
    }
}
