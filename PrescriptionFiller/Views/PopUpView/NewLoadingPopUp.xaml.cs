using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrescriptionFiller.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrescriptionFiller.Views.PopUpView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewLoadingPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        private static NewLoadingPopUp Instance;
        private Button BG1, BG2, BG3;
        private FloatingButton BG;

        public bool Closing
        {
            get;
            set;
        }

        public NewLoadingPopUp()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                poppage.BackgroundColor = Color.Black;
                poppage.Opacity = 0.8;
            }
            MainLayout.OnLayoutChildren += MainLayout_OnLayoutChildren;

            InitButton(BG1 = new Button());
            InitButton(BG2 = new Button());
            InitButton(BG3 = new Button());
            InitButton(BG = new FloatingButton());
        }

        private void InitButton(Button b)
        {
            b.BackgroundColor = Color.White;
            b.TextColor = Color.Transparent;
            b.HorizontalOptions = LayoutOptions.CenterAndExpand;
            b.VerticalOptions = LayoutOptions.CenterAndExpand;
            b.WidthRequest = b.HeightRequest = 48;

            if (Device.RuntimePlatform == Device.iOS)
            {
                b.CornerRadius = 24;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                b.CornerRadius = 256;
            }
            else
            {
                b.CornerRadius = 0;
            }

            if (b is FloatingButton)
            {
                FloatingButton fb = (FloatingButton)b;
                fb.PaddingLeft = 24;
                fb.ImageSource = (FileImageSource)FileImageSource.FromFile("pfa_icon_loader");
                fb.Command = new Command(delegate ()
                {
                    Navigation.PopModalAsync();
                });
            }

            MainLayout.Children.Add(b);
            float pageWidth = App.ScreenWidth / App.ScreenDensity;
            float pageHeight = App.ScreenHeight / App.ScreenDensity;
            b.Layout(new Rectangle((pageWidth - b.WidthRequest) / 2f, (pageHeight - b.HeightRequest) / 2f, b.WidthRequest, b.HeightRequest));
        }

        private void MainLayout_OnLayoutChildren(double x, double y, double width, double height)
        {

        }

        public async Task AnimateAsync()
        {
            uint length = 1000;

            while (Closing == false)
            {
                await Task.WhenAll(
                   BG.RotateTo(360, length * 2, Easing.CubicInOut),
                   BG1.ScaleTo(1.8, length, Easing.Linear),
                   BG2.ScaleTo(1.8, (uint)(length * 1.5), Easing.Linear),
                   BG3.ScaleTo(1.8, length * 2, Easing.Linear),
                   BG1.FadeTo(0, length, Easing.Linear),
                   BG2.FadeTo(0, (uint)(length * 1.5), Easing.Linear),
                   BG3.FadeTo(0, length * 2, Easing.Linear));
                BG1.Scale = BG2.Scale = BG3.Scale = 1;
                BG1.Opacity = BG2.Opacity = BG3.Opacity = 1;
                BG.Rotation = 0;
            }
        }



        public static void Show(INavigation nav)
        {
            if (Instance == null)
            {
                Instance = new NewLoadingPopUp();

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        nav.PushModalAsync(Instance);

                        Task task = Instance.AnimateAsync();
                    });
                })).Start();
            }
        }

        public static async Task Dismiss(INavigation nav)
        {
            try
            {
                if (Instance != null)
                {
                    Instance.Closing = true;

                    try
                    {
                        await nav.PopModalAsync();
                    }
                    catch (Exception e)
                    {
                    };

                    Instance = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}