using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace Fazaa
{
	public partial class HomePage : ContentPage
	{
		public static int staticcustomPins;
		int _loaderIndicator = 0;
		StoreModel _listStores = null;
		int _tabIndicatior = 0;
		public ICommand OpenMenuCommand { get; private set; }
		double _itemHeight = 0;
		bool isFromOtherPage = false;
		public HomePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			_loaderIndicator = 1;
			_tabIndicatior = 0;

			_itemHeight = App.ScreenWidth / 2;
			flowlistview.FlowColumnMinWidth = _itemHeight;
			flowlistview.RowHeight = (int)_itemHeight;
			flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;
			PrepareUI();
		}
		public HomePage(int loader_indicator,int tab_indicator)
		{
			InitializeComponent();

			isFromOtherPage = true;
			_loaderIndicator = loader_indicator;
			_tabIndicatior = tab_indicator;

			NavigationPage.SetHasNavigationBar(this, false); 

			_itemHeight = App.ScreenWidth / 2;
			flowlistview.FlowColumnMinWidth = _itemHeight; 
			flowlistview.RowHeight = (int)_itemHeight; 
			flowlistview.FlowItemTapped += Flowlistview_FlowItemTapped; 
			PrepareUI(); 
		}
		private void PrepareUI() 
		{ 
		try
			{
				
				if (StaticMethods.DeviceType()=="android")
				{
					_slTitleBar.HeightRequest = 60;
					flowlistview.HeightRequest = App.ScreenHeight - 150;
					lblTitle.Margin = new Thickness(0,10,0,0);
				}
			}
			catch (Exception ex)
			{

			} 
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();

			lblCoffee.Text = "Coffee & Bakeries";
			_slGroceries.BackgroundColor = Color.FromHex("#66B047");
			_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
			_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
			_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
			_slDeliever.BackgroundColor = Color.FromHex("#7AB064");

			if (!isFromOtherPage)
			{
				if (_loaderIndicator == 1)
					GetAllStores(1).Wait();
			}
			else
			{
				GetAllStores(_loaderIndicator).Wait();
				SetTabIndicator(_tabIndicatior);
			}

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

		}

		async void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as StoreModel.Stordata;
			await Navigation.PushAsync(new CategoryPage(Convert.ToInt32( item.store_id)));
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
			if (_tabIndicatior != 0)
			{
				_slGroceries.BackgroundColor = Color.FromHex("#66B047");
			_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
			_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
			_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
			_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
				GetAllStores(1).Wait();

			}
			_tabIndicatior = 0;
		}
		public void coffee_Tapped(object sender, EventArgs e)
		{
			if (_tabIndicatior != 1)
			{
				_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
			_slCoffee.BackgroundColor = Color.FromHex("#66B047");
			_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
			_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
			_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
				GetAllStores(2).Wait();
			}
			_tabIndicatior = 1;
		}
		public void pharmacies_Tapped(object sender, EventArgs e)
		{
			if (_tabIndicatior != 2)
			{
				_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
			_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
			_slPharmacy.BackgroundColor = Color.FromHex("#66B047");
			_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
			_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
				GetAllStores(3).Wait();
			}
			_tabIndicatior = 2;
		}
		public void restaurants_Tapped(object sender, EventArgs e)
		{
			if (_tabIndicatior != 3)
			{

				_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
			_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
			_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
			_slRestaurants.BackgroundColor = Color.FromHex("#66B047");
			_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
				GetAllStores(4).Wait();
			}
			_tabIndicatior = 3;
		}
		public async void deliever_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new DelieveryMapPage());
		}
		void SetTabIndicator(int indicator)
		{
			switch (indicator)
			{ 
			        case 0:
					_slGroceries.BackgroundColor = Color.FromHex("#66B047");
					_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
					_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
					_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
					_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
					break;
					
					case 1:
					_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
					_slCoffee.BackgroundColor = Color.FromHex("#66B047");
					_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
					_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
					_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
					break;
					
					case 2:
					_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
					_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
					_slPharmacy.BackgroundColor = Color.FromHex("#66B047");
					_slRestaurants.BackgroundColor = Color.FromHex("#7AB064");
					_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
					break;
					
					case 3:
					_slGroceries.BackgroundColor = Color.FromHex("#7AB064");
					_slCoffee.BackgroundColor = Color.FromHex("#7AB064");
					_slPharmacy.BackgroundColor = Color.FromHex("#7AB064");
					_slRestaurants.BackgroundColor = Color.FromHex("#66B047");
					_slDeliever.BackgroundColor = Color.FromHex("#7AB064");
					break;
			}
		}
		private async Task GetAllStores(int store_type_id)
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_listStores = WebService.GetAllStores(store_type_id);


					}).ContinueWith(async
					t =>
					{
						if (_listStores != null)
						{
							var lenth = _listStores.stordata.Count;
							for (int i = 0; i < lenth; i++)
							{
								_listStores.stordata[i].height = _itemHeight - 50;
							}
							Device.BeginInvokeOnMainThread(async () =>
							{
								flowlistview.FlowItemsSource = _listStores.stordata;
							});

						}
						else
						{
							StaticMethods.ShowToast(_listStores.responseMessage);
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}

	}

}
