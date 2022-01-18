using System;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class AccountInfoViewModel : BaseViewModel
    {
        public INavigation _navigation;
        private IUserDetails _userDetails;
        UserInfoModel userInfoModel;
        public ICommand EditAccountInfoCommand { get; }
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
        public AccountInfoViewModel(INavigation navigation)
        {
            _userDetails = DependencyService.Get<IUserDetails>();
            _navigation = navigation;
            EditAccountInfoCommand = new Command(EditAccountInfoAsync);
            getUserInfo();
        }
        private async void EditAccountInfoAsync(object obj)
        {
            if (userInfoModel != null)
            {
                await _navigation.PushModalAsync(new NavigationPage(new EditAccountInfoView(userInfoModel)));
            }
            else
            {

            }

            
        }
        public async void getUserInfo()
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await _userDetails.GetUserInfo();
                    if (result != null)
                    {
                        Constants.user_id = result.data.id;
                        userInfoModel = result;
                        firstName = result.data.first_name;
                        lastName = result.data.last_name;
                        string[] tempArray = result.data.date_of_birth.Split(' ');
                        dob = tempArray[0];
                        gender = (result.data.sex).ToString();
                        phone = result.data.phone_number;
                        emailAdress = result.data.email;
                        allerigies = result.data.allergies;
                        medicalInsuranceProvider = result.data.medical_insurance_provider;
                        carrier = result.data.carrier_number;
                        plan = result.data.plan_number;
                        memberId = result.data.member_id;
                        issue = result.data.issue_number;
                        personalHeathNumber = result.data.personal_health_number;
                        await NewLoadingPopUp.Dismiss(_navigation);
                    }
                });
            }
            catch (Exception ex)
            {

            }
                
        }
    }
}
