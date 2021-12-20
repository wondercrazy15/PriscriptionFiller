using System;
using System.Windows.Input;
using PrescriptionFiller.Model;
using PrescriptionFiller.Views;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class AddNewPrescriptionViewModel : BaseViewModel
    {
        INavigation _navigation;
        CameraModel _selectedNewPrescriptionInfo = new CameraModel();

        public ICommand BackCommand { get; }
        public ICommand SendToPharmacyCommand { get; }

        public AddNewPrescriptionViewModel(INavigation navigation,CameraModel SelctedNewPrescriptionInfo)
        {
            _navigation = navigation;
            _selectedNewPrescriptionInfo = SelctedNewPrescriptionInfo;

            BackCommand = new Command(BackCommandAsync);
            SendToPharmacyCommand = new Command(SendToPharmacyClick);
        }

        private void SendToPharmacyClick(object obj)
        {
            _navigation.PushModalAsync(new NavigationPage(new AddParmacyView()));
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
