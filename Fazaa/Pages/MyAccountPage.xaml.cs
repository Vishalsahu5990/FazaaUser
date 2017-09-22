using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class MyAccountPage : ContentPage
	{
		public MyAccountPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			//set name and email from local
			var model = StaticMethods.GetLocalSavedData();
			if (!ReferenceEquals(model,null))
			{
				lblUsername.Text = model.First_Name + " " + model.Last_Name;
				lblEmail.Text = model.Email_Address;
				if (model.email_varify.Equals("1"))
				{
				btnVerifyEmail.IsEnabled = false;
					btnVerifyEmail.Text = "Verified";

				}
					
					
			}
			PrepareUI();
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		private void PrepareUI()
		{
			try
			{

				if (StaticMethods.DeviceType() == "android")
				{
					_slTitleBar.HeightRequest = 60;
				lblTitle.Margin = new Thickness(0, 10, 0, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnLogout.Clicked+= BtnLogout_Clicked;
			btnVerifyEmail.Clicked+= BtnVerifyEmail_Clicked;
			btnEditProfile.Clicked+= BtnEditProfile_Clicked;
			btnProfile.Clicked+= BtnProfile_Clicked;
			btnMyOrder.Clicked += BtnMyOrder_Clicked;
			btnFavourite.Clicked += BtnFavourite_Clicked;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnLogout.Clicked -= BtnLogout_Clicked;
			btnVerifyEmail.Clicked -= BtnVerifyEmail_Clicked;
			btnEditProfile.Clicked -= BtnEditProfile_Clicked;
			btnProfile.Clicked -= BtnProfile_Clicked;
			btnMyOrder.Clicked -= BtnMyOrder_Clicked;
			btnFavourite.Clicked -= BtnFavourite_Clicked;
		}

		void BtnVerifyEmail_Clicked(object sender, EventArgs e)
		{
			VerifyEmail(lblEmail.Text).Wait();
		}

		async	void BtnEditProfile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EditProfilePage()); 
		}

		async void BtnProfile_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PaymentPage());
		}

		async void BtnMyOrder_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MyOrderPage());
		}

		async void BtnFavourite_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FavouritePage()); 
		}

		async void BtnLogout_Clicked(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(null, "Would you like to Logout?", "Yes", "No");
			if (answer)
			{
				CrossSecureStorage.Current.DeleteKey("userid");
				App.Current.MainPage = new NavigationPage(new Login());
			}
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
		private async Task VerifyEmail(string email)
		{
			string ret = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				ret = WebService.VarifyEmail(email);


					}).ContinueWith(async
					t =>
					{
						
							StaticMethods.ShowToast(ret);
						


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
