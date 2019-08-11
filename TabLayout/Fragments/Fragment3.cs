
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Support.V4;

namespace TabLayoutSample {
	public class Fragment3 : Android.Support.V4.App.Fragment {
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			var view = inflater.Inflate(Resource.Layout.LayoutFragment3, container, false);
			var button = view.FindViewById<Button>(Resource.Id.button3);
			button.Click += (sender, e) => {
				Toast.MakeText(Application.Context, "Button3が押されました．", ToastLength.Short).Show();
			};

			return view;
		}
	}
}
