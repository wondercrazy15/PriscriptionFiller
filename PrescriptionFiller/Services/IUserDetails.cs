using System;
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
    }
}
