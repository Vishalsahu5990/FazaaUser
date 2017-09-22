using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class ProductDetailsPage : ContentPage
	{
		int _product_id = 0;
		string _isFavourite = string.Empty;
		ProductDetailsModel _productDetailsModel = null;
		public ProductDetailsPage()
		{
			InitializeComponent();
		}
		public ProductDetailsPage(int product_id, string favourite)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			_product_id = product_id;
			_isFavourite = favourite;
			btnAddtoCart.Clicked += BtnAddtoCart_Clicked;
			imgProduct.HeightRequest = App.ScreenHeight / 3;

			PrepareUI();
		}
		private void PrepareUI()
		{
			try
			{
				if (StaticMethods.DeviceType() == "android")
				{
					_slTitleBar.HeightRequest = 60;
					imgBack.Margin = new Thickness(10, 0, 10, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}

		async	void BtnBuyNow_Clicked(object sender, EventArgs e)
		{
			var paymentModel = new PaymentModel();
			var item = (Xamarin.Forms.Button)sender;
			paymentModel.Store_Id = _productDetailsModel.product_detail.Store_Id;
			paymentModel.Total_Price = _productDetailsModel.product_detail.Product_Price;
			//paymentModel.cartitems = _productDetailsModel.product_detail.;

				await Navigation.PushAsync(new CheckoutPage(paymentModel));

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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnBuyNow.Clicked+= BtnBuyNow_Clicked;

			lblCarCount.Text= Convert.ToString(CrossSecureStorage.Current.GetValue ("cart_count"));
			if (lblCarCount.Text != string.Empty)
			{
				lblCarCount.IsVisible = true;
				imgCart.Margin = new Thickness(0, 0, 0, 5);
			}
			GetProductDetails().Wait();
		}

		void minus_Tapped(object sender, EventArgs e)
		{
			var count = Convert.ToInt32(lblQuantity.Text);
			if (count > 1)
			{

				lblQuantity.Text = (count - 1).ToString();
			}
		}

void plus_Tapped(object sender, EventArgs e)
		{
			var count = Convert.ToInt32(lblQuantity.Text);
			if (count < 10)
			{

				lblQuantity.Text = (count +1).ToString();
			}
		}

		void BtnAddtoCart_Clicked(object sender, EventArgs e)
		{
			AddtoCart(_product_id).Wait();
		}
		public void back_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync(true);
		}
		public async void search_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SearchPage());
		}
		public async void cart_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MyCartPage());
		}
		public async void addToWishlist_Tapped(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(_isFavourite))
			{
				if (_isFavourite == "NO")
					AddToWishList(_product_id).Wait();
				else
					StaticMethods.ShowToast("Already added to Favourites.");
			}

		}
		private async Task GetProductDetails()
		{

			StaticMethods.ShowLoader(); 
			Task.Factory.StartNew( 
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_productDetailsModel = WebService.GetProductsDetails(_product_id);
					}).ContinueWith(async
					t =>
					{
						if (_productDetailsModel.responseCode == 200)
						{

							Device.BeginInvokeOnMainThread(async () =>
							{
								imgProduct.Source = _productDetailsModel.product_detail.Product_Image;
								lblProductName.Text = _productDetailsModel.product_detail.Product_Name;
								lblPrice.Text = _productDetailsModel.product_detail.Product_Price + "SR";
								lblDetails.Text = _productDetailsModel.product_detail.Product_description;
							});

						}
						else
						{
							StaticMethods.ShowToast(_productDetailsModel.responseMessage);
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
		private async Task AddToWishList(int product_id)
		{
			int ret = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
				// tasks allow you to use the lambda syntax to pass wor
				() =>
				{

					ret = WebService.AddToWishList(product_id);

				}).ContinueWith(async
					t =>
				{
					if (ret == 200)
					{
						StaticMethods.ShowToast("Successfully added to wishlist.");
					}
					else
					{
						StaticMethods.ShowToast("Unbale to add to wishlist, Please try again later!");
					}



					StaticMethods.DismissLoader();

				}, TaskScheduler.FromCurrentSynchronizationContext()
			);
		}
		private async Task AddtoCart(int product_id)
		{
			CartModel cm = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
				// tasks allow you to use the lambda syntax to pass wor
				() =>
				{

				cm = WebService.AddToCart(product_id,lblQuantity.Text);

				}).ContinueWith(async
					t =>
				{
				if (cm.responseCode == 200)
					{
						lblCarCount.Text = cm.responseMessage;
					CrossSecureStorage.Current.SetValue("cart_count", 	cm.responseMessage);
					if (lblCarCount.Text != string.Empty)
			{
				lblCarCount.IsVisible = true;
				imgCart.Margin = new Thickness(0, 0, 0, 5);
			}
					StaticMethods.ShowToast("Added to cart successfully"); 
					} 
					else
					{
						StaticMethods.ShowToast(cm.responseMessage);
					}



					StaticMethods.DismissLoader();

				}, TaskScheduler.FromCurrentSynchronizationContext()
			);
		}
	}
}
