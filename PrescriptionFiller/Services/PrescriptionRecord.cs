using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrescriptionFiller.Global;
using PrescriptionFiller.Model;

namespace PrescriptionFiller.Services
{
    public class PrescriptionRecord : IPrescriptionRecord
    {
        public async Task<PrescriptionRecordModel> GetPrescriptionRecordResponse()
        {
            try
            {
                string id = "201";// Constants.user_id;
                string url = Constants.baseUrl + "public/api/prescription/user_id/" + id;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.token_type, Constants.access_token);
                //var content = new FormUrlEncodedContent(new[]
                // {
                //    new KeyValuePair<string, string>("grant_type", "password"),
                //    new KeyValuePair<string, string>("client_id", "2"),
                //    new KeyValuePair<string, string>("client_secret", "lWxODhkqibWRX2knZqBlTLk5YrDqGTuBtgNKnyWU"),
                //    new KeyValuePair<string, string>("username", email),
                //    new KeyValuePair<string, string>("password", password),
                //     new KeyValuePair<string, string>("scope", ""),
                //});
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var prescriptionRecordData = JsonConvert.DeserializeObject<PrescriptionRecordModel>(result);
                    return prescriptionRecordData;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
