using System;
using System.Collections.Generic;
using System.Windows.Input;
using Plugin.InputKit.Shared.Controls;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Validations;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Co_Password { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Notes { get; set; }


        public Color _FirstErrorLine;
        public Color FirstErrorLine
        {
            get { return _FirstErrorLine; }
            set
            {
                _FirstErrorLine = value;
                OnPropertyChanged("FirstErrorLine");
            }
        }

        public Color _LastErrorLine;
        public Color LastErrorLine
        {
            get { return _LastErrorLine; }
            set
            {
                _LastErrorLine = value;
                OnPropertyChanged("LastErrorLine");
            }
        }

        public Color _BirthdayErrorLine;
        public Color BirthdayErrorLine
        {
            get { return _BirthdayErrorLine; }
            set
            {
                _BirthdayErrorLine = value;
                OnPropertyChanged("BirthdayErrorLine");
            }
        }

        public Color _PhoneErrorLine;
        public Color PhoneErrorLine
        {
            get { return _PhoneErrorLine; }
            set
            {
                _PhoneErrorLine = value;
                OnPropertyChanged("PhoneErrorLine");
            }
        }

        public Color _EmailErrorLine;
        public Color EmailErrorLine
        {
            get { return _EmailErrorLine; }
            set
            {
                _EmailErrorLine = value;
                OnPropertyChanged("EmailErrorLine");
            }
        }

        public Color _PasswordErrorLine;
        public Color PasswordErrorLine
        {
            get { return _PasswordErrorLine; }
            set
            {
                _PasswordErrorLine = value;
                OnPropertyChanged("PasswordErrorLine");
            }
        }

        public Color _Co_PasswordErrorLine;
        public Color Co_PasswordErrorLine
        {
            get { return _Co_PasswordErrorLine; }
            set
            {
                _Co_PasswordErrorLine = value;
                OnPropertyChanged("Co_PasswordErrorLine");
            }
        }

        IValidation _validation;
        private IUserDetails _userDetails;
        INavigation _navigation;
        public ICommand SignUpCommand { get; }
        public ICommand BackCommand { get; }

        public SignUpViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _userDetails = DependencyService.Get<IUserDetails>();
            _validation = DependencyService.Get<IValidation>();
            SignUpCommand = new Command(SignUpCommandAsync);
            BackCommand = new Command(BackCommandAsync);
            SetErrorLineColor();
        }

        private void SetErrorLineColor()
        {
            FirstErrorLine = Color.Gray;
            LastErrorLine = Color.Gray;
            BirthdayErrorLine = Color.Gray;
            PhoneErrorLine = Color.Gray;
            EmailErrorLine = Color.Gray;
            PasswordErrorLine = Color.Gray;
            Co_PasswordErrorLine = Color.Gray;
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }

        public async void SignUpCommandAsync(object obj)
        {

            if (!string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(Email)
                && !string.IsNullOrEmpty(Password)
                && !string.IsNullOrEmpty(PhoneNumber)
                && !string.IsNullOrEmpty(Co_Password)
                && !string.IsNullOrEmpty(DateOfBirth)
                && !string.IsNullOrEmpty(Sex)
                )
            {
                if (_validation.PhoneNumberValidation(PhoneNumber) && PhoneNumber.Length == 10)
                {
                    if (_validation.EmailValidation(Email))
                    {
                        if (Password == Co_Password)
                        {
                            NewLoadingPopUp.Show(_navigation);
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var result = await _userDetails.GetSignUpResponse(Email, Password, DateOfBirth, Sex, FirstName, LastName, PhoneNumber, "notes");

                                if (result != null)
                                {
                                    await NewLoadingPopUp.Dismiss(_navigation);
                                    await _navigation.PopModalAsync();
                                }
                                else
                                {
                                    await NewLoadingPopUp.Dismiss(_navigation);
                                    // await _navigation.PopModalAsync();
                                }

                            });
                        }
                        else
                        {
                            await MessagePopup.Show(_navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Oops!", "Password does not match.");
                        }
                    }
                    else
                    {
                        EmailErrorLine = Color.Red;
                    }
                }
                else
                {
                    PhoneErrorLine = Color.Red;
                }
            }
            else
            {
                FirstErrorLine = Color.Red;
                LastErrorLine = Color.Red;
                BirthdayErrorLine = Color.Red;
                PhoneErrorLine = Color.Red;
                EmailErrorLine = Color.Red;
                PasswordErrorLine = Color.Red;
                Co_PasswordErrorLine = Color.Red;
            }
        }

        public void ChangeErrorLineColor(BoxView errorLine, Entry entry, string entryName)
        {
            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                if (entryName == "PhoneNumber")
                {
                    if (_validation.PhoneNumberValidation(PhoneNumber) && PhoneNumber.Length == 10)
                    {
                        errorLine.BackgroundColor = Color.Gray;
                    }
                    else
                    {
                        errorLine.BackgroundColor = Color.Red;
                    }
                }
                else if (entryName == "Email")
                {
                    bool IsValid = CheckEmail(Email);
                    if (IsValid)
                    {
                        errorLine.BackgroundColor = Color.Gray;
                    }
                    else
                    {
                        errorLine.BackgroundColor = Color.Red;
                    }
                }
                else
                {
                    errorLine.BackgroundColor = Color.Gray;
                }
            }
            else
            {
                errorLine.BackgroundColor = Color.Red;
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
    }
}