using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class EditMedicalHistoryView : ContentPage
    {
        public EditMedicalHistoryView(UserInfoModel userInfoModel)
        {
            InitializeComponent();
            BindingContext = new EditMedicalHistoryViewModel(Navigation, userInfoModel);
        }
    }
}
