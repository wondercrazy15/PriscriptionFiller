using System;
using Android.Graphics.Drawables;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace PrescriptionFiller.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        [Obsolete]
        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();

                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
            }
        }
    }
}
