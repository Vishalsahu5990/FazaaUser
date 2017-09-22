using System;
using Fazaa;
using Fazaa.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyWebview), typeof(WebviewRenderer))]
namespace Fazaa.iOS
{

	public class WebviewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			this.BackgroundColor = UIColor.Clear;
		}
	}
}