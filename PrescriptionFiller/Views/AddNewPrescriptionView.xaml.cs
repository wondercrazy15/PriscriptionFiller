using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class AddNewPrescriptionView : ContentPage
    {
        public AddNewPrescriptionView(PrescriptionItem SelctedNewPrescriptionInfo)
        {
            InitializeComponent();
            BindingContext = new AddNewPrescriptionViewModel(Navigation, SelctedNewPrescriptionInfo);
        }
    }
}
