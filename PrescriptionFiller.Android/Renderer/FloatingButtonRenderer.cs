﻿using System;
using System.ComponentModel;
using Android.Graphics.Drawables;
using PrescriptionFiller.CustomControls;
using PrescriptionFiller.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FloatingButton), typeof(FloatingButtonRenderer))]
namespace PrescriptionFiller.Droid.Renderer
{
	[Obsolete]
	public class FloatingButtonRenderer : ButtonRenderer
	{
		private GradientDrawable _normal, _pressed;

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				var button = e.NewElement;

				// Create a drawable for the button's normal state
				_normal = new Android.Graphics.Drawables.GradientDrawable();

				if (button.BackgroundColor.R == -1.0 && button.BackgroundColor.G == -1.0 && button.BackgroundColor.B == -1.0)
					_normal.SetColor(Android.Graphics.Color.ParseColor("#ff2c2e2f"));
				else
					_normal.SetColor(button.BackgroundColor.ToAndroid());

				_normal.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
				_normal.SetCornerRadius(button.BorderRadius);

				// Create a drawable for the button's pressed state
				_pressed = new Android.Graphics.Drawables.GradientDrawable();
				var highlight = Context.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ColorActivatedHighlight }).GetColor(0, Android.Graphics.Color.Gray);
				_pressed.SetColor(highlight);
				_pressed.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
				_pressed.SetCornerRadius(button.BorderRadius);

				// Add the drawables to a state list and assign the state list to the button
				var sld = new StateListDrawable();
				sld.AddState(new int[] { Android.Resource.Attribute.StatePressed }, _pressed);
				sld.AddState(new int[] { }, _normal);
				Control.SetBackgroundDrawable(sld);

				Control.SetTypeface(Android.Graphics.Typeface.Create("HelveticaNeue", Android.Graphics.TypefaceStyle.Normal), Android.Graphics.TypefaceStyle.Normal);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			var button = (Xamarin.Forms.Button)sender;

			if (_normal != null && _pressed != null)
			{
				if (e.PropertyName == "BorderRadius")
				{
					_normal.SetCornerRadius(button.BorderRadius);
					_pressed.SetCornerRadius(button.BorderRadius);
				}
				if (e.PropertyName == "BorderWidth" || e.PropertyName == "BorderColor")
				{
					_normal.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
					_pressed.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
				}
			}

			var view = (FloatingButton)this.Element;
			var nativeButton = (global::Android.Widget.Button)this.Control;
			nativeButton.SetPadding((int)view.PaddingLeft, (int)view.PaddingTop, (int)view.PaddingRight, (int)view.PaddingBottom);
		}
	}
}
