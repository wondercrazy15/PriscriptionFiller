using System;
using System.Collections.Generic;

namespace PrescriptionFiller.Model
{
    public class PharmacyListData
    {
        public int id { get; set; }
        public string chain_id { get; set; }
        public string branch_no { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string fax_number { get; set; }
        public string zip_code { get; set; }
        public string phone_number { get; set; }
        public string phone_number2 { get; set; }
        public string phone_number3 { get; set; }
        public string fax_number2 { get; set; }
        public string contact1_name { get; set; }
        public string contact2_name { get; set; }
        public string contact3_name { get; set; }
        public string chain { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string approved { get; set; }
        public string bus_license_no { get; set; }
        public string cra_number { get; set; }
        public string owner_name { get; set; }
        public string manager_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class PharmecyModel
    {
        public List<PharmacyListData> data { get; set; }
        public bool error { get; set; }
        public int error_code { get; set; }
    }

    public class PharmacyResponse
    {
        public int user_id { get; set; }
        public int pharmacy_id { get; set; }
        public string description { get; set; }
        public string extended_health { get; set; }
        public string fax_id { get; set; }
        public string medical_notes { get; set; }
        public string image_binary { get; set; }
        public int fax_status { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }

    public class PharmacySubmittedResponse
    {
        public PharmacyResponse data { get; set; }
        public bool error { get; set; }
        public int error_code { get; set; }
    }
}
