using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.SecureStorage;

namespace Fazaa
{
	public partial class ProductsPage : ContentPage
	{
int _loaderIndicator = 0;
		ProductModel _productsModel = null;
		CategoryModel.StorCategoryData _categoryModel = null;
		int _tabIndicatior = 0;
		double _itemHeight = 0;
		public ProductsPage()
		{
			InitializeComponent();
		}
		public ProductsPage(CategoryModel.StorCategoryData category_model)
		{
			InitializeComponent();

			_categoryModel = category_model;
			NavigationPage.SetHasNavigationBar(this, false); 
			lblTitle.Text = _categoryModel.Category_Name; 
			_loaderIndicator = 1;
			_itemHeight = App.ScreenWidth / 2;
			flowlistview.FlowColumnMinWidth = _itemHeight;
			flowlistview.RowHeight = (int)_itemHeight;

			flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;
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
			imgBack.Margin = new Thickness(10,0,10,0);
			//lblTitle.Margin = new Thickness(0, 10, 0, 0);
		}
	}
	catch (Exception ex)
	{

	}
		}
		protected override void OnAppearing() 
		{
			base.OnAppearing();

			lblCarCount.Text= Convert.ToString(CrossSecureStorage.Current.GetValue ("cart_count"));
			if (lblCarCount.Text != string.Empty) 
			{
				lblCarCount.IsVisible = true;
				imgCart.Margin = new Thickness(0, 0, 0, 5);
			}

			if(_loaderIndicator==1)
			GetAllProductsByCategory().Wait();
		
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		async void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as ProductModel.StorCategoryProductData;
			await	Navigation.PushAsync(new ProductDetailsPage(Convert.ToInt32(item.Product_Id), item.favorite));
		}

		public void back_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync(true);
		}
		public async void search_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SearchPage());
		}
		public async void wishlist_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FavouritePage());
		}
		public async void cart_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MyCartPage());
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
		private async Task GetAllProductsByCategory()
		{
			_loaderIndicator = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
				 
				_productsModel = WebService.GetProductsByCategory(Convert.ToInt32(_categoryModel.Store_Id),Convert.ToInt32(_categoryModel.Category_Id));


					}).ContinueWith(async
					t =>
					{
						if (_productsModel.responseCode == 200)
						{
					var lenth = _productsModel.stor_category_product_data.Count;
							for (int i = 0; i < lenth; i++)
							{
                              _productsModel.stor_category_product_data[i].height = _itemHeight - 80;
								_productsModel.stor_category_product_data[i].Product_Price = _productsModel.stor_category_product_data[i].Product_Price + "SR";
							}
							Device.BeginInvokeOnMainThread(async () =>
							{
								flowlistview.FlowItemsSource = _productsModel.stor_category_product_data;
							});

						}
						else
						{
							StaticMethods.ShowToast(_productsModel.responseMessage);
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
