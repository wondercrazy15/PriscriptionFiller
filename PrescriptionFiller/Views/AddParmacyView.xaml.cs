using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class AddParmacyView : ContentPage
    {
        public AddParmacyView(PrescriptionItem _selectedNewPrescriptionInfo, string medicalNotesTxt, string prescriptionDescriptionTxt)
        {
            InitializeComponent();
            BindingContext = new AddPharmacyDetailViewModel(Navigation, _selectedNewPrescriptionInfo, medicalNotesTxt, prescriptionDescriptionTxt);
        }
    }
}
