using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAll();
        Task<User> Get(Guid Id);

        string GetByName(string role);
        Task<ResponseModel> InsertAsync(User entity);
        Task<ResponseModel> InsertAreaOfInterestAsync(List<UserAreaOfInterest> entity);
        Task<ResponseModel> DeleteAreaOfInterestAsync(Guid UserId);
        Task<ResponseModel> UpdateAsync(User entity);
        Task<ResponseModel> DeleteAsync(User entity);
        void SaveChanges();
        Task<User> VerifyEmail(string EmailAddress);
        Task<ResponseModel> InsertForgotLinkAsync(ForgotPasswordLinks entity);
        Task<ResponseModel> VerifyForgotPasswordLink(string Link);
        Task<ResponseModel> ResetPassword(string Link, string NewPass);
        Task<IQueryable<AreaOfInterest>> GetAllAreaOfInterest();
        Task<UserStatus> GetUserStatus(Guid Id);

        Task<ResponseModel> EditUserById(User user);

        Task<IQueryable<AreaOfInterest>> GetAreaOfInterestList(Guid Id);
    }
}
