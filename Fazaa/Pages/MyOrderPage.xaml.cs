using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class MyOrderPage : ContentPage
	{
		List<MyOrderModel.Stordata> listOrder = null;
		public MyOrderPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
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
			GetMyOrders().Wait();
		}

		private async Task GetMyOrders()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				listOrder = WebService.GetMyAllOrder();


					}).ContinueWith(async
					t =>
					{
				if (!ReferenceEquals(listOrder,null))
						{

					Device.BeginInvokeOnMainThread(() => 
					{
					flowlistview.FlowItemsSource = listOrder;
						for (int i = 0; i < listOrder.Count;i++)
						{
							listOrder[i].Product_Image = listOrder[i].product_detail[0].Product_Image;
							listOrder[i].Product_Name = listOrder[i].product_detail[0].Product_Name;
							listOrder[i].Product_description = listOrder[i].product_detail[0].Product_description;
							listOrder[i].Product_Price = "Amount: SR" + listOrder[i].product_detail[0].Product_Price.ToString();
							listOrder[i].quantity = "Qty: " + listOrder[i].product_detail[0].quantity;

						}
					});
						}
						else
						{
							StaticMethods.ShowToast("Something went wrong, Please try again later!");
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
