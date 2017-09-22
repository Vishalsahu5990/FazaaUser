using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;
using Rg.Plugins.Popup.Extensions;
using Plugin.SecureStorage;

namespace Fazaa
{
	public partial class MyCartPage : ContentPage
	{
		CartModel _cartModel = null;
		CartItemList items;
		int _tabIndicatior = 0;
		double _totalPrice = 0;
		string store_id = string.Empty;
		public static string itemId = string.Empty;
		PaymentModel paymentModel = null;
		public MyCartPage()
		{
			InitializeComponent();
			FlowListView.Init();
			NavigationPage.SetHasNavigationBar(this, false);
			listView.ItemTapped += ListView_ItemTapped; 
			PrepareUI();

			AddToCartPopup.ItemSelected+= AddToCartPopup_ItemSelected;
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);

		}

		void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}

		private void PrepareUI()
		{
			try
			{

				if (StaticMethods.DeviceType() == "android")
				{
					_slTitleBar.HeightRequest = 60;
					listView.HeightRequest = App.ScreenHeight - 130;
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
			GetAllCartItems().Wait();

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			itemId = string.Empty;
		}
		void AddToCartPopup_ItemSelected(object sender, int e)
		{
			if (e > 0)
			{
				if (itemId != string.Empty)
				{
					CartModel.UserFavoriteProduct listitem = (from itm in items.Items where itm.Product_Id == itemId select itm).FirstOrDefault<CartModel.UserFavoriteProduct>();

					UpdateCart(Convert.ToInt32( listitem.Cart_id), e).Wait();

				}
			}

		}

		void Quantity_Clicked(object sender, System.EventArgs e)
{
	Device.BeginInvokeOnMainThread(async () =>
	{
		var item = (Xamarin.Forms.Button)sender;
		CartModel.UserFavoriteProduct listitem = (from itm in items.Items where itm.Product_Id == item.CommandParameter select itm).FirstOrDefault<CartModel.UserFavoriteProduct>();
		itemId = listitem.Product_Id;
				AddToCartPopup s = new AddToCartPopup();
Navigation.PushPopupAsync(s);
	});		}

		void Delete_Clicked(object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				var item = (Xamarin.Forms.Button)sender;
				CartModel.UserFavoriteProduct listitem = (from itm in items.Items where itm.Product_Id == item.CommandParameter select itm).FirstOrDefault<CartModel.UserFavoriteProduct>();
				items.Items.Remove(listitem);
				lblTitle.Text = "My Cart(" + items.Items.Count.ToString() + ")";
				CrossSecureStorage.Current.SetValue("cart_count", items.Items.Count.ToString());
				BindingContext = new CartViewModel() { Items = items, Total_Price = (_totalPrice - Convert.ToDouble(listitem.Product_Price.Replace("SR", "")) * Convert.ToDouble(listitem.quantity)).ToString() };
				RemovefromCart(Convert.ToInt32(listitem.Product_Id)).Wait();
			});

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
		async void Checkout_Clicked(object sender, System.EventArgs e)
		{
			paymentModel = new PaymentModel();
			var item = (Xamarin.Forms.Button)sender;
			paymentModel.Store_Id = item.CommandParameter.ToString();
			paymentModel.Total_Price = _totalPrice.ToString();
			paymentModel.cartitems = _cartModel.user_favorite_product;
			if (!string.IsNullOrEmpty(item.CommandParameter.ToString()))
				await Navigation.PushAsync(new CheckoutPage(paymentModel));
			else
				StaticMethods.ShowToast("Something went wrong, Please try again later!");
		}
		private async Task GetAllCartItems()
		{
			
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_cartModel = WebService.GetCartItems();


					}).ContinueWith(async
				t =>
		{
			try
			{
				if (_cartModel.responseCode == 200)
				{
						_totalPrice = 0;
					var lenth = _cartModel.user_favorite_product.Count;
					for (int i = 0; i < lenth; i++)
					{
							_cartModel.user_favorite_product[i].Calculated_Price=(Convert.ToDouble(_cartModel.user_favorite_product[i].Product_Price) * Convert.ToInt32(_cartModel.user_favorite_product[i].quantity)).ToString()+"SR";
						_totalPrice = _totalPrice + (Convert.ToDouble(_cartModel.user_favorite_product[i].Product_Price) * Convert.ToInt32(_cartModel.user_favorite_product[i].quantity));
						_cartModel.user_favorite_product[i].Total_Price = _totalPrice.ToString();
						store_id = _cartModel.user_favorite_product[i].Store_Id;
						_cartModel.user_favorite_product[i].Product_Price = _cartModel.user_favorite_product[i].Product_Price + "SR";

						_cartModel.user_favorite_product[i].Total_Items = lenth.ToString();
					}
					Device.BeginInvokeOnMainThread(async () =>
		{
			items = new CartItemList(_cartModel.user_favorite_product);
			listView.ItemsSource = items.Items;
			lblTitle.Text = "My Cart(" + items.Items.Count.ToString() + ")";

							BindingContext = new CartViewModel() { Items = items,Store_Id=store_id, Total_Price = _totalPrice.ToString() };
		});

				}
				else
				{

					StaticMethods.ShowToast(_cartModel.responseMessage);
				}

			}
			catch (Exception ex)
			{

			}

			StaticMethods.DismissLoader();

		}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
		private async Task RemovefromCart(int cart_id)
		{
			int ret = 0;
			Task.Factory.StartNew(
				// tasks allow you to use the lambda syntax to pass wor
				() =>
				{

					ret = WebService.DeleteFromCart(cart_id);

				}).ContinueWith(async
					t =>
				{
					if (ret == 200)
					{
						StaticMethods.ShowToast("Successfully removed from cart.");
					}
					else
					{
						StaticMethods.ShowToast("Unbale to remove from cart, Please try again later");
					}



					StaticMethods.DismissLoader();

				}, TaskScheduler.FromCurrentSynchronizationContext()
			);
		}
private async Task UpdateCart(int cart_id,int quantity)
{
	int ret = 0;
	Task.Factory.StartNew(
		// tasks allow you to use the lambda syntax to pass wor
		() =>
		{

_cartModel = WebService.UpdateCartItem(cart_id,quantity);

		}).ContinueWith(async
					t =>
		{
if (_cartModel.responseCode == 200)
				{
				_totalPrice = 0;
					var lenth = _cartModel.user_favorite_product.Count;
					for (int i = 0; i<lenth; i++)
					{
							_cartModel.user_favorite_product[i].Calculated_Price=(Convert.ToDouble(_cartModel.user_favorite_product[i].Product_Price) * Convert.ToInt32(_cartModel.user_favorite_product[i].quantity)).ToString()+"SR";
						_totalPrice = _totalPrice + (Convert.ToDouble(_cartModel.user_favorite_product[i].Product_Price) * Convert.ToInt32(_cartModel.user_favorite_product[i].quantity));
						_cartModel.user_favorite_product[i].Total_Price = _totalPrice.ToString();
						store_id = _cartModel.user_favorite_product[i].Store_Id;
						_cartModel.user_favorite_product[i].Product_Price = _cartModel.user_favorite_product[i].Product_Price + "SR";

						_cartModel.user_favorite_product[i].Total_Items = lenth.ToString();
					}
					Device.BeginInvokeOnMainThread(async() =>
		{
			items = new CartItemList(_cartModel.user_favorite_product);
listView.ItemsSource = items.Items;
			lblTitle.Text = "My Cart(" + items.Items.Count.ToString() + ")";

							BindingContext = new CartViewModel() { Items = items,Store_Id = store_id, Total_Price = _totalPrice.ToString() };
		});

				}
				else
				{

					StaticMethods.ShowToast(_cartModel.responseMessage);
				}




			StaticMethods.DismissLoader();

		}, TaskScheduler.FromCurrentSynchronizationContext()
	);
		}
	}
}
