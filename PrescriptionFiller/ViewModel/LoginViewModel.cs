using System;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Validations;
using PrescriptionFiller.Views;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public INavigation _navigation;
        private IUserDetails _userDetails;
        public IValidation _validation;

        private string _emailAddress;
        public string Email
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged("Email");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        //Command
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            _userDetails = DependencyService.Get<IUserDetails>();
            _validation = DependencyService.Get<IValidation>();
            _navigation = navigation;

            SignInCommand = new Command(SignInCommandAsync);
            SignUpCommand = new Command(SignUpCommandAsync);
        }



        private void SignUpCommandAsync(object obj)
        {
            _navigation.PushModalAsync(new NavigationPage(new SignUpView()));
        }

        private void SignInCommandAsync(object obj)
        {
            GetData();
        }

        [Obsolete]
        private async void GetData()
        {
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool IsValid = CheckEmail(Email);
                if (IsValid)
                {
                    Constants.access_token = null;
                    Constants.token_type = null;
                    Constants.user_id =0;
                    var result = await _userDetails.GetLoginResponse(Email, Password);
                    if (result != null)
                    {
                        LoginModel loginResponse = new LoginModel();
                        loginResponse = result;
                        Constants.access_token = loginResponse.access_token;
                        Constants.token_type = loginResponse.token_type;
                        Constants.user_password = Password;
                        GetUserId();
                        await NewLoadingPopUp.Dismiss(_navigation);
                        await _navigation.PushModalAsync(new HomeView());
                    }
                    else
                    {
                        await NewLoadingPopUp.Dismiss(_navigation);
                    }
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }

            });
            
        }

        private async void GetUserId()
        {
          
            var result = await _userDetails.GetUserInfo();
            if (result != null)
            {
                Constants.user_id = result.data.id;
            }
        }

        public bool CheckEmail(string email)
        {
          bool IsEmail = _validation.EmailValidation(email);
            if (IsEmail)
                return true;
            else
                return false;
        }

        public bool CheckPassword(string password)
        {
            bool IsPassword = _validation.PasswordValidation(password);
            if (IsPassword)
                return true;
            else
                return false;
        }
    }
}
