using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
        }

        void buttonNew_Clicked(System.Object sender, System.EventArgs e)
        {
            newView1.IsVisible = true;
            sentView1.IsVisible = false;
        }

        void buttonSent_Clicked(System.Object sender, System.EventArgs e)
        {
            newView1.IsVisible = false;
            sentView1.IsVisible = true;
        }
        //private void SentInfoCommand(object sender, EventArgs e)
        //{
        //    ((CollectionView)sender).SelectedItem = null;
        //}
    }
}
