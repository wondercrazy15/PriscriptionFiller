using System;
namespace PrescriptionFiller.Model
{
    public class LoginModel
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
    public class ResetPassword
    {
        public string Email { get; set; }
    }
}
