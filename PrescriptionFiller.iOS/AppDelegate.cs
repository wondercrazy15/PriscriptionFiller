﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace PrescriptionFiller.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            Forms.SetFlags("RadioButton_Experimental");
            global::Xamarin.Forms.Forms.Init();
            App.ScreenWidth = (int)(UIScreen.MainScreen.Bounds.Width * UIScreen.MainScreen.Scale);
            App.ScreenHeight = (int)(UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale);
            App.ScreenDensity = (float)UIScreen.MainScreen.Scale;
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
