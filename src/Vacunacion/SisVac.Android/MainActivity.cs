using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ZXing.Mobile;
using AndroidX.AppCompat.App;

namespace SisVac.Droid
{
    [Activity(Label = "SisVacRD", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            MobileBarcodeScanner.Initialize(Application);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            ((AppCompatActivity)this).Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightNo);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            XF.Material.Droid.Material.HandleBackButton(base.OnBackPressed);
            //No need to call  Rg.Plugins.Popup.Popup.SendBackPressed();
        }
    }
}