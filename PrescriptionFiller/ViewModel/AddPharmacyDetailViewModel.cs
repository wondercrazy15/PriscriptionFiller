using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class AddPharmacyDetailViewModel :BaseViewModel
    {
        INavigation _navigation;
        public ICommand BackCommand { get; }
        public AddPharmacyDetailViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BackCommand = new Command(BackCommandAsync);
        }

        private void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
