using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace JustApp.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY", Value = Keys.GOOGLE_MAPS_ANDROID_API_KEY)]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
