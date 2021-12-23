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
        PrescriptionItem _selectedNewPrescriptionInfo = new PrescriptionItem();

        public ICommand BackCommand { get; }
        public ICommand SendToPharmacyCommand { get; }

        private string _NewCapturedImageSource;
        public string NewCapturedImageSource
        {
            get
            {
                return _NewCapturedImageSource;
            }
            set
            {
                _NewCapturedImageSource = value;
                OnPropertyChanged("NewCapturedImageSource");
            }
        }

        private string _medicalNotesTxt;
        public string medicalNotesTxt
        {
            get
            {
                return _medicalNotesTxt;
            }
            set
            {
                _medicalNotesTxt = value;
                OnPropertyChanged("medicalNotesTxt");
            }
        }

        private string _prescriptionDescriptionTxt;
        public string prescriptionDescriptionTxt
        {
            get
            {
                return _prescriptionDescriptionTxt;
            }
            set
            {
                _prescriptionDescriptionTxt = value;
                OnPropertyChanged("prescriptionDescriptionTxt");
            }
        }

        //NewCapturedImageSource

        public AddNewPrescriptionViewModel(INavigation navigation, PrescriptionItem SelctedNewPrescriptionInfo)
        {
            _navigation = navigation;
            _selectedNewPrescriptionInfo = SelctedNewPrescriptionInfo;
            NewCapturedImageSource = _selectedNewPrescriptionInfo.thumbPath;
            BackCommand = new Command(BackCommandAsync);
            SendToPharmacyCommand = new Command(SendToPharmacyClick);
        }

        private void SendToPharmacyClick(object obj)
        {
            //_selectedNewPrescriptionInfo.med
            _navigation.PushModalAsync(new NavigationPage(new AddParmacyView(_selectedNewPrescriptionInfo, medicalNotesTxt, prescriptionDescriptionTxt)));
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
