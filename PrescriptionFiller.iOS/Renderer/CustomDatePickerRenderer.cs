using System;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]

namespace PrescriptionFiller.iOS.Renderer
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                DatePicker dpicker = (e.NewElement == null) ? e.OldElement : e.NewElement;
                Control.TextColor = (dpicker as CustomDatePicker).TextColor.ToUIColor();
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
