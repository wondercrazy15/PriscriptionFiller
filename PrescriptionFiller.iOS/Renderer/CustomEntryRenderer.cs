using System;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly : ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace PrescriptionFiller.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}
