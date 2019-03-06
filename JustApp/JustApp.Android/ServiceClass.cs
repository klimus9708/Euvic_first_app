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
using System.Threading;
using Android.Util;

namespace JustApp.Droid
{
    [Service]
    class ServiceClass : Service

    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        private Timer _Check_timer_Data;
        public void DebugMyBGService()
        {
            _Check_timer_Data = new Timer((o) => { Log.Debug("SS", "Detect Service"); },
                null, 0, 2000);
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug("SS", "My Bg Services Has Started Succeded");
            DebugMyBGService();
            return StartCommandResult.NotSticky;
        }
        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }

}
