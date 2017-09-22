using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class MenuPage : ContentPage
	{
		public StackLayout Home { get; set; }
		public MenuPage()
		{
			InitializeComponent();
			Icon = "icon.png";
			Title = "menu"; // The Title property must be set.



		}
	}
}
