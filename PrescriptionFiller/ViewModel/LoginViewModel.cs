using System;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.interfaces;
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
        public ICommand ResetPassword { get; }

        public LoginViewModel(INavigation navigation)
        {
            _userDetails = DependencyService.Get<IUserDetails>();
            _validation = DependencyService.Get<IValidation>();
            _navigation = navigation;

            SignInCommand = new Command(SignInCommandAsync);
            SignUpCommand = new Command(SignUpCommandAsync);
            ResetPassword = new Command(ResetPasswordAsync);
        }



        private void SignUpCommandAsync(object obj)
        {
            _navigation.PushModalAsync(new NavigationPage(new SignUpView()));
        }

        [Obsolete]
        private void SignInCommandAsync(object obj)
        {
            //DependencyService.Get<MyToast>().Display("Prescription is successfully Sent", true);
            GetData();
        }

        [Obsolete]
        private async void GetData()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                bool IsValid = CheckEmail(Email);
                if (IsValid)
                {
                    NewLoadingPopUp.Show(_navigation);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Constants.access_token = null;
                        Constants.token_type = null;
                        Constants.user_id = 0;
                        var result = await _userDetails.GetLoginResponse(Email, Password);
                        if (result != null)
                        {
                            LoginModel loginResponse = new LoginModel();
                            loginResponse = result;
                            Constants.access_token = loginResponse.access_token;
                            Constants.token_type = loginResponse.token_type;
                            Constants.user_password = Password;
                            //GetUserId();
                            var userdata = await _userDetails.GetUserInfo();
                            if (result != null)
                            {
                                Constants.user_id = userdata.data.id;
                                await NewLoadingPopUp.Dismiss(_navigation);
                                await _navigation.PushModalAsync(new HomeView());
                            }
                            //await NewLoadingPopUp.Dismiss(_navigation);
                            //await _navigation.PushModalAsync(new HomeView());
                        }
                        else
                        {
                            await NewLoadingPopUp.Dismiss(_navigation);
                            await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "InValid Credentials");
                        }
                    });
                }
                else
                {
                    await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Please enter valid email");
                }
            }
            else
            {
                await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Please Enter Credentials");
            }
        }

        //private async void GetUserId()
        //{

        //    var result = await _userDetails.GetUserInfo();
        //    if (result != null)
        //    {
        //        Constants.user_id = result.data.id;
        //    }
        //}

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

        private async void ResetPasswordAsync(object obj)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                bool IsValid = CheckEmail(Email);
                if (IsValid)
                {
                    NewLoadingPopUp.Show(_navigation);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var result = await _userDetails.ResetPassword(Email);
                        if (result)
                        {
                            await NewLoadingPopUp.Dismiss(_navigation);
                            await MessagePopup.Show(_navigation, MessagePopup.Type.Success, MessagePopup.Icon.User, "Password Request Sent", "You will receive an email with instructions.");
                            //await _navigation.PushModalAsync(new HomeView());
                        }
                        else
                        {
                            await NewLoadingPopUp.Dismiss(_navigation);
                            await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Server not responded.");
                        }
                    });
                }
                else
                {
                    await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Incorrect email address.");
                }
            }
            else
            {
                await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Please enter email");
            }
        }
    }
}
