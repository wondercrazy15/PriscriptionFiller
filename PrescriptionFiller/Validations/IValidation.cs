using System;
namespace PrescriptionFiller.Validations
{
    public interface IValidation
    {
        bool EmailValidation(string email);
        bool PasswordValidation(string password);
        bool PhoneNumberValidation(string phonenumber);
    }
}
