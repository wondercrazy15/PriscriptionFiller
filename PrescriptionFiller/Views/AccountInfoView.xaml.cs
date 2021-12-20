using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class AccountInfoView : ContentPage
    {
         public AccountInfoViewModel viewModel;
        public AccountInfoView()
        {
            InitializeComponent();           
            BindingContext = new AccountInfoViewModel(Navigation);
            viewModel = this.BindingContext as AccountInfoViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.getUserInfo();
        }
        //public AccountInfoView()
        //{
        //    InitializeComponent();
        //    BindingContext = new AccountInfoViewModel(Navigation);
        //}

        ////async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        ////{
        ////    await Navigation.PushAsync(new EditAccountInfoView());
        ////}
    }
}
