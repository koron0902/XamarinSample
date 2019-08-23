using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace BroadcastReceiver {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		static TextView tv;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			tv = FindViewById<TextView>(Resource.Id.tv1);

			receiver receiver = new receiver();
			IntentFilter filter = new IntentFilter();
			filter.AddAction(Intent.ActionBatteryChanged);
			RegisterReceiver(receiver, filter);
			
		}

		public static void Update(int _level) {
			tv.Text = _level.ToString() + "%";
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

