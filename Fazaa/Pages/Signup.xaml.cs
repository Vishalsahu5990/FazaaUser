using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class Signup : ContentPage
	{
		Picker picker_city;

		public Signup()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();

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
				 lblCity.TextColor =  Color.Black;
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
					 SignUpProcess().Wait();
				 }
			 };
		}

		async void back_Tapped(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			txtFirstname.Focused += TxtFirstname_Focused;
			txtLastname.Focused += TxtLastname_Focused;
			txtEmail.Focused += TxtEmail_Focused;
			txtPhone.Focused += TxtPhone_Focused;
			txtPassword.Focused += TxtPassword_Focused;

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		void TxtFirstname_Focused(object sender, FocusEventArgs e)
		{
			txtFirstname.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtLastname_Focused(object sender, FocusEventArgs e)
		{
			txtLastname.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			txtEmail.PlaceholderColor = Color.FromHex("#BABBBA");
			txtEmail.TextColor = Color.Black;
		}

		void TxtPhone_Focused(object sender, FocusEventArgs e)
		{
			txtPhone.PlaceholderColor = Color.FromHex("#BABBBA");
		}

		void TxtPassword_Focused(object sender, FocusEventArgs e)
		{
			txtPassword.PlaceholderColor = Color.FromHex("#BABBBA");
		}
		private void PrepareUI()
		{
			try
			{
				if (StaticMethods.DeviceType() == "android")
				{
					imgBG.IsVisible = false;
					this.BackgroundImage = "login_bg";
					btnSignup.BorderRadius = 50;
					btnSignup.BorderRadius = 50;

				}


			}
			catch (Exception ex)
			{

			}
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
			else if (string.IsNullOrEmpty(txtPhone.Text))
			{

				txtPhone.PlaceholderColor = Color.Red;
				return false;

			}
			else if (string.IsNullOrEmpty(txtEmail.Text))
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
			else if (string.IsNullOrEmpty(txtPassword.Text))
			{

				txtPassword.PlaceholderColor = Color.Red;
				return false;

			}

			else
			{
				return true;
			}
		}
		private async Task SignUpProcess()
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();


						um.user_firstname = txtFirstname.Text;
				um.user_lastname = txtLastname.Text;
				um.user_email = txtEmail.Text;
				um.user_password = txtPassword.Text;
				um.user_dob = lblDob.Text;
				um.user_city = lblCity.Text;
				um.user_phonenuber = txtPhone.Text;
					um.registration_type = 1;
				       
						um = WebService.SignUp(um);


					}).ContinueWith(async
					t =>
					{
						if (um.responseCode == 200)
						{
							
							StaticDataModel.userId = Convert.ToInt32(um.userdetail.User_Id);
							CrossSecureStorage.Current.SetValue("userid", um.userdetail.User_Id.ToString());
							CrossSecureStorage.Current.SetValue("first_name", um.userdetail.First_Name.ToString());
							CrossSecureStorage.Current.SetValue("last_name", um.userdetail.Last_Name.ToString());
							CrossSecureStorage.Current.SetValue("email", um.userdetail.Email_Address.ToString());
							StaticMethods.ShowToast(um.responseMessage);
							App.Current.MainPage = new NavigationPage(new RootPage());

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
