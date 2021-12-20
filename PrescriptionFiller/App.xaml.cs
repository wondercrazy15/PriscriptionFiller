using System;
using PrescriptionFiller.Services;
using PrescriptionFiller.Validations;
using PrescriptionFiller.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrescriptionFiller
{
    public partial class App : Application
    {
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static float ScreenDensity;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IUserDetails, UserDetails>();
            DependencyService.Register<IValidation, Validation>();
            DependencyService.Register<IPrescriptionRecord, PrescriptionRecord>();

            //MainPage = new HomePage();
            MainPage = new LoginView();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
