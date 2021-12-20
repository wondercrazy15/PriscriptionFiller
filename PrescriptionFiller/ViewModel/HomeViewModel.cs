using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PrescriptionFiller.Global;
using PrescriptionFiller.LocalDatabase;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;
using PrescriptionFiller.Views;
using PrescriptionFiller.Views.PopUpView;
using Xamarin.Forms;
using P = System.IO.Path;

namespace PrescriptionFiller.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        INavigation _navigation;
        IPrescriptionRecord _prescriptionRecord;
        PrescriptionRecordModel prescriptionRecordModel = new PrescriptionRecordModel();

        public ICommand buttonSent { get; }
        public ICommand buttonNew { get; }
        public ICommand buttonTakePhoto { get; }

        public ObservableCollection<HomeModel> GetHomeModels { get; set; }
        public ObservableCollection<HomeModel> Obj_GetHomeModels = new ObservableCollection<HomeModel>();
        public ICommand SentInfoCommand { private set; get; }

        public ObservableCollection<CameraModel> GetNewCameraImage { get; set; }
        public ObservableCollection<CameraModel> Obj_GetNewCameraImage = new ObservableCollection<CameraModel>();
        public ICommand NewInfoCommand { private set; get; }

        public HomeModel _SelectedSentInfoCommand;
        public HomeModel SelectedSentInfoCommand
        {
            get { return _SelectedSentInfoCommand; }
            set
            {
                _SelectedSentInfoCommand = value;
                OnPropertyChanged("SelectedSentInfoCommand");
            }
        }

        public CameraModel _SelectedNewInfoCommand;
        public CameraModel SelectedNewInfoCommand
        {
            get { return _SelectedNewInfoCommand; }
            set
            {
                _SelectedNewInfoCommand = value;
                OnPropertyChanged("SelectedNewInfoCommand");
            }
        }

        private string _newBoxHeight, _sentBoxHeight, _sentButtonLableColor, _newButtonLableColor;
        public string newBoxViewHeight
        {
            get
            {
                return _newBoxHeight;
            }
            set
            {
                _newBoxHeight = value;
                OnPropertyChanged("newBoxViewHeight");
            }
        }
        public string sentBoxViewHeight
        {
            get
            {
                return _sentBoxHeight;
            }
            set
            {
                _sentBoxHeight = value;
                OnPropertyChanged("sentBoxViewHeight");
            }
        }
        public string newButtonLableColor
        {
            get
            {
                return _newButtonLableColor;
            }
            set
            {
                _newButtonLableColor = value;
                OnPropertyChanged("newButtonLableColor");
            }
        }
        public string sentButtonLableColor
        {
            get
            {
                return _sentButtonLableColor;
            }
            set
            {
                _sentButtonLableColor = value;
                OnPropertyChanged("sentButtonLableColor");
            }
        }

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _prescriptionRecord = DependencyService.Get<IPrescriptionRecord>();

            newBoxViewHeight = "5";
            sentBoxViewHeight = "0";
            newButtonLableColor = "#FFC65B";
            sentButtonLableColor = "White";
            buttonNew = new Command(ButtonNewCommand);
            buttonSent = new Command(ButtonSentCommand);
            buttonTakePhoto = new Command(ButtonTakePhotoCommand);
            SentInfoCommand = new Command(SentInfoClick);
            NewInfoCommand = new Command(NewInfoClick);

            GetHomeModels = Obj_GetHomeModels;
            GetNewCameraImage = Obj_GetNewCameraImage;
            NewCameraData();
        }

        private async void NewInfoClick(object obj)
        {
            try
            {
                //sent image click
                var Data = SelectedNewInfoCommand;
                Console.WriteLine(SelectedNewInfoCommand.getCameraImage.ToString());
                await _navigation.PushModalAsync(new NavigationPage(new AddNewPrescriptionView(SelectedNewInfoCommand)));
                //SelectedSentInfoCommand = null;
            }
            catch (Exception ex)
            {

            }
        }

        private async void SentInfoClick(object obj)
        {
            try
            {
                //sent image click
                var Data = SelectedSentInfoCommand;
                Console.WriteLine(SelectedSentInfoCommand.Pharmacy);
                //SelectedSentInfoCommand = null;
            }
            catch (Exception ex)
            {

            }
        }

        private void ButtonTakePhotoCommand(object obj)
        {
            //Click permission of photo
            try
            {
                //ICameraServices cameraServices = DependencyService.Get<ICameraServices>();
                //cameraServices.CameraCaptureImage();
                ICameraService cameraService = DependencyService.Get<ICameraService>();
                Task<Photo> takePictureTask = cameraService.TakePicture(delegate (Photo photo)
                {
                    Console.WriteLine("camera");
                    string thumbPath = P.Combine(P.GetDirectoryName(photo.Picture.Path), P.GetFileNameWithoutExtension(photo.Picture.Path)) + "_thumb.jpg";
                    PrescriptionItem prescriptionItem = new PrescriptionItem();
                    prescriptionItem.thumbPath = thumbPath;
                    prescriptionItem.userId = int.Parse("201"); //Constants.user_id;
                    LocalPrescriptionDatabase.Instance.SaveItem(prescriptionItem);


                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }

        private void ButtonSentCommand(object obj)
        {
            //Loading
            newBoxViewHeight = "0";
            sentBoxViewHeight = "5";
            newButtonLableColor = "White";
            sentButtonLableColor = "#FFC65B";
            //Device.BeginInvokeOnMainThread(() =>
            //{
                PrescriptionData();
            //});
            
        }
        private void ButtonNewCommand(object obj)
        {
            newBoxViewHeight = "5";
            sentBoxViewHeight = "0";
            newButtonLableColor = "#FFC65B";
            sentButtonLableColor = "White";
            NewCameraData();
        }

        private async void NewCameraData()
        {
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {
                //IEnumerable<PrescriptionItem> localPrescriptionItems = LocalPrescriptionDatabase.Instance.GetItems(201);//Constants.user_id
                //Console.WriteLine("localPrescriptionItems => " + localPrescriptionItems.Count());
                //foreach (PrescriptionItem localPrescriptionItem in localPrescriptionItems)
                //{
                //    //API.PrescriptionRecord serverPrescriptionRecord;
                //    //serverPrescriptionRecord = serverPrescriptionRecordsDict.getValue(localPrescriptionItem.path);

                //    //if (serverPrescriptionRecord == null)
                //    //    continue;

                //    //int serverPrescriptionId = (serverPrescriptionRecord == null) ? -1 : serverPrescriptionRecord.id;
                //    //int serverPharmacyId = (serverPrescriptionRecord == null) ? -1 : Int32.Parse(serverPrescriptionRecord.pharmacy_id);
                //    //int localPrescriptionId = localPrescriptionItem.serverPrescriptionId;
                //    //string serverStatus = (serverPrescriptionRecord == null) ? "" : nvl(serverPrescriptionRecord.fax_status);
                //    //string localStatus = nvl(localPrescriptionItem.status);
                //    //string serverPrescriptionCreatedDt = (serverPrescriptionRecord == null) ? "" : nvl(serverPrescriptionRecord.created_at);

                //    //if (serverPrescriptionRecord != null)
                //    //{
                //    //    if (!serverPrescriptionId.Equals(localPrescriptionId) ||
                //    //        !serverStatus.Equals(localStatus))
                //    //    {
                //    //        localPrescriptionItem.status = serverStatus;
                //    //        localPrescriptionItem.serverPrescriptionId = serverPrescriptionId;
                //    //        localPrescriptionItem.createdDt = serverPrescriptionCreatedDt;
                //    //        LocalPrescriptionDatabase.Instance.SaveItem(localPrescriptionItem);
                //    //    }
                //    //}

                //    //// todo add call to get Pharamacy info by pharmacy id
                //    //if (serverPharmacyId > 0 && !processedPharamcyId(serverPharmacyId, API.Instance.pharmacyInfos))
                //    //{
                //    //    await API.Instance.RetrievePharmaciesById(HomePage.Instance.Navigation, serverPharmacyId,
                //    //       retrievePharmaciesByIdSuccess,
                //    //       retrievePharmaciesByIdFail);
                //    //}
                //}
                //await NewLoadingPopUp.Dismiss(_navigation);





                var result = await _prescriptionRecord.GetPrescriptionRecordResponse();
                if (result != null)
                {
                    prescriptionRecordModel = result;
                    GetNewCameraImage.Clear();
                    foreach (var i in prescriptionRecordModel.data)
                    {
                        string pharmacy = "", physician = "", image = "";
                        byte[] imageBytes;
                        ImageSource image1;
                        if (i.pharmacy_id != null)
                            pharmacy = i.pharmacy_id.ToString();
                        else
                            pharmacy = "";

                        if (i.physician_id != null)
                            physician = i.physician_id.ToString();
                        else
                            physician = "";

                        if (i.image_binary != null)
                        {
                            if (i.image_binary != "")
                            {
                                imageBytes = Convert.FromBase64String(i.image_binary);
                                image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageBytes));
                                GetNewCameraImage.Add(new CameraModel
                                {
                                    getCameraImage = image1
                                });
                            }
                        }


                    }
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
            });
        }

        private async void PrescriptionData()
        {
            NewLoadingPopUp.Show(_navigation);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await _prescriptionRecord.GetPrescriptionRecordResponse();
                if (result != null)
                {
                    prescriptionRecordModel = result;
                    GetHomeModels.Clear();
                    foreach (var i in prescriptionRecordModel.data)
                    {
                        string pharmacy = "", physician = "", image = "";
                        byte[] imageBytes;
                        ImageSource image1;
                        if (i.pharmacy_id != null)
                            pharmacy = i.pharmacy_id.ToString();
                        else
                            pharmacy = "";

                        if (i.physician_id != null)
                            physician = i.physician_id.ToString();
                        else
                            physician = "";

                        if (i.image_binary != null)
                        {
                            if (i.image_binary != "")
                            {
                                imageBytes = Convert.FromBase64String(i.image_binary);
                                image1 = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageBytes));
                            }
                            else
                                image1 = "default_image";
                        }
                        else
                            image1 = "default_image";

                        GetHomeModels.Add(new HomeModel
                        {
                            Pharmacy = pharmacy,
                            Physician = physician,
                            GetImage = image1
                        });
                    }
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
                else
                {
                    await NewLoadingPopUp.Dismiss(_navigation);
                }
            });

        }
    }
}
