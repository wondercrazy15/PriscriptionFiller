using System;
using System.Collections.Generic;
using System.Windows.Input;
using Plugin.InputKit.Shared.Controls;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
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

        private IUserDetails _userDetails;
        INavigation _navigation;
        public ICommand SignUpCommand { get; }
        public ICommand BackCommand { get; }

        public SignUpViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _userDetails = DependencyService.Get<IUserDetails>();
            SignUpCommand = new Command(SignUpCommandAsync);
            BackCommand = new Command(BackCommandAsync);
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

            }
        }

        public void ChangeErrorLineColor(BoxView errorLine, Entry entry, string entryName)
        {
            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                //if (entryName.StartsWith("FullName"))
                //{

                //}

                errorLine.BackgroundColor = Color.Gray;
            }
            else
            {
                errorLine.BackgroundColor = Color.Red;
            }
        }
    }
}