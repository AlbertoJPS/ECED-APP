using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECED_APP
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/ic_launcher", Theme = "@style/EcedTheme.Splash", NoHistory = true, MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
            Finish();

            // Disable activity slide-in animation
            //OverridePendingTransition(0, 0);;



            //, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
        }


    }
}