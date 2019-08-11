using Android;
using Android.App;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Widget;

namespace gps {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, Android.Locations.ILocationListener {
		LocationManager locationManager;
		string locationProvider;

		TextView lng, lat, alt;

		public LocationManager LocationManager { get => locationManager; set => locationManager = value; }
		public string LocationProvider { get => locationProvider; set => locationProvider = value; }

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			// GPSに関して権限を要求する．
			if(CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted) {
				ActivityCompat.RequestPermissions(this,
					new string[] { Manifest.Permission.AccessFineLocation },
					1);
			} else {
				// もしすでにGPSの権限を獲得している場合には設定を進める．
				using(var locationCriteria = new Criteria()) {
					LocationManager = (LocationManager)GetSystemService(LocationService);
					locationCriteria.Accuracy = Accuracy.Fine;
					locationCriteria.PowerRequirement = Power.NoRequirement;

					LocationProvider = LocationManager.GetBestProvider(locationCriteria, true);
				}
			}

			lat = FindViewById<TextView>(Resource.Id.latitude);
			lng = FindViewById<TextView>(Resource.Id.longitude);
			alt = FindViewById<TextView>(Resource.Id.altitude);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			if(grantResults[0] == Permission.Granted) {
				using(var locationCriteria = new Criteria()) {
					LocationManager = (LocationManager)GetSystemService(LocationService);
					locationCriteria.Accuracy = Accuracy.Fine;
					locationCriteria.PowerRequirement = Power.NoRequirement;

					LocationProvider = LocationManager.GetBestProvider(locationCriteria, true);
				}
				LocationManager.RequestLocationUpdates(LocationProvider, 1500, 1, this);
			}
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		void ILocationListener.OnLocationChanged(Location location) {
			lng.Text = location.Longitude.ToString();
			lat.Text = location.Latitude.ToString();
			alt.Text = location.HasAltitude? location.Altitude.ToString() : "N/A";
		}

		void ILocationListener.OnProviderDisabled(string provider) {
			Toast.MakeText(ApplicationContext, "OnProviderDisabled", ToastLength.Short).Show();
		}

		void ILocationListener.OnProviderEnabled(string provider) {
			Toast.MakeText(ApplicationContext, "OnProviderEnabled", ToastLength.Short).Show();
		}

		void ILocationListener.OnStatusChanged(string provider, Availability status, Bundle extras) {
			Toast.MakeText(ApplicationContext, "OnStatusChanged", ToastLength.Short).Show();
		}

		protected override void OnResume() {
			base.OnResume();
			if(LocationProvider != null) {
				LocationManager.RequestLocationUpdates(LocationProvider, 1500, 1, this);
			}
		}

		protected override void OnPause() {
			base.OnPause();
			if(LocationManager != null)
				LocationManager.RemoveUpdates(this);
		}
	}
}