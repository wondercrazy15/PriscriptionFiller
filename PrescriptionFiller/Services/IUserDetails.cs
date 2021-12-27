using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PrescriptionFiller.Model;

namespace PrescriptionFiller.Services
{
    public interface IUserDetails
    {
        Task<LoginModel> GetLoginResponse(string email, string password);
        Task<UserInfoModel> GetUserInfo();
        Task<SignUpModel> GetSignUpResponse(string email, string password, string DateOfBirth, string sex, string firstName, string lastName, string phoneNumber, string notes);
        Task<UserInfoModel> UpdateUserInfoModel(Data getInfoModel);
        Task<PharmecyModel> GetPharmacyList(string city, string pharmacyName);
        Task<PharmacySubmittedResponse> GetPharmacySubmittedResponse(PrescriptionItem _selectedNewPrescriptionInfo, string pharmacyID, string medicalNote, string prescriptionDescriptions);
        Task<bool> ResetPassword(string email);
        Task<PharmecyModel> MyLocationResponse(string lattitude, string longitude);
    }
}
