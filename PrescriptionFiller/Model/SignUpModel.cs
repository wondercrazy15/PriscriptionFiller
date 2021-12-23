using System;
namespace PrescriptionFiller.Model
{
    //public class SignUpModel
    //{
    //    public string email { get; set; }
    //    public string password { get; set; }
    //    public string date_of_birth { get; set; }
    //    public string sex { get; set; }
    //    public string first_name { get; set; }
    //    public string last_name { get; set; }
    //    public string phone_number { get; set; }
    //    public string notes { get; set; }
    //}
    public class UserData
    {
        public string email { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string name { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }

    public class SignUpModel
    {
        public UserData data { get; set; }
        public bool error { get; set; }
        public int error_code { get; set; }
    }
}
