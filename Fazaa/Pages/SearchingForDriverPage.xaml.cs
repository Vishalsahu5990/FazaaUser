using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class SearchingForDriverPage : ContentPage
	{
		public int number = 0;
		private bool FromAcceept = false;
		public SearchingForDriverPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			NoDriverPopup.PopupClosed+= NoDriverPopup_PopupClosed;
			StartTimer();

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			NoDriverPopup.PopupClosed-= NoDriverPopup_PopupClosed;
		}
		void NoDriverPopup_PopupClosed(object sender, int e)
		{
			try
			{
				App.Current.MainPage = new RootPage();	
			}
			catch (Exception ex)
			{

			}

		}

		private void PrepareUI()
		{
			try
			{
				
				//imgDash.WidthRequest = App.ScreenWidth / 2.6;
				webview.WidthRequest = App.ScreenWidth;
				if (StaticMethods.DeviceType() == "android")
				{
					webview.WidthRequest = App.ScreenWidth / 2+15;
					webview.HeightRequest = 23;
					_slTitleBar.HeightRequest = 60;
					lblTitle.Margin = new Thickness(0, 10, 0, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void StartTimer()
		{
			number = 45;
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{

				if (number > 0)
				{
					number = number - 1; //continue
										 //lblCounter.Text = number.ToString();
					Debug.WriteLine(number.ToString());
					return true;
				}
				else
				{
					//if (!FromAcceept)
					//{
					//	Application.Current.MainPage = new RootPage();
					//}
NoDriverPopup np = new NoDriverPopup();
Navigation.PushPopupAsync(np);
					return false;
				}
                //DisplayAlert("Message","Sorry... No driver available near you, Please try after some time.",
				return false; //not continue

			});
		}
		private void StopTimer()
		{
			number = 0;
			//lblCounter.Text = "0"
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			if (number > 0)
			{ 
				
			}
			else
			{
				Navigation.PopModalAsync();
			}
		}
	}
}
