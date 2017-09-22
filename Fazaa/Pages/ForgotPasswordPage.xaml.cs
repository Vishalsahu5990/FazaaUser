using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class ForgotPasswordPage : ContentPage
	{
		public ForgotPasswordPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		async void back_Tapped(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync();
		}

		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			txtEmail.TextColor = Color.Black;
		}

		void BtnSubmit_Clicked(object sender, EventArgs e)
		{
			if (IsValidte())
			{
				ForgotPassword().Wait();
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnSubmit.Clicked+= BtnSubmit_Clicked;
			txtEmail.Focused+= TxtEmail_Focused;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnSubmit.Clicked -= BtnSubmit_Clicked;
			txtEmail.Focused -= TxtEmail_Focused;
		}
		private void PrepareUI()
		{
			try
			{
				if (StaticMethods.DeviceType() == "android")
				{
					imgBG.IsVisible = false;
					this.BackgroundImage = "login_bg";
					btnSubmit.BorderRadius = 50;
					btnSubmit.BorderRadius = 50;

				}


			}
			catch (Exception ex)
			{

			}
		}
		bool IsValidte()
		{
			if (string.IsNullOrEmpty(txtEmail.Text))
			{

				txtEmail.PlaceholderColor = Color.Red;
				return false;

			}
			else if (!string.IsNullOrEmpty(txtEmail.Text))
			{

				if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					txtEmail.TextColor = Color.Red;
					return false;
				}
				else
				{
					return true;
				}


			}
			else
			{ 
			return true;
			}


		}
		private async Task ForgotPassword()
		{
			string ret = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{


				ret = WebService.ForgotPassword(txtEmail.Text);


					}).ContinueWith(async
					t =>
					{
				if (!string.IsNullOrEmpty(ret))
						{

							
							StaticMethods.ShowToast("A link was sent on your email address please check your inbox to change  your password.");
							Navigation.PopAsync();

						}
						else
						{
							StaticMethods.ShowToast("Something went wrong, Plese try again later!");
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
