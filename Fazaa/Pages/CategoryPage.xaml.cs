using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Fazaa
{
	public partial class CategoryPage : ContentPage
	{
        int _loaderIndicator = 0;
		CategoryModel _listStoresCategories = null;
		int _tabIndicatior = 0;
		public ICommand OpenMenuCommand { get; private set; }
		double _itemHeight = 0;
		int _store_id = 0;
		public CategoryPage(int store_id)
		{
			InitializeComponent();
			_store_id = store_id;
			NavigationPage.SetHasNavigationBar(this, false);
			_loaderIndicator = 1;
			_itemHeight = App.ScreenWidth / 2;
			flowlistview.FlowColumnMinWidth = _itemHeight;
			flowlistview.RowHeight = (int)_itemHeight;
			flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;
			PrepareUI();
		}
		public CategoryPage()
		{
			InitializeComponent();

		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			_tabIndicatior = 0;
			lblCoffee.Text = "Coffee & Bakeries";


			if(_loaderIndicator==1)
			GetAllStoresCategories().Wait();

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

		}
private void PrepareUI()
{
	try
	{
				_loaderIndicator = 1;
		if (StaticMethods.DeviceType() == "android")
		{
			_slTitleBar.HeightRequest = 60;
			flowlistview.HeightRequest = App.ScreenHeight - 150;
			lblTitle.Margin = new Thickness(0, 10, 0, 0);
		}
	}
	catch (Exception ex)
	{

	}
		}
		async void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as CategoryModel.StorCategoryData;
			await Navigation.PushAsync(new ProductsPage(item));
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
			App.Current.MainPage = new RootPage(1,0);

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
		private async Task GetAllStoresCategories()
		{
			_loaderIndicator = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_listStoresCategories = WebService.GetCategoriesByStoreId(_store_id);


					}).ContinueWith(async
					t =>
					{
				if (_listStoresCategories.responseCode==200)
						{
							var lenth = _listStoresCategories.stor_category_data.Count;
							for (int i = 0; i < lenth; i++)
							{
								_listStoresCategories.stor_category_data[i].height = _itemHeight - 60;
							}
		       					Device.BeginInvokeOnMainThread(async () =>
							{
								flowlistview.FlowItemsSource = _listStoresCategories.stor_category_data;
							});

						}
						else
						{
							StaticMethods.ShowToast(_listStoresCategories.responseMessage);
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}

	}

}
