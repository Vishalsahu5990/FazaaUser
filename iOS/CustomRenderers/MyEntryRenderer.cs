using System;
using CoreGraphics;
using Fazaa;
using Fazaa.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace Fazaa.iOS
{
	public class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// do whatever you want to the UITextField here!
				Control.BackgroundColor = UIColor.White;
				Control.BorderStyle = UITextBorderStyle.None;
				if (StaticDataModel.IsFromPopup)
				{
					Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
					Control.LeftViewMode = UITextFieldViewMode.Always;
				}
			}
		}
	}
}
