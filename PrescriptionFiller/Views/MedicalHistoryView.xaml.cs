using System;
using System.Collections.Generic;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class MedicalHistoryView : ContentPage
    {
        public MedicalHistoryViewModel viewModel;
        public MedicalHistoryView()
        {
            InitializeComponent();
            BindingContext = new MedicalHistoryViewModel(Navigation);
            viewModel = this.BindingContext as MedicalHistoryViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetMedicalInfo();
        }
    }
}
