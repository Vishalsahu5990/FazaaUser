using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Fazaa
{
	public partial class DelieveryMapPage : ContentPage
	{
		public DelieveryMapPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			//PrepareUI();
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		public void grocerry_Tapped(object sender, EventArgs e)
		{
			App.Current.MainPage = new RootPage(1, 0);

		}
		public void coffee_Tapped(object sender, EventArgs e)
		{
			App.Current.MainPage = new RootPage(2, 1);

		}
		public void pharmacies_Tapped(object sender, EventArgs e)
		{
			App.Current.MainPage = new RootPage(3, 2);

		}
		public void restaurants_Tapped(object sender, EventArgs e)
		{
			App.Current.MainPage = new RootPage(4, 3);

		}
		public void deliever_Tapped(object sender, EventArgs e)
		{
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var position = await App.locator.GetPositionAsync();
			//AddBarMarkerOnMap(position.Latitude, position.Longitude);
		}
		void PrepareUI()
		{
			//var height=App.ScreenHeight / 2;
			//_MainRelativeLayout.HeightRequest = height;
			//_map.HeightRequest = height;
		}
		//private void AddBarMarkerOnMap(double lat, double lng)
		//{
		//	_map.Pins.Clear();
		//	try
		//	{
		//		var pin = new CustomPin
		//		{
		//			Pin = new Pin
		//			{
		//				Type = PinType.Place,
		//				Position = new Xamarin.Forms.Maps.Position(lat, lng),
		//				Label = "Path",
		//				Address = Guid.NewGuid().ToString(),
		//			},
		//			Id = "Xamarin",

		//			//Url = BarDetails.url

		//		};
		//		HomePage.staticcustomPins = 0;
		//		_map.CustomPins = new List<CustomPin> { pin };
		//		_map.Pins.Add(pin.Pin);
		//		_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lat, lng), Distance.FromMiles(3)));

		//	}
		//	catch (Exception ex)
		//	{

		//	}

		//}
	}
}
