using System;
namespace PrescriptionFiller.Model
{
    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string allergies { get; set; }
        public string medical_insurance_provider { get; set; }
        public string carrier_number { get; set; }
        public string plan_number { get; set; }
        public string member_id { get; set; }
        public string issue_number { get; set; }
        public string personal_health_number { get; set; }
        public string shots { get; set; }
        public string drugs { get; set; }
        public string vaccinations { get; set; }
        public int user_type { get; set; }
        public int activated { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string date_registered { get; set; }
    }
    public class UserInfoModel
    {
        public Data data { get; set; }
        public bool error { get; set; }
        public int error_code { get; set; }
    }
}