using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class CheckoutPage : ContentPage
	{
		CheckoutModel checkoutModel = null;
		PaymentModel paymentModel = null;
		string[] latArray;
        string[] longArray;
     	int _storeId = 0;
		int countSelectAddress = 0; 
		int countSelectStoreAddress = 0; 
		//int countAddress1 = 1;
		//int countAddress2 = 0;
		//int countAddress3 = 0;
		//int countCash = 0;
		//int countCreditCard = 0;
		int addressCount = 0;
		int addressSelectorCount = 1;
		int StoreaddressSelectorCount = 1;
		int paymentMethodCount = 0;
		public CheckoutPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);


			PrepareUI();
		}
		public CheckoutPage(PaymentModel pm)
		{
			paymentModel = pm;
			_storeId = Convert.ToInt32(pm.Store_Id);
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			addressCount = 0;
			AddNewAddressPopup.AddressAdded += AddNewAddressPopup_AddressAdded;
			btnAddAddress.Clicked += BtnAddAddress_Clicked;
			txtAddress.Focused += TxtAddress_Focused;
			GetCheckoutAddress().Wait();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			AddNewAddressPopup.AddressAdded -= AddNewAddressPopup_AddressAdded;
			btnAddAddress.Clicked -= BtnAddAddress_Clicked;
			txtAddress.Focused -= TxtAddress_Focused;
		}

		void TxtAddress_Focused(object sender, FocusEventArgs e)
		{
			txtAddress.PlaceholderColor = Color.Gray;
		}

		void AddNewAddressPopup_AddressAdded(object sender, string e)
		{

		}

		void BtnAddAddress_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtAddress.Text))
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					_slEntryView.IsVisible = false;
					if (addressCount == 1)
					{
						lblAddress2.Text = txtAddress.Text;
						lblAddress2.IsVisible = true;
						_slAddress2.IsVisible = true;
					}
					else if (addressCount == 2)
					{
						lblAddress3.Text = txtAddress.Text;
						lblAddress3.IsVisible = true;
						_slAddress3.IsVisible = true;
					}
					txtAddress.Text = string.Empty;
				});
			}
			else
			{
				txtAddress.PlaceholderColor = Color.Red;
			}
		}

		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		public void _slCash_Tapped(object sender, EventArgs e)
		{


			paymentMethodCount = 1;
			imgCashCheck.Source = "check";
			imgCreditCheck.Source = "uncheck";
			_slCreditSection.IsVisible = false;

		}
		public void _slCreditCard_Tapped(object sender, EventArgs e)
		{
			paymentMethodCount = 2;
			imgCashCheck.Source = "uncheck";
			imgCreditCheck.Source = "check";
			_slCreditSection.IsVisible = true;
		}
		public void _slSelectAddress_Tapped(object sender, EventArgs e)
		{
			if (countSelectAddress % 2 == 0)
			{
				_slAddress.IsVisible = true;
			}
			else
			{
				_slAddress.IsVisible = false;
			}
			countSelectAddress++;
		}
		public void _slSelectStoreAddress_Tapped(object sender, EventArgs e)
		{
			if (countSelectStoreAddress % 2 == 0)
			{
				_slStoreAddress.IsVisible = true;
			}
			else
			{
				_slStoreAddress.IsVisible = false;
			}
			countSelectStoreAddress++;
		}
		public void _slAddNewAddress_Tapped(object sender, EventArgs e)
		{

			if (addressCount < 2)
			{
				addressCount = addressCount + 1;
				//AddNewAddressPopup ad = new AddNewAddressPopup();
				//Navigation.PushPopupAsync(ad);
				_slEntryView.IsVisible = true;
			}
			else
			{
				StaticMethods.ShowToast("You cannot add more than 3 addresses.");
			}

		}
		public void _slAddress1_Tapped(object sender, EventArgs e)
		{

			addressSelectorCount = 1;
			imgCheck1.Source = "check";
			imgCheck2.Source = "uncheck";
			imgCheck3.Source = "uncheck";

		}
		public void _slAddress2_Tapped(object sender, EventArgs e)
		{
			addressSelectorCount = 2;
			imgCheck2.Source = "check";
			imgCheck1.Source = "uncheck";
			imgCheck3.Source = "uncheck";
		}
		public void _slAddress3_Tapped(object sender, EventArgs e)
		{
			addressSelectorCount = 3;
			imgCheck3.Source = "check";
			imgCheck2.Source = "uncheck";
			imgCheck1.Source = "uncheck";
		}


		public void _slStoreAddress1_Tapped(object sender, EventArgs e)
		{

			StoreaddressSelectorCount = 1;
			imgStoreCheck1.Source = "check";
			imgStoreCheck2.Source = "uncheck";
			imgStoreCheck3.Source = "uncheck";

		}
		public void _slStoreAddress2_Tapped(object sender, EventArgs e)
		{
			StoreaddressSelectorCount = 2;
			imgStoreCheck2.Source = "check";
			imgStoreCheck1.Source = "uncheck";
			imgStoreCheck3.Source = "uncheck";
		}
		public void _slStoreAddress3_Tapped(object sender, EventArgs e)
		{
			StoreaddressSelectorCount = 3;
			imgStoreCheck3.Source = "check";
			imgStoreCheck2.Source = "uncheck";
			imgStoreCheck1.Source = "uncheck";
		}
		private void PrepareUI()
		{
			try
			{
				AddNewAddressPopup.addressCount = 0;
				_slAddress.IsVisible = false;
				_slEntryView.IsVisible = false;
				_slAddress2.IsVisible = false;
				_slAddress3.IsVisible = false;
				_slCreditSection.IsVisible = false;
				_slStoreAddress.IsVisible = false;

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

void ConfirmOrder_Clicked(object sender, System.EventArgs e)
		{
			ProcessOrder();


		}
		private void ProcessOrder()
		{
			try
			{
				SetUserAddress(addressSelectorCount);
				SetStoreAddress(StoreaddressSelectorCount);
				if (paymentMethodCount > 0)
				{
					paymentModel.Payment_By = paymentMethodCount.ToString();
					if (!string.IsNullOrEmpty(paymentModel.order_address))
					{
						Navigation.PushAsync(new PickorDeliveryPage(paymentModel));
					}
					else if (!string.IsNullOrEmpty(paymentModel.order_store_address))
					{
						Navigation.PushAsync(new PickorDeliveryPage(paymentModel));
					}
					else
					{ 
StaticMethods.ShowToast("Something went wrong, Please try again later!");
					}

					}
				else
				{
					StaticMethods.ShowToast("Please select payment method");
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void SetUserAddress(int num)
		{
			try
			{
				switch (num)
				{
					case 1:
						if(!string.IsNullOrEmpty(lblAddress1.Text))
						paymentModel.order_address = lblAddress1.Text;
						break;
					case 2:
						if(!string.IsNullOrEmpty(lblAddress2.Text))
						paymentModel.order_address = lblAddress2.Text;
						break;
					case 3:
						if(!string.IsNullOrEmpty(lblAddress3.Text))
						paymentModel.order_address = lblAddress3.Text;
						break;
				}
			}
			catch (Exception ex)
			{

			}

		}
		private void SetStoreAddress(int num)
		{
			
			try
			{
				switch (num)
				{
					case 1:
						if(!string.IsNullOrEmpty(lblStoreAddress1.Text))
							paymentModel.order_store_address = lblStoreAddress1.Text;
						paymentModel.order_store_lat = latArray[0].ToString();
						paymentModel.order_store_long = longArray[0].ToString();
						break;
					case 2:
						if(!string.IsNullOrEmpty(lblStoreAddress2.Text))
						paymentModel.order_store_address = lblStoreAddress2.Text;
						paymentModel.order_store_lat = latArray[1].ToString();
						paymentModel.order_store_long = longArray[1].ToString();
						break;
					case 3:
						if(!string.IsNullOrEmpty(lblStoreAddress3.Text))
						paymentModel.order_store_address = lblStoreAddress3.Text;
						paymentModel.order_store_lat = latArray[2].ToString();
						paymentModel.order_store_long = longArray[2].ToString();
						break;
				}
			}
			catch (Exception ex)
			{

			}
		}
		private async Task GetCheckoutAddress()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						checkoutModel = WebService.GetCheckOutDetails(_storeId);


					}).ContinueWith(async
				t =>
		{

			if (checkoutModel.responseCode == 200)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
			StoreaddressSelectorCount = 1;
			imgStoreCheck1.Source = "check";
			imgStoreCheck2.Source = "uncheck";
			imgStoreCheck3.Source = "uncheck";

					lblAddress1.Text = checkoutModel.useraddress.user_address;
					imgCheck1.Source = "check";
					_slAddress2.IsVisible = false;
					_slAddress3.IsVisible = false;
					 latArray = checkoutModel.storeaddress.store_latitude.Split('|');
					 longArray=	checkoutModel.storeaddress.store_longitude.Split('|');
					var data = checkoutModel.storeaddress.store_address.Split('|'); 

					if (data != null)
					{
						for (int i = 0; i < data.Length; i++) 
						{
							if (i == 0)
							{
								lblStoreAddress1.Text = data[i].ToString();
								_slStoreAddress1.IsVisible = true;
								paymentModel.order_store_lat = latArray[i].ToString();
									paymentModel.order_store_long = longArray[i].ToString();
							} 
							else if (i == 1)
							{
								lblStoreAddress2.Text = data[i].ToString();
								_slStoreAddress2.IsVisible = true;
							}
							else 
							{
								lblStoreAddress3.Text = data[i].ToString();
								_slStoreAddress3.IsVisible = true;
							}
						}
					}
				});

			}
			else
			{

				StaticMethods.ShowToast(checkoutModel.responseMessage);
			}



			StaticMethods.DismissLoader();

		}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
