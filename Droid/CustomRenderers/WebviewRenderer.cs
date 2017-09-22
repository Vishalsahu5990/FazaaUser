using System;
using Fazaa;
using Fazaa.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyWebview), typeof(WebviewRenderer))]
namespace Fazaa.Droid
{
	public class WebviewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
		{ base.OnElementChanged(e); this.Control.SetBackgroundColor(global:: Android.Graphics.Color.Transparent); 
		}
	}
}



