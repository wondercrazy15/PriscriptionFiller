using System;
using Xamarin.Forms;

namespace PrescriptionFiller.Model
{
    public class HomeModel
    {
       public ImageSource GetImage { get; set; }
       public string Pharmacy { get; set; }
       public string Physician { get; set; }
    }

    public class CameraModel
    {
        public ImageSource getCameraImage { get; set; }
        public string getCameraPhotoPath { get; set; }
    }
}
