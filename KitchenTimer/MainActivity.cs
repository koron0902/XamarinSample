using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace KitchenTimer {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {

		// 残り秒数
		private int sec_ = 180;

		Handler handler_;
		TextView tv_;
		AudioTrack audio_;

		// Beep音用の変数群
		const double amplification_ = 0.4;
		const int sampleRate_ = 44100; // [samples / sec]
		const short bitRate_ = 16;	// [bits / sec]
		const short freq_ = 440;	// [Hz] = [1 / sec]
		const double duration_ = 0.5;	// [sec]
		short[] audioBuf_;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			handler_ = new Handler();

			// (1)
			// 時間を表示させるviewの取得
			tv_ = FindViewById<TextView>(Resource.Id.remains);

			// (2)
			// Buttonのクリック動作を設定
			// どうせ保持していても使わないので直接構築
			// 1000ミリ秒経過後にタスクを実行するように設定しているぞ
			FindViewById<Button>(Resource.Id.start)
				.Click += (sender, e) => {
					handler_.PostDelayed(() => Action(), 1000);
				};
		}

		/// @brief	: Resume時に呼び出されるhook
		/// @return	: None
		protected override void OnResume() {
			base.OnResume();

			// [samples / sec] * [sec] = [samples]
			int samples = (int)(sampleRate_ * duration_); 
			audioBuf_ = new short[samples];

			// (3)
			// Beep音の生成
			for(int point = 0;point < samples;point++) {
				// pointの最大値はsamplesと同値．すなわち発音時間でのsample数
				// すなわち，point / sampleRate_は時間位置(time / freq)と等価的存在
				audioBuf_[point] = (short)((amplification_ * short.MaxValue) *
					System.Math.Sin(2.0 * System.Math.PI * freq_ * point / sampleRate_));
			}

			audio_ = new AudioTrack(Stream.Music,
															sampleRate_,
															ChannelOut.Mono,
															Encoding.Pcm16bit,
															audioBuf_.Length * bitRate_ / 8,
															AudioTrackMode.Static);
			audio_.Write(audioBuf_, 0, audioBuf_.Length);
		}


		// (4)
		/// @brief	: 一定時間ごとに行うタスク
		/// @return	: None
		void Action() {
			handler_.RemoveCallbacks(Action);
			sec_--;

			if(sec_ > 0)
				handler_.PostDelayed(Action, 1000);
			else
				Beep();

			// (5)
			// この関数が別スレッドで動いているので，
			// UIスレッドを明示的に指定
			RunOnUiThread(() => {
				tv_.Text = (sec_ / 60).ToString("D2") +
				":" +
				(sec_ % 60).ToString("D2");
			});

		}


		///  @brief		: Beep音を鳴らす
		///  @return	: None
		void Beep() {
			audio_.Stop();
			audio_.ReloadStaticData();
			audio_.Play();
		}
	}
}

