using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
using PrescriptionFiller.Helper;
using PrescriptionFiller.interfaces;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views;
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

        private string _longitude;
        public string longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
            }
        }

        private string _latitude;
        public string latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
            }
        }

        //Commands
        public ICommand BackCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SelectedItemCommand { private set; get; }
        public ICommand SendToPharmacyCommand { get; }
        public ICommand LocationCommand { get; }
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
            LocationCommand = new Command(LocationCommandAsync);
            GetPharmecyModels = obj_GetPharmecyModels;
        }

        private async void LocationCommandAsync(object obj)
        {
            try
            {
                var hasPermission = await GeoLocationUtil.CheckPermissions(Permission.Location);
            longitude = null;
            latitude = null;
            if (!hasPermission)
            {
                return;
            }
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                if (locator == null)
                {
                    return; //pharmacyListViewModel.currlocationStr = "locator is null!";
                }
                else
                {
                    //pharmacyListViewModel.currlocationStr = "task starting";
                    Position position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
                    if (position == null)
                    {
                        return;
                    }
                    longitude = position.Longitude.ToString();
                    latitude = position.Latitude.ToString();
                    //pharmacyListViewModel.currlocationStr = position.Longitude.ToString()
                    //+ ", " + position.Latitude.ToString();
                }
                longitude = longitude;
                latitude = latitude;
                if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                {
                    var result = await _userDetails.MyLocationResponse(latitude, longitude);
                    if (result != null)
                    {
                        await NewLoadingPopUp.Dismiss(_navigation);
                        GetPharmecyModels.Clear();

                        foreach (var item in result.data)
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
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
            });
            }
            catch (Exception ex)
            {

            }
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

                    foreach (var item in _navigation.ModalStack)
                    {
                        if (_navigation.ModalStack is HomeView)
                        {
                            return;
                        }
                        await item.Navigation.PopModalAsync();
                    }                  
                    //DependencyService.Get<MyToast>().Display("Prescription is successfully Sent",true);
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);                                       
                    DependencyService.Get<MyToast>().Display("Request Failed", false);
                }
            });         
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
