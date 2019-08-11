using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;

namespace TabLayoutSample {
	public class FragmentAdapter : FragmentPagerAdapter {
		string[] tabTitles_ = {
			"Tab1",
			"Tab2",
			"Tab3"
		};

		public FragmentAdapter(FragmentManager _fm) : base(_fm) {

		}

		public FragmentAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {
		}

		public override int Count => 3;

		public override Android.Support.V4.App.Fragment GetItem(int _position) {
			switch(_position) {
			case 0:
				return new Fragment1();
			case 1:
				return new Fragment2();
			case 2:
				return new Fragment3();
			}

			return null;
		}

		public override ICharSequence GetPageTitleFormatted(int position) {
			return new Java.Lang.String(tabTitles_[position]);
		}
	}
}
