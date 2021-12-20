using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class EditAccountInfoView : ContentPage
    {
        public EditAccountInfoView(UserInfoModel userInfoModel)
        {
            InitializeComponent();
            BindingContext = new EditAccountInfoViewModel(Navigation, userInfoModel);
        }
    }
}
