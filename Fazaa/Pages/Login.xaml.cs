using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class Login : ContentPage
	{
		Userdetail _userDetailsModel = null;
		Account account;
		AccountStore store;
		bool flag = false;
		public Login()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);


			txtEmail.Text = "developer.farkya@gmail.com";
			txtPassword.Text = "1234567";
			store = AccountStore.Create();
			account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

			txtEmail.Focused += TxtEmail_Focused;
			txtPassword.Focused += TxtPassword_Focused;
			txtEmail.TextChanged += TxtEmail_TextChanged;

			btnSignup.Clicked += async (object sender, EventArgs e) =>
			 {
				 await Navigation.PushAsync(new Signup());
			 };
			btnSignIn.Clicked += (object sender, EventArgs e) =>
			{
				if (Isvalidated())
				{
					LoginProcess().Wait();
				}
			};
			PrepareUI();
		}
		public async void ForgotPasswordTapped(object sender, EventArgs e)
		{

			await Navigation.PushAsync(new ForgotPasswordPage());
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
					btnSignIn.BorderRadius = 50;
					if (Device.OS == TargetPlatform.Android)
					{
						btnCircle.BorderRadius = 8;
					}
				}

				btnSignup.WidthRequest = App.ScreenWidth / 3;
			}
			catch (Exception ex)
			{

			}
		}

		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			txtEmail.PlaceholderColor = Color.FromHex("#C1C1C1");

		}

		void TxtPassword_Focused(object sender, FocusEventArgs e)
		{
			txtPassword.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					txtEmail.TextColor = Color.Red;

				}
				else
				{
					txtEmail.TextColor = Color.Black;
				}
			}
		}
		private bool Isvalidated()
		{

			try
			{
				if (string.IsNullOrEmpty(txtEmail.Text))
				{
					txtEmail.PlaceholderColor = Color.Red;
					return false;
				}

				else if (string.IsNullOrEmpty(txtPassword.Text))
				{
					txtPassword.PlaceholderColor = Color.Red;
					return false;
				}

				else if (!string.IsNullOrEmpty(txtEmail.Text))
				{
					if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
					{
						txtEmail.TextColor = Color.Red;
						StaticMethods.ShowToast("Invalid email.");
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
			catch (Exception ex)
			{
				return false;
			}
			finally
			{

			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			MessagingCenter.Subscribe<Login>(this, "Hi", (sender) =>
			{
				// do something whenever the "Hi" message is sent
			});
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
		public void fbTapped(object sender, EventArgs e)
		{
			flag = true;
			FacebookLogin();
		}
		public void twitterTapped(object sender, EventArgs e)
		{
			flag = false;
			TwitterLogin();
		}
		private void FacebookLogin()
		{
			try
			{
				flag = true;
				var authenticator = new OAuth2Authenticator(
								clientId: Constants.ClientIdFacebook,
							   scope: "email", // the scopes for the particular API you're accessing, delimited by "+" symbols
							   authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
							   redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
				authenticator.Completed += OnAuthCompleted;
				authenticator.Error += OnAuthError;

				var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
				presenter.Login(authenticator);
			}
			catch (Exception ex)
			{

			}
		}
		private void TwitterLogin()
		{
			try
			{
				flag = false;
				var authenticator = new OAuth1Authenticator(
					consumerKey: Constants.ClientIdTwitter,
					consumerSecret: Constants.SecretTwitter,
					requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
					authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
					accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
					callbackUrl: new Uri("http://mobile.twitter.com"));

				authenticator.Completed += OnAuthCompleted;
				authenticator.Error += OnAuthError;

				var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
				presenter.Login(authenticator);
			}
			catch (Exception ex)
			{

			}
		}
		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			StaticMethods.ShowLoader();
			//var a = sender as OAuth1Authenticator;
			var authenticator = sender as OAuth2Authenticator;
			OAuth2Request request2 = null;
			OAuth1Request request1 = null;
			Response response = null;

			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			try
			{

				if (e.IsAuthenticated)
				{
					// If the user is authenticated, request their basic user data from Social 
					var UserInfoUrlFacebook = "https://graph.facebook.com/me?fields=email,first_name,last_name";
					var UserInfoUrl = "https://api.twitter.com/1.1/account/verify_credentials.json";
					var GetUserDataUrl = string.Empty;
					if (flag)
						GetUserDataUrl = UserInfoUrlFacebook;
					else
						GetUserDataUrl = UserInfoUrl;

					if (flag)
					{
						request2 = new OAuth2Request("GET", new Uri(GetUserDataUrl), null, e.Account);
						response = await request2.GetResponseAsync();
					}
					else
					{
						request1 = new OAuth1Request("GET", new Uri(GetUserDataUrl), null, e.Account);
						response = await request1.GetResponseAsync();
					}


					if (response != null)
					{
						// Deserialize the data and store it in the account store
						// The users email address will be used to identify data in SimpleDB
						string userJson = await response.GetResponseTextAsync();
						if (userJson != "Not Found")
							App.User = JsonConvert.DeserializeObject<User>(userJson);
					}
					SignUpProcess(App.User).Wait();
					if (account != null)
					{
						store.Delete(account, Constants.AppName);
					}

					store.Save(account = e.Account, Constants.AppName);



				}
			}
			catch (Exception ex)
			{

			}
			finally
			{
				StaticMethods.DismissLoader();
			}
			//Navigation.InsertPageBefore(new HomePage(), this);
			//await Navigation.PopToRootAsync();
		}


		void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}
		private async Task LoginProcess()
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();
						um.email = txtEmail.Text;
						um.password = txtPassword.Text;
						um = WebService.Login(um);


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
					CrossSecureStorage.Current.SetValue("email_varify", um.userdetail.email_varify.ToString()); 
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
		private async Task SignUpProcess(User user)
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();


						um.user_firstname = user.First_Name;
						um.user_lastname = user.Last_Name;
						um.user_email = user.Email;
						if (flag)
						{
							um.facebook_id = user.Id;
						}
						else
						{
							um.user_firstname = user.Name;
							um.twitter_id = user.Id;
						}

						um.registration_type = 2;

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
