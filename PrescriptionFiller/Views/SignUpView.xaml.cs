using System;
using System.Collections.Generic;
using Plugin.InputKit.Shared.Controls;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public partial class SignUpView : ContentPage
    {
        public SignUpViewModel ViewModel;
        public SignUpView()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SignUpViewModel(Navigation);
        }

        void Full_Name_Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(FullNameErrorColor, entry, "FirstName");
        }

        void Last_Name_Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(LastNameErrorColor, entry, "LastName");
        }

        void Phone_Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(PhoneNumberErrorColor, entry, "PhoneNumber");
        }

        void EmailEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(EmailErrorColor, entry, "Email");
        }

        void Password_Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(PasswordErrorColor, entry, "Password");
        }

        void Co_Password_Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            ViewModel.ChangeErrorLineColor(Co_PasswordErrorColor, entry, "Co_Password");
        }

        void CustomDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
        }

        void RadioButtonGroupView_SelectedItemChanged(System.Object sender, System.EventArgs e)
        {
            RadioButtonGroupView radio = (RadioButtonGroupView)sender;
            int selected = radio.SelectedIndex;
            if (selected != 0)
            {
                ViewModel.Sex = "Female";
            }
            else
            {
                ViewModel.Sex = "Male";
            }

        }

        void Show_CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            Xamarin.Forms.CheckBox checkBox = (Xamarin.Forms.CheckBox)sender;
            if (checkBox.IsChecked)
            {
                Password.IsPassword = false;
                Co_Password.IsPassword = false;
            }
            else
            {
                Password.IsPassword = true;
                Co_Password.IsPassword = true;
            }
        }
    }
}