using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace TabLayoutSample {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			var tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);
			var viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);

			viewPager.Adapter = new FragmentAdapter(SupportFragmentManager);
			tabLayout.SetupWithViewPager(viewPager);
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}