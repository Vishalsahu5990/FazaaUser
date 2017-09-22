using System;
using System.Drawing;
using Fazaa;
using Fazaa.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace Fazaa.iOS
{
	public class CustomButtonRenderer : ButtonRenderer
	{
		UIButton btn;
		public CustomButtonRenderer()
		{

		}
		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
			//CAGradientLayer btnGradient = new CAGradientLayer();
			//btnGradient.Frame = btn.Bounds;
			//btnGradient.Colors = new CGColor[] { Color.White.ToCGColor(), Color.FromHex("#0073BD").ToCGColor() };
			//btn.Layer.InsertSublayer(btnGradient, 0);
			//btn.Layer.MasksToBounds = true;
			//btn.Layer.BorderColor = Color.FromHex("#0073BE").ToCGColor();
			//btn.Layer.BorderWidth = 1;
			//btn.SetTitleColor(Color.Black.ToUIColor(), UIControlState.Normal);
		}


		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				btn = (UIButton)Control;
			}
		}
	}
}