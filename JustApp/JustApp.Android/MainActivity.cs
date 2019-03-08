using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Android.Content;
using Xamarin.Forms.GoogleMaps.Android;
using System.Threading.Tasks;
using Android;

namespace JustApp.Droid
{
    [Activity(Label = "JustApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            StateListener phoneStateListener = new StateListener(this);
            TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            telephonyManager.Listen(phoneStateListener, PhoneStateListenerFlags.CallState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CatchingNativeBitmapDescriptorFactory()
            };

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);

            LoadApplication(new App());
        }

        public void UpdateCallState(CallState state, string incomingNumber)
        {

        }



        // permisje 

        //https://github.com/jamesmontemagno/MarshmallowSamples/blob/master/RuntimePermissions/MarshmallowPermission/MainActivity.cs
        //https://blog.xamarin.com/requesting-runtime-permissions-in-android-marshmallow/




    }
}