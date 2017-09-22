using System;
using System.Windows.Input;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public class RootPage:MasterDetailPage
	{
public static readonly BindableProperty MenuTappedCommandProperty = BindableProperty.Create<HomePage, ICommand>(x => x.OpenMenuCommand, null);

public Command MenuTapped
{
	get { return (Command)GetValue(MenuTappedCommandProperty); }
	set
	{
        SetValue(MenuTappedCommandProperty, value);
	}
		}
		public RootPage()
		{
			OnLoad();
		}
		public RootPage(int loader_indicator,int tab_indicator)
		{
			OnLoad(loader_indicator,tab_indicator);
		}
		void OnLoad()
		{ 
			NavigationPage.SetHasNavigationBar(this, false);
			var menuPage = new MenuPage();
			this.Padding = new Thickness(0, 0, 0, 0);

			StaticDataModel.currentContext = this;

			//event to opent menu slider
			MenuTapped = new Command(async (x) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
											   {

												   this.IsPresented = true;
											   });
			});


			var slhome = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stHome").GestureRecognizers.Add(slhome);
			slhome.Tapped += (object sender, EventArgs e) =>
			{
				Page displayPage = (Login)Activator.CreateInstance<Login>();

				Detail = new NavigationPage(new HomePage());

				IsPresented = false;

			};
			var slAboutUs = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stAboutus").GestureRecognizers.Add(slAboutUs);
			slAboutUs.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new AboutUsPage());

				IsPresented = false;
			};
			var slcategories = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stCategories").GestureRecognizers.Add(slcategories);
			slcategories.Tapped += (object sender, EventArgs e) =>
			{
				//Page displayPage = (CategoryPage)Activator.CreateInstance<CategoryPage>();

				Detail = new NavigationPage(new CategoryPage(1));

				IsPresented = false;

			};
			var slMyAccount = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stAccount").GestureRecognizers.Add(slMyAccount);
			slMyAccount.Tapped += (object sender, EventArgs e) =>
			{

				Page displayPage = (MyAccountPage)Activator.CreateInstance<MyAccountPage>();

				Detail = new NavigationPage(new MyAccountPage());

				IsPresented = false;
			};
			var stWishlist = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stWishlist").GestureRecognizers.Add(stWishlist);
			stWishlist.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new FavouritePage());
				IsPresented = false;
			};
			var stFavourite = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stFavourite").GestureRecognizers.Add(stFavourite);
			stFavourite.Tapped += (object sender, EventArgs e) =>
			{

				Detail = new NavigationPage(new MyCartPage());

				IsPresented = false;

			};
			var stSetting = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stSetting").GestureRecognizers.Add(stSetting);
			stSetting.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new PaymentPage());
				IsPresented = false;

			};
			var stSearch = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stSearch").GestureRecognizers.Add(stSearch);
			stSearch.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new SearchPage(true));
				IsPresented = false;

			};
			var stLogout = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stLogout").GestureRecognizers.Add(stLogout);
			stLogout.Tapped += async (object sender, EventArgs e) =>
			{
						IsPresented = false;
						var answer = await DisplayAlert(null, "Would you like to Logout?", "Yes", "No");
						if (answer)
						{
							CrossSecureStorage.Current.DeleteKey("userid");
							App.Current.MainPage = new NavigationPage(new Login());
						}
						else
					{

					}
			};


			Master = menuPage;
			Detail = new NavigationPage(new HomePage());


			var layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand
											   ,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.End,
				HeightRequest = 60
			};
			var content = new ContentPage()
			{

			};
			var username = menuPage.FindByName<Label>("lblUsername");
			var email = menuPage.FindByName<Label>("lblEmail");

			var model = StaticMethods.GetLocalSavedData();
			if (model != null)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{

					username.Text = model.First_Name + " " + model.Last_Name;
					email.Text = model.Email_Address;

				});
			}
		}
		void OnLoad(int loader_indicator,int tab_indicator)
		{
			NavigationPage.SetHasNavigationBar(this, false);
			var menuPage = new MenuPage();
			this.Padding = new Thickness(0, 0, 0, 0);

			StaticDataModel.currentContext = this;

			//event to opent menu slider
			MenuTapped = new Command(async (x) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
											   {

												   this.IsPresented = true;
											   });
			});


			var slhome = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stHome").GestureRecognizers.Add(slhome);
			slhome.Tapped += (object sender, EventArgs e) =>
			{
				Page displayPage = (Login)Activator.CreateInstance<Login>();

				Detail = new NavigationPage(new HomePage());

				IsPresented = false;

			};
			var slAboutUs = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stAboutus").GestureRecognizers.Add(slAboutUs);
			slAboutUs.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new AboutUsPage());

				IsPresented = false;
			};
			var slcategories = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stCategories").GestureRecognizers.Add(slcategories);
			slcategories.Tapped += (object sender, EventArgs e) =>
			{
				//Page displayPage = (CategoryPage)Activator.CreateInstance<CategoryPage>();

				Detail = new NavigationPage(new CategoryPage(1));

				IsPresented = false;

			};
			var slMyAccount = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stAccount").GestureRecognizers.Add(slMyAccount);
			slMyAccount.Tapped += (object sender, EventArgs e) =>
			{

				Page displayPage = (MyAccountPage)Activator.CreateInstance<MyAccountPage>();

				Detail = new NavigationPage(new MyAccountPage());

				IsPresented = false;
			};
			var stWishlist = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stWishlist").GestureRecognizers.Add(stWishlist);
			stWishlist.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new FavouritePage());
				IsPresented = false;
			};
			var stFavourite = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stFavourite").GestureRecognizers.Add(stFavourite);
			stFavourite.Tapped += (object sender, EventArgs e) =>
			{

				Detail = new NavigationPage(new MyCartPage());

				IsPresented = false;

			};
			var stSetting = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stSetting").GestureRecognizers.Add(stSetting);
			stSetting.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new PaymentPage());
				IsPresented = false;

			};
			var stSearch = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stSearch").GestureRecognizers.Add(stSearch);
			stSearch.Tapped += (object sender, EventArgs e) =>
			{
				Detail = new NavigationPage(new SearchPage(true));
				IsPresented = false;
			};
			var stLogout = new TapGestureRecognizer();
			menuPage.FindByName<StackLayout>("stLogout").GestureRecognizers.Add(stLogout);
			stLogout.Tapped += async (object sender, EventArgs e) =>
			{
				IsPresented = false;
				var answer = await DisplayAlert(null, "Would you like to Logout?", "Yes", "No");
				if (answer)
				{
					CrossSecureStorage.Current.DeleteKey("userid");
					App.Current.MainPage = new NavigationPage(new Login());
				}
				else
				{

				}
			};


			Master = menuPage;
			Detail = new NavigationPage(new HomePage(loader_indicator,tab_indicator));


			var layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand
											   ,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.End,
				HeightRequest = 60
			};
			var content = new ContentPage()
			{

			};
			var username = menuPage.FindByName<Label>("lblUsername");
			var email = menuPage.FindByName<Label>("lblEmail");

			var model = StaticMethods.GetLocalSavedData();
			if (model != null)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{

					username.Text = model.First_Name + " " + model.Last_Name;
					email.Text = model.Email_Address;

				});
			}
		}
	}
}
