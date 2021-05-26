using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.UserServices
{
    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<ResponseModel> GetUserBy(Guid Id);
        string GetUserByName(string username);
        Task<ResponseModel> InsertUser(User user);
        Task<ResponseModel> UpdateUser(User user);
        Task<ResponseModel> InsertUserAreaOfInterest(Guid UserId, List<Guid> AreaOfInterestIds);
        Task<ResponseModel> UpdateUserAreaOfInterest(Guid UserId, List<Guid> AreaOfInterestIds);
        ResponseModel Delete(User user);

        Task<ResponseModel> Login(string email, string password, UserType userType, string devicetype, string devicetoken);
        Task<ResponseModel> Logout(Guid UserId);
        Task<ResponseModel> ChangePassword(ChangePasswordDTO password);
        Task<ResponseModel> VerifyEmail(string EmailAddress);
        Task<ResponseModel> InsertForgotLink(ForgotPasswordLinks entity);
        Task<ResponseModel> VerifyForgotPasswordLink(string Link);
        Task<ResponseModel> GetAllAreaOfInterest();
        Task<ResponseModel> ResetPassword(string Link, string NewPassword);
        Task<ResponseModel> GetUserStatus(Guid Id);

        Task<ResponseModel> EditUserStatus(User user);

        Task<ResponseModel> GetAreaOfInterestById(Guid Id);
    }
}
