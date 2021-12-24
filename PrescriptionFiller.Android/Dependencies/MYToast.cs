using System;
using Android.Views;
using Android.Widget;
using PrescriptionFiller.Droid.Dependencies;
using PrescriptionFiller.interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MYToast))]
namespace PrescriptionFiller.Droid.Dependencies
{
    public class MYToast : MyToast
    {
        [Obsolete]
        public void Display(string message, bool success)
        {
            Toast toast = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            Android.Graphics.Drawables.GradientDrawable drawable = new Android.Graphics.Drawables.GradientDrawable();
            drawable.SetCornerRadius(50);
            toast.View.SetBackgroundDrawable(drawable);
            if (success)
                toast.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#02b85a"));
            else
                toast.View.SetBackgroundColor(Android.Graphics.Color.ParseColor("#fc0505"));
            //toast.SetGravity(GravityFlags.DisplayClipHorizontal, 0, 100);
            toast.View.SetPadding(10, 10, 10, 10);
            toast.Show();
        }
    }
}
