﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Util;
using Android.Content;

namespace SisVac.Droid
{

    [Activity(Label="SisVacRD", Icon = "@mipmap/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
        }
    }
}
