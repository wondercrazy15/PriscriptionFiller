using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginViewModel ViewModel;
        public LoginView()
        {
            InitializeComponent();
            BindingContext = ViewModel = new LoginViewModel(Navigation);
        }

        void EmailEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
           bool IsVaild = ViewModel.CheckEmail(e.NewTextValue);
            if (IsVaild || string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                EmailBottomLine.BackgroundColor = Color.Blue;
            }
            else
            {
                EmailBottomLine.BackgroundColor = Color.Red;
            }
        }

        void PasswordEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            bool IsValid = ViewModel.CheckPassword(e.NewTextValue);
            if (IsValid || string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                PasswordBottomLine.BackgroundColor = Color.Blue;
            }
            else
            {
                PasswordBottomLine.BackgroundColor = Color.Red;
            }
        }
    }
}
