using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class EditProfilePage : ContentPage
	{
		public EditProfilePage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			var slDateofbirth = new TapGestureRecognizer();
			stDob.GestureRecognizers.Add(slDateofbirth);
			slDateofbirth.Tapped += (object sender, EventArgs e) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					datepicker.Focus();
				});

			};
			var slCity = new TapGestureRecognizer();
			stCity.GestureRecognizers.Add(slCity);
			slCity.Tapped += (object sender, EventArgs e) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					CityPicker.Focus();
				});

			};

			CityPicker.Items.Add("Delhi");
			CityPicker.Items.Add("Mumbai");
			CityPicker.Items.Add("Pune");
			CityPicker.Items.Add("Indore");



			CityPicker.SelectedIndexChanged += (object sender, EventArgs e) =>
			{
				var item = CityPicker.Items[CityPicker.SelectedIndex];
				lblCity.Text = item;
				lblCity.TextColor = Color.Black;
			};
			datepicker.DateSelected += (sender, e) =>
			{
				lblDob.Text = e.NewDate.ToString("d");
				lblDob.TextColor = Color.Black;
			};
			btnSignup.Clicked += (object sender, EventArgs e) =>
			{
				if (IsValidate())
				{
					UpdateProfile().Wait();
				}
			};
			if (StaticMethods.DeviceType() == "android")
			{
				_slTitleBar.HeightRequest = 60;
				lblTitle.Margin = new Thickness(0, 10, 0, 0);
			}
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		void TxtFirstname_Focused(object sender, FocusEventArgs e)
		{
			txtFirstname.PlaceholderColor = Color.FromHex("#BABBBA");
		}
		 
		void TxtLastname_Focused(object sender, FocusEventArgs e)
		{
			txtLastname.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtPhone_Focused1(object sender, FocusEventArgs e)
		{
			txtPhone.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtAddress_Focused(object sender, FocusEventArgs e)
		{
			txtAddress.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtZipcode_Focused(object sender, FocusEventArgs e)
		{
			txtZipcode.PlaceholderColor = Color.FromHex("#BABBBA");
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			txtFirstname.Focused += TxtFirstname_Focused;
			txtLastname.Focused += TxtLastname_Focused;
			txtPhone.Focused+= TxtPhone_Focused1;
			txtAddress.Focused+= TxtAddress_Focused;
			txtZipcode.Focused+= TxtZipcode_Focused;

			GetProfile().Wait();

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
		private bool IsValidate()
		{
			if (string.IsNullOrEmpty(txtFirstname.Text))
			{
				txtFirstname.PlaceholderColor = Color.Red;
				return false;

			}
			else if (string.IsNullOrEmpty(txtLastname.Text))
			{

				txtLastname.PlaceholderColor = Color.Red;
				return false;

			}
			else if (lblDob.Text == "DOB")
			{

				lblDob.TextColor = Color.Red;
				return false;

			}
			else if (lblCity.Text == "City/Area")
			{

				lblCity.TextColor = Color.Red;
				return false;

			}
			else if (string.IsNullOrEmpty(txtAddress.Text))
			{

				txtAddress.PlaceholderColor = Color.Red;
				return false;

			}

			else if (string.IsNullOrEmpty(txtPhone.Text))
			{

				txtPhone.PlaceholderColor = Color.Red;
				return false;

			}
			else if (string.IsNullOrEmpty(txtZipcode.Text))
			{

				txtZipcode.PlaceholderColor = Color.Red;
				return false;

			}

			else
			{
				return true;
			}
		}
		private async Task UpdateProfile()
		{
			UserModel um = null;
			int ret = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();


						um.user_firstname = txtFirstname.Text;
						um.user_lastname = txtLastname.Text;
				        um.ZipCode = txtZipcode.Text;
				        um.Address = txtAddress.Text;
						um.user_dob = lblDob.Text;
						um.user_city = lblCity.Text;
						um.user_phonenuber = txtPhone.Text;
						

				     ret = WebService.UpdateProfile(um);


					}).ContinueWith(async
					t =>
					{
						if (ret == 200)
						{

						
					CrossSecureStorage.Current.SetValue("first_name", txtFirstname.Text);
					CrossSecureStorage.Current.SetValue("last_name", txtLastname.Text);
					App.Current.MainPage = new RootPage();

						}
						else
						{
							StaticMethods.ShowToast(um.responseMessage); 
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetProfile()
		{
			UserModel um = null;
			int ret = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						
				um = WebService.GetProfile();


					}).ContinueWith(async
					t =>
					{
				if (um.responseCode == 200)
						{

					Device.BeginInvokeOnMainThread(() => 
					{
						txtFirstname.Text = um.userdetail.First_Name;
						txtLastname.Text = um.userdetail.Last_Name;
						txtPhone.Text = um.userdetail.Phone_Number.ToString();
						txtZipcode.Text = um.userdetail.ZipCode;
						txtAddress.Text = um.userdetail.Address;
						lblDob.Text = um.userdetail.Date_Of_Birth;
						lblCity.Text = um.userdetail.City;

						lblDob.TextColor = Color.Black;
						lblCity.TextColor = Color.Black;
					});

						}
						else
						{
							StaticMethods.ShowToast(um.responseMessage);
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
