using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class AboutUsPage : ContentPage
	{
		public AboutUsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			if (StaticMethods.DeviceType() == "android")
			{
				_slTitleBar.HeightRequest = 60;
				lblTitle.Margin = new Thickness(0, 10, 0, 0);
			}
		} 
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
	}
}
