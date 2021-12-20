using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class MedicalHistoryView : ContentPage
    {
        public MedicalHistoryView()
        {
            InitializeComponent();
            BindingContext = new MedicalHistoryViewModel(Navigation);
        }
    }
}
