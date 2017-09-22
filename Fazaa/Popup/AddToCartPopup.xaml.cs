using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
namespace Fazaa
{
	public partial class AddToCartPopup : PopupPage
	{
public static event EventHandler<int> ItemSelected;
		public AddToCartPopup()
		{
			InitializeComponent();

			btnCancel.Clicked+= BtnCancel_Clicked;
			btnSave.Clicked+= BtnSave_Clicked;
			txtQuantity.Focused+= TxtQuantity_Focused;
			StaticDataModel.IsFromPopup = true;
		}
protected override async void OnAppearing()
{
	base.OnAppearing(); 


}
protected override void OnDisappearing()
{
			base.OnDisappearing();
			StaticDataModel.IsFromPopup = false;
		}

		void TxtQuantity_Focused(object sender, FocusEventArgs e)
		{
			lblErrorMsg.IsVisible = false;
		}

		async void BtnCancel_Clicked(object sender, EventArgs e)
		{
await Navigation.PopPopupAsync();
		}

		async void BtnSave_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(txtQuantity.Text))
			{
				var val = Convert.ToInt32(txtQuantity.Text);
				 if (val == 0)
				{
					lblErrorMsg.IsVisible = true;
				lblErrorMsg.Text = "Please enter a valid quantity";
				}
			else if (val< 10 )
				{
					ItemSelected(this, Convert.ToInt32(txtQuantity.Text));
					await Navigation.PopPopupAsync();
				} 

				else
				{ 
					lblErrorMsg.IsVisible = true;
				lblErrorMsg.Text = "The Quantity you have entered is greater than the stock";
				}
			}
			else
			{
				lblErrorMsg.IsVisible = true;
				lblErrorMsg.Text = "Please enter a valid quantity";
			}
			}
			catch (Exception ex)
			{

			}


		}
	}
}
