using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace Fazaa
{
	public partial class EditAddressPopup : PopupPage 
	{
		public EditAddressPopup()
		{
			InitializeComponent();
		}
		public EditAddressPopup(string address)
		{
			InitializeComponent();
			txtAddress.Text = address;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnEditAddress.Clicked+= BtnEditAddress_Clicked;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnEditAddress.Clicked -= BtnEditAddress_Clicked;
		}

		void BtnEditAddress_Clicked(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(txtAddress.Text))
			{
				UpdateAddress().Wait();
			}
			else
			{
				txtAddress.PlaceholderColor = Color.Red;
			}
		}
		private async Task UpdateAddress()
		{
			string ret = string.Empty;

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
				ret = WebService.UpdateAddress(txtAddress.Text);
				}).ContinueWith(async
					t =>
					{
						if (ret.Equals("success"))
						{
							Device.BeginInvokeOnMainThread(() =>
							{
						MessagingCenter.Send<object,string>(this, "NotifyUpdateAddress",txtAddress.Text);

								Navigation.PopPopupAsync();
							});
						}
						else
						{
							StaticMethods.ShowToast("Unable to save details, please try again later!");
						}





						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
	}
}
