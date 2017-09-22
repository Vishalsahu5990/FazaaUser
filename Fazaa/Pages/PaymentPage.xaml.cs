using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
namespace Fazaa
{
	public partial class PaymentPage : ContentPage
	{
		CardDetailsModel.UserCarddata refUserCardData = null;
		public PaymentPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		public void editPaymentInfo_Tapped(object sender, EventArgs e)
		{
			Navigation.PushPopupAsync(new EditCardDetailsPopup(refUserCardData));
		}
		public async void deletePaymentInfo_Tapped(object sender, EventArgs e)
		{
			var result = await DisplayAlert("Alert", "Are you confirm to delete this card?","Yes" ,"No");
			if(result)
			DeleteCard().Wait();
		}
		public async void editAddress_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushPopupAsync(new EditAddressPopup(lblAddress.Text));
		}
		public async void deleteAddressInfo_Tapped(object sender, EventArgs e)
		{
			var result = await DisplayAlert("Alert", "Are you confirm to delete this address?", "Yes", "No");
			if (result)
			DeleteAddress().Wait();
		}

		void BtnAddNewCard_Clicked(object sender, EventArgs e)
		{
			Navigation.PushPopupAsync(new EditCardDetailsPopup(refUserCardData));
		}

		void BtnAddNewAddress_Clicked(object sender, EventArgs e)
		{
			Navigation.PushPopupAsync(new EditAddressPopup(string.Empty));
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
			btnAddNewCard.Clicked+= BtnAddNewCard_Clicked;
			btnAddNewAddress.Clicked+= BtnAddNewAddress_Clicked; 
			GetPaymentDetails().Wait();
			MessagingCenter.Subscribe<object>(this, "NotifyUpdateCardDetails", (sender) => {
				GetPaymentDetails().Wait();
			});
			MessagingCenter.Subscribe<object,string>(this, "NotifyUpdateAddress", (sender,address) => {
				Device.BeginInvokeOnMainThread(() =>
				{
					lblAddress.Text = address;
					slAddressEditDelete.IsVisible = true;
					btnAddNewAddress.IsVisible = false;
				});
			});

		}
		private void PrepareUI()
		{
			try
			{

				if (StaticMethods.DeviceType() == "android")
				{
					_slTitleBar.HeightRequest = 60;

				}
			
			}
			catch (Exception ex)
			{

			}
		}
		private async Task GetPaymentDetails()
		{
			
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				refUserCardData = WebService.GetCardDetails();


					}).ContinueWith(async
					t =>
					{
				if (!ReferenceEquals(refUserCardData,null))
				{
					Device.BeginInvokeOnMainThread(() => 
					{
						if(!string.IsNullOrEmpty(refUserCardData.Card_Number))	
						{
							slPaymentInfoEditDelete.IsVisible = true;
							slCardInfo.IsVisible = true;
						    btnAddNewCard.IsVisible = false;
							lblCardType.Text = refUserCardData.Card_Type;
							lblCardNumber.Text = refUserCardData.Card_Number;
							lblSecurityCode.Text = refUserCardData.Security_Code;
							lblCardHolderName.Text = refUserCardData.Card_Holder_Name;
							lblExpirationDate.Text = refUserCardData.Card_Expairy;
							lblAddress.Text = refUserCardData.Address;
						}
						else
						{
							slPaymentInfoEditDelete.IsVisible = false;
							slCardInfo.IsVisible = false;
							btnAddNewCard.IsVisible = true;
						}
						if (!string.IsNullOrEmpty(refUserCardData.Address))
						{
							slAddressEditDelete.IsVisible = true;
							btnAddNewAddress.IsVisible = false;
							lblAddress.Text = refUserCardData.Address;
						}
						else
						{
							btnAddNewAddress.IsVisible = true;
							slAddressEditDelete.IsVisible = false;
						}
					});
				}
				else
				{
					StaticMethods.ShowToast("Unable to get details, please try again later!");
				}
				StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
		private async Task DeleteCard()
		{
			string ret = string.Empty;

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						CardDetailsModel.UserCarddata objCardDetails = new CardDetailsModel.UserCarddata();
				objCardDetails.Card_Holder_Name = string.Empty;
						objCardDetails.Card_Number = string.Empty;
				     objCardDetails.Card_Type = string.Empty;
						objCardDetails.Security_Code = string.Empty;
						objCardDetails.Card_Expairy = string.Empty;
						ret = WebService.UpdateCardDetails(objCardDetails);


					}).ContinueWith(async
					t =>
					{
						if (ret.Equals("success"))
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								slPaymentInfoEditDelete.IsVisible = false;
								slCardInfo.IsVisible = false;
								btnAddNewCard.IsVisible = true;
							});
						}
						else
						{
							StaticMethods.ShowToast("Unable to delete card, please try again later!");
						}





						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
		private async Task DeleteAddress()
		{
			string ret = string.Empty;

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						CardDetailsModel.UserCarddata objCardDetails = new CardDetailsModel.UserCarddata();
						
				       
				     ret = WebService.UpdateAddress(string.Empty);


					}).ContinueWith(async
					t =>
					{
						if (ret.Equals("success"))
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								btnAddNewAddress.IsVisible = true;
								slAddressEditDelete.IsVisible = false;
								lblAddress.Text = string.Empty;
							});
						}
						else
						{
							StaticMethods.ShowToast("Unable to delete address, please try again later!");
						}





						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}

	}
}
