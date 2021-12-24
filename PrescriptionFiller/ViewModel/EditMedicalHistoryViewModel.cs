using System;
using System.Windows.Input;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class EditMedicalHistoryViewModel : BaseViewModel
    {
        INavigation _navigation;
        UserInfoModel GetInfoModel;
        IUserDetails _userDetails;
        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }

        private string _shots;
        public string Shots
        {
            get { return _shots; }
            set
            {
                _shots = value;
                OnPropertyChanged("shots");
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

        public EditMedicalHistoryViewModel(INavigation navigation, UserInfoModel userInfoModel)
        {
            _userDetails = DependencyService.Get<IUserDetails>();
            _navigation = navigation;
            GetInfoModel = userInfoModel;
            BackCommand = new Command(BackCommandAysnc);
            SaveCommand = new Command(SaveCommandAsync);
            FetchData(GetInfoModel);
        }

        private void FetchData(UserInfoModel getInfoModel)
        {
            Shots = getInfoModel.data.shots;
            Medication = getInfoModel.data.drugs;
            Notes = getInfoModel.data.vaccinations;
        }

        private async void SaveCommandAsync(object obj)
        {
            Data medicalUpdateInfo = new Data();
            medicalUpdateInfo.first_name = GetInfoModel.data.first_name;
            medicalUpdateInfo.last_name = GetInfoModel.data.last_name;
            medicalUpdateInfo.date_of_birth = GetInfoModel.data.date_of_birth;
            medicalUpdateInfo.sex = GetInfoModel.data.sex;
            medicalUpdateInfo.phone_number = GetInfoModel.data.phone_number;
            medicalUpdateInfo.email = GetInfoModel.data.email;
            medicalUpdateInfo.allergies = GetInfoModel.data.allergies;
            medicalUpdateInfo.medical_insurance_provider = GetInfoModel.data.medical_insurance_provider;
            medicalUpdateInfo.carrier_number = GetInfoModel.data.carrier_number;
            medicalUpdateInfo.plan_number = GetInfoModel.data.plan_number;
            medicalUpdateInfo.member_id = GetInfoModel.data.member_id;
            medicalUpdateInfo.issue_number = GetInfoModel.data.issue_number;
            medicalUpdateInfo.personal_health_number = GetInfoModel.data.personal_health_number;
            medicalUpdateInfo.shots = Shots;
            medicalUpdateInfo.drugs = Medication;
            medicalUpdateInfo.vaccinations = Notes;

            var resp = await _userDetails.UpdateUserInfoModel(medicalUpdateInfo);
            await _navigation.PopModalAsync();
            //await _navigation.PushModalAsync(new NavigationPage(new MedicalHistoryView()));
            //_navigation.PopModalAsync();
        }

        private void BackCommandAysnc(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
