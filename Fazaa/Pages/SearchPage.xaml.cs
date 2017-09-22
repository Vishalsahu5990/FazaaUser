using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class SearchPage : ContentPage
	{
		bool isFromNavigationMenu = false;
		int _loaderIndicator = 0;
		ProductModel _productsModel = null;
		CategoryModel.StorCategoryData _categoryModel = null;
		int _tabIndicatior = 0;
		double _itemHeight = 0;
		public SearchPage()
		{
			InitializeComponent();
			imgMenu.Source = "back";
			NavigationPage.SetHasNavigationBar(this, false);
			flowlistview.FlowColumnMinWidth = App.ScreenWidth/2;
			_itemHeight = App.ScreenWidth / 2;
		}
		public SearchPage(bool isfrom_navigationMenu)
		{
			InitializeComponent();
			imgMenu.Source = "navigation";
			isFromNavigationMenu = isfrom_navigationMenu;
			NavigationPage.SetHasNavigationBar(this, false);
			flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2;
			_itemHeight = App.ScreenWidth / 2;
		}
		public void back_Tapped(object sender, EventArgs e)
		{
			if(isFromNavigationMenu)
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
			else
				Navigation.PopAsync(true);
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
		void SearchBar_SearchButtonPressed(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				var text = searchBar.Text;

				SearchProduct(text);


			});
		}

		async void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as ProductModel.StorCategoryProductData;
			await Navigation.PushAsync(new ProductDetailsPage(Convert.ToInt32(item.Product_Id), item.favorite));
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			searchBar.Focus();
			searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;
			flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			searchBar.SearchButtonPressed -= SearchBar_SearchButtonPressed;
			flowlistview.FlowItemTapped -= Flowlistview_FlowItemTapped;
		}
		private async Task GetAllProductsByCategory()
		{
			_loaderIndicator = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_productsModel = WebService.GetProductsByCategory(Convert.ToInt32(_categoryModel.Store_Id), Convert.ToInt32(_categoryModel.Category_Id));


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
		private async Task SearchProduct( string text)
		{
			_loaderIndicator = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				_productsModel = WebService.SearchProduct(text);


					}).ContinueWith(async
					t =>
					{
				if (!ReferenceEquals(_productsModel.stor_category_product_data,null) )
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
					DisplayAlert("Aleart!","No product found!","Ok");
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
