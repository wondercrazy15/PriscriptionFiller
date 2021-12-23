using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class AddPharmacyDetailViewModel : BaseViewModel
    {
        public ObservableCollection<PharmacyListData> GetPharmecyModels { get; set; }
        public ObservableCollection<PharmacyListData> obj_GetPharmecyModels = new ObservableCollection<PharmacyListData>();
        PrescriptionItem _selectedNewPrescriptionInfo = new PrescriptionItem();
        PharmecyModel pharmecy = new PharmecyModel();
        IUserDetails _userDetails;
        INavigation _navigation;
        string PharmacyID, medicalNote, prescriptionDescriptions;
        String _city, _pharmacyName;
      
        public string pharmacyName
        {
            get
            {
                return _pharmacyName;
            }
            set
            {
                _pharmacyName = value;
                OnPropertyChanged("pharmacyName");
            }
        }
       
        public string city
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("city");
            }
        }
        //Commands
        public ICommand BackCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SelectedItemCommand { private set; get; }
        public ICommand SendToPharmacyCommand { get; }
        public PharmacyListData _SelectedItemInfoCommand;
        public PharmacyListData SelectedItemInfoCommand
        {
            get { return _SelectedItemInfoCommand; }
            set
            {
                _SelectedItemInfoCommand = value;
                OnPropertyChanged("SelectedItemInfoCommand");
            }
        }
        public AddPharmacyDetailViewModel(INavigation navigation, PrescriptionItem selectedNewPrescriptionInfo, string medicalNotesTxt, string prescriptionDescriptionTxt)
        {
          
            _userDetails = DependencyService.Get<IUserDetails>();
            _selectedNewPrescriptionInfo = selectedNewPrescriptionInfo;           
            _navigation = navigation;
            medicalNote = medicalNotesTxt;
            prescriptionDescriptions = prescriptionDescriptionTxt;
            BackCommand = new Command(BackCommandAsync);
            SearchCommand = new Command(SearchCommandAsync);
            SelectedItemCommand = new Command(SelectedItemCommandAsync);
            SendToPharmacyCommand = new Command(SendToPharmacyCommandAsync);
            GetPharmecyModels = obj_GetPharmecyModels;
        }

        private async void SendToPharmacyCommandAsync(object obj)
        {
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await _userDetails.GetPharmacySubmittedResponse(_selectedNewPrescriptionInfo, PharmacyID, medicalNote, prescriptionDescriptions);
                if (result != null)
                {
                    var results = await _userDetails.GetUserInfo();
                    await NewLoadingPopUp.Dismiss(_navigation);
                    await App.Current.MainPage.DisplayAlert("Success", "Response", "Ok");
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                    await App.Current.MainPage.DisplayAlert("Failed", "Response", "Ok");
                }
            });


            //var resp = await _userDetails.GetPharmacySubmittedResponse(_selectedNewPrescriptionInfo);

            //if (resp != null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Success", "Response", "Ok");
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Failed", "Response", "Ok");
            //}
        }

        private void SelectedItemCommandAsync(object obj)
        {
            var data = SelectedItemInfoCommand;
            PharmacyID = data.id.ToString();           
        }

        public async void SearchCommandAsync(object obj)
        {
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {

                GetPharmecyModels.Clear();
                var result = await _userDetails.GetPharmacyList(city, pharmacyName);
                if(result != null)
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                    pharmecy = result;
                    foreach (var item in pharmecy.data)
                    {
                        GetPharmecyModels.Add(new PharmacyListData
                        {
                            name = item.name,
                            address = item.address,
                            fax_number = item.fax_number,
                            id = item.id
                        });
                    }
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
            });
        }
      
        public void BackCommandAsync(object obj)
        {
            _navigation.PopModalAsync();
        }
    }
}
