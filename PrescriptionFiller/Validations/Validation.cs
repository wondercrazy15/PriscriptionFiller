using System;
using System.Text.RegularExpressions;

namespace PrescriptionFiller.Validations
{
    public class Validation : IValidation
    {
        public bool EmailValidation(string email)
        {
            try
            {
                email = email.Trim();
                if (!string.IsNullOrWhiteSpace(email))
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(email);
                    if (match.Success)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }
          
        }

        public bool PasswordValidation(string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(password))
                {
                    Regex regex = new Regex("^[a-zA-Z0-9]+$");
                    Match match = regex.Match(password);
                    if (match.Success)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
