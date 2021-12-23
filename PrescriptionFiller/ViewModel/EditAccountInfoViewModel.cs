using System;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class EditAccountInfoViewModel : BaseViewModel
    {
        public INavigation _navigation;
        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }
        UserInfoModel GetInfoModel;
        private IUserDetails _userDetails;

        private string _firstName;
        public string firstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("firstName");
            }
        }
        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("lastName");
            }
        }
        private string _dob;
        public string dob
        {
            get { return _dob; }
            set
            {
                _dob = value;
                OnPropertyChanged("dob");
            }
        }
        private string _gender;
        public string gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("gender");
            }
        }
        private string _phone;
        public string phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("phone");
            }
        }
        private string _emailAdress;
        public string emailAdress
        {
            get { return _emailAdress; }
            set
            {
                _emailAdress = value;
                OnPropertyChanged("emailAdress");
            }
        }
        private string _allerigies;
        public string allerigies
        {
            get { return _allerigies; }
            set
            {
                _allerigies = value;
                OnPropertyChanged("allerigies");
            }
        }
        private string _medicalInsuranceProvider;
        public string medicalInsuranceProvider
        {
            get { return _medicalInsuranceProvider; }
            set
            {
                _medicalInsuranceProvider = value;
                OnPropertyChanged("medicalInsuranceProvider");
            }
        }
        private string _carrier;
        public string carrier
        {
            get { return _carrier; }
            set
            {
                _carrier = value;
                OnPropertyChanged("carrier");
            }
        }
        private string _plan;
        public string plan
        {
            get { return _plan; }
            set
            {
                _plan = value;
                OnPropertyChanged("plan");
            }
        }
        private string _memberId;
        public string memberId
        {
            get { return _memberId; }
            set
            {
                _memberId = value;
                OnPropertyChanged("memberId");
            }
        }
        private string _issue;
        public string issue
        {
            get { return _issue; }
            set
            {
                _issue = value;
                OnPropertyChanged("issue");
            }
        }
        private string _personalHeathNumber;
        public string personalHeathNumber
        {
            get { return _personalHeathNumber; }
            set
            {
                _personalHeathNumber = value;
                OnPropertyChanged("personalHeathNumber");
            }
        }
        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("password");
            }
        }
        public EditAccountInfoViewModel(INavigation navigation, UserInfoModel userInfoModel)
        {
            GetInfoModel = userInfoModel;
            _navigation = navigation;
            _userDetails = DependencyService.Get<IUserDetails>();
            password = Constants.user_password;
            BackCommand = new Command(BackCommandAsync);
            SaveCommand = new Command(SaveCommandAsync);
            FecthData(userInfoModel);
        }

        private void FecthData(UserInfoModel userInfoModel)
        {
            firstName = userInfoModel.data.first_name;
            lastName = userInfoModel.data.last_name;
            dob = userInfoModel.data.date_of_birth;
            gender = (userInfoModel.data.sex).ToString();
            phone = userInfoModel.data.phone_number;
            emailAdress = userInfoModel.data.email;
            allerigies = userInfoModel.data.allergies;
            medicalInsuranceProvider = userInfoModel.data.medical_insurance_provider;
            carrier = userInfoModel.data.carrier_number;
            plan = userInfoModel.data.plan_number;
            memberId = userInfoModel.data.member_id;
            issue = userInfoModel.data.issue_number;
            personalHeathNumber = userInfoModel.data.personal_health_number;
        }

        private async void SaveCommandAsync()
        {
            Data userUpdateInfo = new Data();
            userUpdateInfo.first_name = firstName;
            userUpdateInfo.last_name = lastName;
            userUpdateInfo.date_of_birth = dob;
            userUpdateInfo.sex = gender;
            userUpdateInfo.phone_number = phone;
            userUpdateInfo.email = emailAdress;
            userUpdateInfo.allergies = allerigies;
            userUpdateInfo.medical_insurance_provider = medicalInsuranceProvider;
            userUpdateInfo.carrier_number = carrier;
            userUpdateInfo.plan_number = plan;
            userUpdateInfo.member_id = memberId;
            userUpdateInfo.issue_number = issue;
            userUpdateInfo.personal_health_number = personalHeathNumber;

            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var updatedUserInfo = await _userDetails.UpdateUserInfoModel(userUpdateInfo);
                if (updatedUserInfo != null)
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                    await _navigation.PopModalAsync();
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
            });

            //var updatedUserInfo = await _userDetails.UpdateUserInfoModel(userUpdateInfo);
            //await _navigation.PopModalAsync();
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
