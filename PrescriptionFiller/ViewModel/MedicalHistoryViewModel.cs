using System;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views;
//using PrescriptionFiller.Global;
using Xamarin.Forms;
//using static Android.Provider.SyncStateContract;

namespace PrescriptionFiller.ViewModel
{
    public class MedicalHistoryViewModel : BaseViewModel
    {
        INavigation _navigation;
        UserInfoModel userInfoModel;
        private IUserDetails _userDetails;
        public ICommand BackCommand { get; }
        public ICommand EditAccountInfoCommand { get; }

        private string _shots;
        public string Shots
        {
            get { return _shots; }
            set
            {
                _shots = value;
                OnPropertyChanged("Shots");
            }
        }
        private string _medication;
        public string Medication
        {
            get { return _medication; }
            set
            {
                _medication = value;
                OnPropertyChanged("Medication");
            }
        }
        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public MedicalHistoryViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _userDetails = DependencyService.Get<IUserDetails>();
            GetMedicalInfo();
            EditAccountInfoCommand = new Command(EditAccountInfoCommandAsync);
            BackCommand = new Command(BackCommandAsync);
        }

        private async void GetMedicalInfo()
        {
            var result = await _userDetails.GetUserInfo();
            ///var res = loginViewModel.userInfoResponse;
            if (result != null)
            {
                Constants.user_id = result.data.id.ToString();
                //Global.Constants.user_id = result.data.id.ToString();
                userInfoModel = result;
                Shots = result.data.shots;
                Medication = result.data.drugs;
                Notes = result.data.vaccinations;
            }
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }

        private void EditAccountInfoCommandAsync(object obj)
        {
            _navigation.PushModalAsync(new NavigationPage(new EditMedicalHistoryView(userInfoModel)));
        }
    }
}
