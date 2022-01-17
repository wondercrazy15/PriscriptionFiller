using System;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace PrescriptionFiller.Droid.Renderer
{
    [Obsolete]
    public class CustomDatePickerRenderer :  DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            CustomDatePicker datePicker = (CustomDatePicker)Element;

            if (datePicker != null)
            {
                SetTextColor(datePicker);
            }

            if (e.OldElement == null)
            {
                //Wire events
            }

            if (e.NewElement == null)
            {
                //Unwire events
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            CustomDatePicker datePicker = (CustomDatePicker)Element;

            if (e.PropertyName == CustomDatePicker.TextColorProperty.PropertyName)
            {
                //this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
                this.Control.SetTextColor(Android.Graphics.Color.Black);
            }
        }

        void SetTextColor(CustomDatePicker datePicker)
        {
            //this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
            this.Control.SetTextColor(Android.Graphics.Color.Black);
        }
    }
}
