using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace Fazaa
{
	public partial class EditCardDetailsPopup : PopupPage
	{
		CardDetailsModel.UserCarddata refUserCardData = null;
		public EditCardDetailsPopup(CardDetailsModel.UserCarddata cardDetails)
		{
			InitializeComponent();
			refUserCardData = cardDetails;
			SetData();
		}
		public void month_Tapped(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() => 
			{
				pickerMonth.Focus();
			});
		}
		public void year_Tapped(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				pickerYear.Focus();
			});
		}

		void PickerMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = pickerMonth.Items[pickerMonth.SelectedIndex];
			lblMonth.Text = item;
			lblMonth.TextColor = Color.Black;
		}

		void PickerYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = pickerYear.Items[pickerYear.SelectedIndex];
			lblYear.Text = item;
			lblYear.TextColor = Color.Black;
		}

		void BtnEditCard_Clicked(object sender, EventArgs e)
		{
			if(IsValidate())
			{
				UpdateCardDetails().Wait();
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			pickerMonth.SelectedIndexChanged+= PickerMonth_SelectedIndexChanged;
			pickerYear.SelectedIndexChanged+= PickerYear_SelectedIndexChanged;
			btnEditCard.Clicked+= BtnEditCard_Clicked;
		
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			pickerMonth.SelectedIndexChanged -= PickerMonth_SelectedIndexChanged;
			pickerYear.SelectedIndexChanged -= PickerYear_SelectedIndexChanged;
			btnEditCard.Clicked -= BtnEditCard_Clicked;
		}
		void SetData()
		{
			try
			{
				for (int i = 1; i <= 12; i++)
					pickerMonth.Items.Add(i.ToString());

				for (int i = 2017; i <= 2030; i++)
					pickerYear.Items.Add(i.ToString());

				lblCardType.Text = refUserCardData.Card_Type;
				lblCardNumber.Text = refUserCardData.Card_Number;
				lblSecurityCode.Text = refUserCardData.Security_Code;
				lblCardHolderName.Text = refUserCardData.Card_Holder_Name;

				if(!string.IsNullOrEmpty(refUserCardData.Card_Expairy))
				{
					var array = refUserCardData.Card_Expairy.Split('/');
					var month = array[0];
					var year = array[1];
					lblMonth.Text = month;
					lblYear.Text = year;
					lblMonth.TextColor = Color.Black;
					lblYear.TextColor = Color.Black;
				}


			}
			catch (Exception ex)
			{

			}
		}
		bool IsValidate()
		{
			if(string.IsNullOrEmpty(lblCardType.Text))
			{
				lblCardType.PlaceholderColor = Color.Red;
			return false;	
			}
			else if (string.IsNullOrEmpty(lblCardHolderName.Text))
			{
				lblCardHolderName.PlaceholderColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(lblCardNumber.Text))
			{
				lblCardNumber.PlaceholderColor = Color.Red;
				return false;
			}
			else if (lblMonth.Text.Equals("Month"))
			{
				lblMonth.TextColor = Color.Red;
				return false;
			}
			else if (lblYear.Text.Equals("Year"))
			{
				lblYear.TextColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(lblSecurityCode.Text))
			{
				lblSecurityCode.PlaceholderColor = Color.Red;
				return false;
			}
			else
			{
				return true;
			}
			return false;

		}
		private async Task UpdateCardDetails()
		{
			string ret = string.Empty;

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

				CardDetailsModel.UserCarddata objCardDetails = new CardDetailsModel.UserCarddata();
						objCardDetails.Card_Holder_Name = lblCardHolderName.Text;
				objCardDetails.Card_Number = lblCardNumber.Text;
				objCardDetails.Security_Code = lblSecurityCode.Text;
				objCardDetails.Card_Expairy = lblMonth.Text + "/" + lblYear.Text;
						objCardDetails.Card_Type = lblCardType.Text;
				ret = WebService.UpdateCardDetails(objCardDetails);


					}).ContinueWith(async
					t =>
					{
				if (ret.Equals("success"))
						{
							Device.BeginInvokeOnMainThread(() =>
							{
						MessagingCenter.Send<object>(this, "NotifyUpdateCardDetails");

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
