using System;
using System.Collections.Generic;

namespace PrescriptionFiller.Model
{
    public class PrescriptionRecordModel
    {
        public List<PrescriptionRecordModelData> data { get; set; }
        public bool error { get; set; }
        public int error_code { get; set; }
    }
    public class PrescriptionRecordModelData
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string physician_id { get; set; }
        public int pharmacy_id { get; set; }
        public string description { get; set; }
        public object extended_health { get; set; }
        public string image_path { get; set; }
        public string image_binary { get; set; }
        public object fax_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public string medical_notes { get; set; }
        public int fax_status { get; set; }
        public string date_processed { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        public string time_received { get; set; }
        public string time_processed { get; set; }
        public string time_shipped { get; set; }
        public string urgency { get; set; }
        public string delivery_status { get; set; }
        public string review_status { get; set; }
        public string review_reason { get; set; }
        public string tax_status { get; set; }
    }
}
