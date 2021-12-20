using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class AddParmacyView : ContentPage
    {
        public AddParmacyView()
        {
            InitializeComponent();
            BindingContext = new AddPharmacyDetailViewModel(Navigation);
        }
    }
}
