using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class PickorDeliveryPage : ContentPage
	{
		PaymentModel _PaymentModel = null;
		int paymentType = 0;
		string productid = string.Empty;
		string product_quantity = string.Empty;
		public PickorDeliveryPage()
		{

			InitializeComponent(); 
			PrepareUI();
			StaticDataModel.currentContext.IsGestureEnabled = false; 
		}
		public PickorDeliveryPage(PaymentModel payment_model)
		{
			_PaymentModel = payment_model;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
			StaticDataModel.currentContext.IsGestureEnabled = false;
		}
		public void back_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		public void Pickup_Tapped(object sender, EventArgs e)
		{
			paymentType = 1;
			PrepareOrder(1);
		}
		public void Delivery_Tapped(object sender, EventArgs e)
		{
			paymentType = 2;
            PrepareOrder(2);

		}
		private void PrepareUI()
		{
			try
			{

				if (StaticMethods.DeviceType() == "android")
				{
					_slTitleBar.HeightRequest = 60;
					lblTitle.Margin = new Thickness(0, 10, 0, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void PrepareOrder(int paymentType)
		{
			try
			{
				List<string> idList=new List<string>();
				List<string> quantityList = new List<string>();

				var model = _PaymentModel.cartitems;
				for (int i = 0; i < model.Count; i++) 
				{
					idList.Add(model[i].Product_Id);
					quantityList.Add(model[i].quantity);
				}
				///Genrating Pipe seprating items to submit
				productid = string.Join("|", idList);
				product_quantity = string.Join("|", quantityList);
				_PaymentModel.Product_Id = productid;
				_PaymentModel.Product_quantity = product_quantity;
				_PaymentModel.delivery_method = paymentType.ToString();
                SubmitOrder().Wait();  
			} 
			catch (Exception ex)
			{

			}
		}
		private async Task SubmitOrder()
		{
			int ret = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						ret = WebService.AddOrder(_PaymentModel);


					}).ContinueWith(async
				t =>
		{

			if (ret == 200)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
						if(paymentType==1)
							App.Current.MainPage = new NavigationPage(new RootPage());
						else
						Navigation.PushModalAsync(new SearchingForDriverPage());	

				StaticMethods.ShowToast("Order Added successfully.");

				});

			}
			else
			{ 

				StaticMethods.ShowToast("Problem on adding your order, Please try after some time.");
			}  



			StaticMethods.DismissLoader();

		}, TaskScheduler.FromCurrentSynchronizationContext()
				); 
		}
	}
}
