using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;

namespace PrescriptionFiller.Services
{
    public class UserDetails : IUserDetails
    {
        //Constants constants = new Constants();
        public async Task<LoginModel> GetLoginResponse(string email, string password)
        {
            try
            {
                string url = Constants.baseUrl + "oauth/token";
                HttpClient client = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "2"),
                    new KeyValuePair<string, string>("client_secret", "lWxODhkqibWRX2knZqBlTLk5YrDqGTuBtgNKnyWU"),
                    new KeyValuePair<string, string>("username", email),
                    new KeyValuePair<string, string>("password", password),
                     new KeyValuePair<string, string>("scope", ""),
                });

                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var loginresponse = JsonConvert.DeserializeObject<LoginModel>(result);
                    return loginresponse;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<UserInfoModel> GetUserInfo()
        {
            try
            {
                string url = Constants.baseUrl + "public/api/user";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<UserInfoModel>(result);
                    return userInfoResponse;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<SignUpModel> GetSignUpResponse(string email, string password, string DateOfBirth, string sex, string firstName, string lastName, string phoneNumber, string notes)
        {
            try
            {
                string url = Constants.baseUrl + "user";
                HttpClient client = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("date_of_birth", DateOfBirth),
                    new KeyValuePair<string, string>("sex", sex=="Male"?sex="M":sex="F"),
                    new KeyValuePair<string, string>("first_name", firstName),
                    new KeyValuePair<string, string>("last_name", lastName),
                    new KeyValuePair<string, string>("phone_number", phoneNumber),
                    new KeyValuePair<string, string>("notes", notes),
                });

                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var signupresponse = JsonConvert.DeserializeObject<SignUpModel>(result);
                    return signupresponse;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<UserInfoModel> UpdateUserInfoModel(Data getInfoModel)
        {
            try
            {
                string url = Constants.baseUrl + "public/api/user/" + Constants.user_id;
                HttpClient client = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("email", getInfoModel.email),
                    new KeyValuePair<string, string>("password", Constants.user_password),
                    new KeyValuePair<string, string>("date_of_birth", getInfoModel.date_of_birth),
                    new KeyValuePair<string, string>("sex", getInfoModel.sex),
                    new KeyValuePair<string, string>("first_name", getInfoModel.first_name),
                    new KeyValuePair<string, string>("last_name", getInfoModel.last_name),
                    new KeyValuePair<string, string>("phone_number", getInfoModel.phone_number),
                    new KeyValuePair<string, string>("allergies", getInfoModel.allergies),
                    new KeyValuePair<string, string>("medical_insurance_provider", getInfoModel.medical_insurance_provider),
                    new KeyValuePair<string, string>("carrier_number", getInfoModel.carrier_number),
                    new KeyValuePair<string, string>("plan_number", getInfoModel.plan_number),
                    new KeyValuePair<string, string>("member_id", getInfoModel.member_id),
                    new KeyValuePair<string, string>("issue_number", getInfoModel.issue_number),
                    new KeyValuePair<string, string>("personal_health_number", getInfoModel.personal_health_number),
                    new KeyValuePair<string, string>("shots", getInfoModel.shots),
                    new KeyValuePair<string, string>("drugs", getInfoModel.drugs),
                    new KeyValuePair<string, string>("vaccinations", getInfoModel.vaccinations),
                });
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                HttpResponseMessage response = await client.PutAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var updatedUserIfo = JsonConvert.DeserializeObject<UserInfoModel>(result);
                    return updatedUserIfo;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<PharmecyModel> GetPharmacyList(string city, string pharmacyName)
        {
            //"public/api/pharmacy/name/" + ((pharmacyName != null && !pharmacyName.Equals("")) ? pharmacyName : "null") + "/city/" + ((city != null && !city.Equals("")) ? city : "null")
            try
            {
                string url = Constants.baseUrl + "public/api/pharmacy/name/" + ((pharmacyName != null && !pharmacyName.Equals("")) ? pharmacyName : "null") + "/city/" + ((city != null && !city.Equals("")) ? city : "null");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<PharmecyModel>(result);
                    return userInfoResponse;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<PharmacySubmittedResponse> GetPharmacySubmittedResponse(PrescriptionItem _selectedNewPrescriptionInfo, string pharmacyID, string medicalNote, string prescriptionDescriptions)
        {
            #region MyRegion
            //try
            //{
            //    string url = "https://api.prescriptionfiller.com/api/prescription/";
            //    //string url = Constants.baseUrl + "api/prescription/";
            //    HttpClient client = new HttpClient();
            //    byte[] imageArray = System.IO.File.ReadAllBytes(_selectedNewPrescriptionInfo.thumbPath);
            //    var base64ImageRepresentation = Convert.ToBase64String(imageArray);

            //    #region
            //    SendPharmacyInfo content = new SendPharmacyInfo();
            //    content.user_id = "201";
            //    content.pharmacy_id = "17316";
            //    content.description = "";
            //    content.extended_health = "";
            //    content.fax_id = "";
            //    content.medical_notes = "";
            //    content.image_binary = "";// "data:image/png;base64," + base64ImageRepresentation;
            //    #endregion

            //    //SendPharmacyInfo content = new SendPharmacyInfo();
            //    //content.user_id = Constants.user_id.ToString();
            //    //content.pharmacy_id = pharmacyID;
            //    //content.description = prescriptionDescriptions;
            //    //content.extended_health = "";
            //    //content.fax_id = _selectedNewPrescriptionInfo.pharmacyFaxNumber;
            //    //content.medical_notes = medicalNote;
            //    //content.image_binary = base64ImageRepresentation; //"data:image/png;base64," +

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
            //    var jasondata = JsonConvert.SerializeObject(content);
            //    var jsondatarray = System.Text.Encoding.ASCII.GetBytes(jasondata);
            //    //var jsonrequest = new StringContent(jasondata);
            //    var jsonrequest = new ByteArrayContent(jsondatarray);
            //    HttpResponseMessage response = await client.PostAsync(url, jsonrequest);

            //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var result = await response.Content.ReadAsStringAsync();
            //        var signupresponse = JsonConvert.DeserializeObject<PharmacySubmittedResponse>(result);
            //        return signupresponse;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            #endregion
            try
            {
                string url = "https://api.prescriptionfiller.com/api/prescription/";
                HttpClient client = new HttpClient();
                byte[] imageArray = System.IO.File.ReadAllBytes(_selectedNewPrescriptionInfo.thumbPath);
                var base64ImageRepresentation = Convert.ToBase64String(imageArray);              

                SendPharmacyInfo content = new SendPharmacyInfo();
                content.user_id = Constants.user_id.ToString();
                content.pharmacy_id = pharmacyID;
                content.description = prescriptionDescriptions;
                content.extended_health = "";
                content.fax_id = _selectedNewPrescriptionInfo.pharmacyFaxNumber;
                content.medical_notes = medicalNote;
                content.image_binary = base64ImageRepresentation;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                var jasondata = JsonConvert.SerializeObject(content);
                //var d = System.Text.Encoding.ASCII.GetBytes(jasondata);
                var jsonrequest = new StringContent(jasondata, System.Text.Encoding.UTF8, "application/json");

                //var jsonrequest = new ByteArrayContent(d);
                //HttpResponseMessage hrm = await _client.PostAsync(url, new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                //HttpResponseMessage response = await client.PostAsync(url, content);

                HttpResponseMessage response = await client.PostAsync(url, jsonrequest);               
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var signupresponse = JsonConvert.DeserializeObject<PharmacySubmittedResponse>(result);
                    return signupresponse;
                }
            }
            catch (Exception ex)
            {

            }
            return null;          
        }
        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                string url = Constants.baseUrl + "public/api/reset_password";
                HttpClient client = new HttpClient();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email)
                });

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<ResetPassword>(result);
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<PharmecyModel> MyLocationResponse(string lattitude, string longitude)
        {
            try
            {
                string url = Constants.baseUrl + "public/api/pharmacy/latitude/" + lattitude + "/longitude/" + longitude;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<PharmecyModel>(result);
                    return userInfoResponse;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }

}

public class SendPharmacyInfo
{
    public string user_id { get; set; }
    public string pharmacy_id { get; set; }
    public string description { get; set; }
    public string extended_health { get; set; }
    public string fax_id { get; set; }
    public string medical_notes { get; set; }
    public string image_binary { get; set; }
}
