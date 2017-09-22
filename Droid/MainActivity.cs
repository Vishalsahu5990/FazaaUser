using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms;
using Plugin.SecureStorage;
using Android.Gms.Common;
using Firebase.Iid;
using Android.Gms.Tasks;
using Android.Util;

namespace Fazaa.Droid
{
	[Activity(Label = "Fazaa.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);
			GetDeviceSize();
			global::Xamarin.Forms.Forms.Init(this, bundle);
			global::Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
			Xamarin.Auth.Presenters.OAuthLoginPresenter.PlatformLogin = (authenticator) =>
			{
				var oAuthLogin = new OAuthLoginPresenter();
				oAuthLogin.Login(authenticator);
			};

			UserDialogs.Init(() => (Activity)Forms.Context);
			SecureStorageImplementation.StoragePassword = "Rudiment123";
			IsPlayServicesAvailable();
			LoadApplication(new App());
		}
		private void GetDeviceSize()
		{
			try
			{
				var pixels = Resources.DisplayMetrics.WidthPixels; // real pixels
				var scale = Resources.DisplayMetrics.Density;
				int dps = (int)((pixels - 0.5f) / scale);

				App.ScreenWidth = dps;

				pixels = Resources.DisplayMetrics.HeightPixels; // real pixels
				scale = Resources.DisplayMetrics.Density;
				dps = (int)((pixels - 0.5f) / scale);

				App.ScreenHeight = dps; // real pixel
			}
			catch (Exception ex)
			{

			}
		}
		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{

				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
				{
					var code = GoogleApiAvailability.Instance.GetErrorString(resultCode);
				}
				else
				{
					Console.WriteLine("This device is not supported");

					Finish();
				}
				return false;
			}
			else
			{
				System.Threading.Tasks.Task.Run(() =>
		   {
			   var instanceid = FirebaseInstanceId.Instance;
			   instanceid.DeleteInstanceId();
			   //Log.Debug("TAG", "{0} {1}", instanceid.Token, instanceid.GetToken(this.GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
			   StaticDataModel.DeviceToken = FirebaseInstanceId.Instance.Token;
			   Console.WriteLine("Google Play Services is available.");

		   });





				return true;
			}
		}
	}
}
