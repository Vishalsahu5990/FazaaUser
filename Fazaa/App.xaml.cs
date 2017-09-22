using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{

	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;
		public static User User { get; set; }
		public static IGeolocator locator;
		public App() 
		{
			InitializeComponent();
			locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

    		var exists = CrossSecureStorage.Current.HasKey("userid");
			if (!exists)
			{
				MainPage = new NavigationPage(new Fazaa.Login());  
			}
			else
			{ 
				StaticDataModel.userId= Convert.ToInt32(CrossSecureStorage.Current.GetValue ("userid"));
				MainPage = new NavigationPage(new Fazaa.RootPage());
			} 

			//MainPage = new NavigationPage(new Fazaa.DelieveryMapPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
