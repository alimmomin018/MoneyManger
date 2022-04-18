﻿using Android.OS;
using AndroidX.Core.View;
using MoneyManger.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(MoneyManger.Droid.Services.EnvironmentService))]
namespace MoneyManger.Droid.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public async void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;

            //this may not be necessary(but may be for older than M)
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                await Task.Delay(50);
                WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = darkStatusBarTint;
            }

            window.SetStatusBarColor(color.ToPlatformColor());
        }
    }
}