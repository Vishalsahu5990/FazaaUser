using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class AddNewAddressPopup : PopupPage
	{
public static event EventHandler<string> AddressAdded; 
		public static int addressCount = 0;
       

		public AddNewAddressPopup() 
		{
			InitializeComponent();
			btnSave.Clicked+= BtnSave_Clicked;
			btnCancel.Clicked+= BtnCancel_Clicked;
			txtAddress.Focused+= TxtAddress_Focused;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnSave.Clicked+= BtnSave_Clicked;
			btnCancel.Clicked+= BtnCancel_Clicked;
			txtAddress.Focused+= TxtAddress_Focused;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnSave.Clicked-= BtnSave_Clicked;
			btnCancel.Clicked-= BtnCancel_Clicked;
			txtAddress.Focused-= TxtAddress_Focused;
		}
void TxtAddress_Focused(object sender, FocusEventArgs e)
		{
			lblErrorMsg.IsVisible = false;
		}
		void BtnSave_Clicked(object sender, EventArgs e)
		{
				if (!string.IsNullOrEmpty(txtAddress.Text))
				{
				

var handler = AddressAdded;
					if (handler != null)
						handler(this, txtAddress.Text);

Navigation.PopPopupAsync();
   
					
				}
				else 
				{
					lblErrorMsg.Text = "Invalid Address";
					lblErrorMsg.IsVisible = true;
				}

		}

async	void BtnCancel_Clicked(object sender, EventArgs e)
		{
await Navigation.PopPopupAsync();
		}


	}
}
