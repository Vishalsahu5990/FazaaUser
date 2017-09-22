using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
namespace Fazaa
{
	public partial class FavouritePage : ContentPage
	{
		FavouritesItemList items;
		int _tabIndicatior = 0;
		FavouriteModel _favouriteModel = null;
		public FavouritePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			flowlistview.FlowColumnMinWidth = App.ScreenWidth;
        PrepareUI();
		}
		private void PrepareUI()
{
	try
	{
		if (StaticMethods.DeviceType() == "android")
		{
			_slTitleBar.HeightRequest = 60;
			flowlistview.HeightRequest = App.ScreenHeight - 150;
			
		}
	}
	catch (Exception ex)
	{

	}
		}protected override void OnAppearing()
		{
			base.OnAppearing();
			GetAllFavourites().Wait();
		}

		public void btnRemove(object sender, EventArgs e)
		{
var item = (Xamarin.Forms.Button)sender;
			FavouriteModel.UserFavoriteProduct listitem = (from itm in items.Items where itm.Product_Id == item.CommandParameter select itm).FirstOrDefault<FavouriteModel.UserFavoriteProduct>();
items.Items.Remove(listitem);
			RemovefromFavourites(Convert.ToInt32(listitem.Favorite_id)).Wait();
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		public void logo_Tapped(object sender, EventArgs e)
		{
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
private async Task GetAllFavourites()
{

	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				_favouriteModel = WebService.GetFavourites();


			}).ContinueWith(async
					t =>
			{
				if (_favouriteModel.responseCode == 200)
				{
					var lenth = _favouriteModel.user_favorite_product.Count;
							for (int i = 0; i<lenth; i++)
							{
						_favouriteModel.user_favorite_product[i].Product_Price = _favouriteModel.user_favorite_product[i].Product_Price + "SR";
							}
					Device.BeginInvokeOnMainThread(async () =>
					{
						items = new FavouritesItemList(_favouriteModel.user_favorite_product);
						flowlistview.FlowItemsSource = items.Items;
					});

				}
				else
				{
					StaticMethods.ShowToast(_favouriteModel.responseMessage);
				}


				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
						   );
		}
private async Task RemovefromFavourites(int favourite_id)
{
			int ret = 0;
	Task.Factory.StartNew(
		// tasks allow you to use the lambda syntax to pass wor
		() =>
		{

				ret = WebService.DeleteFromWishList(favourite_id);

		}).ContinueWith(async
					t =>
		{
			if (ret == 200)
			{
				StaticMethods.ShowToast("Successfully removed from Favourites.");
			}
			else
			{ 
				StaticMethods.ShowToast("Unbale to remove from Favourites, Please try again later");
				}
		


			StaticMethods.DismissLoader();

		}, TaskScheduler.FromCurrentSynchronizationContext()
	);
		}
	}
}
