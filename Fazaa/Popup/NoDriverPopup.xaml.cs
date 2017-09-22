using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class NoDriverPopup : PopupPage
	{
public static event EventHandler<int> PopupClosed;
		public NoDriverPopup()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnClose.Clicked+= BtnClose_Clicked;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnClose.Clicked-= BtnClose_Clicked;
		}

		async void BtnClose_Clicked(object sender, EventArgs e)
		{
			PopupClosed(this, 1);
            await Navigation.PopPopupAsync();
		}
	}
}
