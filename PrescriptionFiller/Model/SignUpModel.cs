using System;
namespace PrescriptionFiller.Model
{
    public class SignUpModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string notes { get; set; }
    }
}
