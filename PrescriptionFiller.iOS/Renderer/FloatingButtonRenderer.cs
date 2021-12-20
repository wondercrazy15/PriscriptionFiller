using System;
using System.ComponentModel;
using CoreGraphics;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FloatingButton), typeof(FloatingButtonRenderer))]
namespace PrescriptionFiller.iOS.Renderer
{
    public class FloatingButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            try
            {
                Control.ContentEdgeInsets = new UIKit.UIEdgeInsets(0, 10, 0, 0);
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }

    public class CircleView : UIView
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext g = UIGraphics.GetCurrentContext())
            {
                UIColor.Blue.SetFill();
                UIColor.Red.SetStroke();

                g.AddEllipseInRect(rect);
                g.FillPath();
            }
        }

    }
}
