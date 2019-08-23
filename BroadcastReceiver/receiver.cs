
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

namespace BroadcastReceiver {
	[BroadcastReceiver]
	public class receiver : Android.Content.BroadcastReceiver {
		public override void OnReceive(Context context, Intent intent) {
			Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
			if(intent.Action == Intent.ActionBatteryChanged) {
				MainActivity.Update(intent.GetIntExtra(BatteryManager.ExtraLevel, -1 ));
			}
		}
	}
}
