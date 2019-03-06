using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using JustApp.Droid;

namespace JustApp
{
    public class StateListener : PhoneStateListener
    {
        private readonly MainActivity _activity;

        public StateListener(MainActivity activity)
        {
            _activity = activity;
        }
        public override void OnCallStateChanged([GeneratedEnum] CallState state, string incomingNumber)
        {
            base.OnCallStateChanged(state, incomingNumber);
            _activity.UpdateCallState(state, incomingNumber);
            Console.WriteLine(incomingNumber);
        }
    }
}