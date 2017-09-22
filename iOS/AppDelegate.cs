using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
//using PushNotification.Plugin;
using UIKit;

namespace Fazaa.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		string userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//CrossPushNotification.Initialize<CrossPushNotificationListener>();
			// set default useragent
			NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
			NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();
			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

			//device screen size
			App.ScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;

			SetupTitleBar();
			//For Auth Login
			Xamarin.Auth.Presenters.OAuthLoginPresenter.PlatformLogin = (authenticator) =>
			{
				var oAuthLogin = new OAuthLoginPresenter();
				oAuthLogin.Login(authenticator);
			};


			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
		private void SetupTitleBar() 
		{

			UITabBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(50f, 111f, 183f); //bar background
			//UINavigationBar.Appearance.TintColor = UIColor.FromRGB(1f, 0.377f, 0.376f);//Tint color of button items
			//UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
			//{
			//	TextColor = UIColor.FromRGB(0.76f, 0.76f, 0.76f)

			//});
		}
		private void PrepareRemoteNotification()
		{
			try
			{
				if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
				{
					var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
						UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
						new NSSet());

					UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
					UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					UIApplication.SharedApplication.RegisterForRemoteNotifications();
				}
				else
				{
					UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
					UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

				}
			}
			catch (Exception ex)
			{

			}
		}
		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			//if (CrossPushNotification.Current is IPushNotificationHandler)
			//{
			//	((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
			//}
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary options, Action<UIBackgroundFetchResult> completionHandler)
		{
			//if (CrossPushNotification.Current is IPushNotificationHandler)
			//{
				
			//	((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(options);
			//}

		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken))
			{
				DeviceToken = DeviceToken.Trim('<').Trim('>');
				DeviceToken = DeviceToken.Replace(" ", "");
				StaticDataModel.DeviceToken = DeviceToken.ToString();
				Console.WriteLine(DeviceToken);
			}
			//if (CrossPushNotification.Current is IPushNotificationHandler)
			//{
			//	((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
			//}
		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			application.RegisterForRemoteNotifications();
		}



		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			//if (CrossPushNotification.Current is IPushNotificationHandler)
			//{
			//	((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
			//}
		}
	}
}
