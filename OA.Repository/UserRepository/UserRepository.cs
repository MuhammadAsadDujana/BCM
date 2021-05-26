using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dContext;
        private DbSet<User> entites;

        public UserRepository()
        {
            _dContext = new ApplicationContext();
        }

        public UserRepository(ApplicationContext dContext)
        {
            this._dContext = dContext;
            entites = dContext.Set<User>();
        }

        public async Task<User> Get(Guid Id)
        {
            var user = await _dContext.User.Where(x => x.UserId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            return user;
        }

        public string GetByName(string username)
        {
            string role =  _dContext.User.Where(x => x.FirstName == username && x.IsActive == true).FirstOrDefault().UserType.ToString();
            return role;
        }


        public async Task<IQueryable<User>> GetAll()
        {
            try
            {
                var data = await _dContext.User.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<ResponseModel> InsertAsync(User entity)
        {
            if (entity == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(entity).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", ConstantMessages.RegistrationSuccess, "200", entity, null);
        }
        public async Task<ResponseModel> UpdateAsync(User entity)
        {
            if (entity == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", entity, null);

            _dContext.Entry(entity).State = EntityState.Modified;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Updated", "200", entity, null);
        }
        public Task<ResponseModel> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<User> VerifyEmail(string EmailAddress)
        {
            var user = await _dContext.User.Where(x => x.IsActive == true && x.Email == EmailAddress).FirstOrDefaultAsync();
            return user;
        }

        public async Task<ResponseModel> InsertForgotLinkAsync(ForgotPasswordLinks entity)
        {
            if (entity == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(entity).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", entity, null);
        }
        public async Task<ResponseModel> VerifyForgotPasswordLink(string Link)
        {
            var data = await _dContext.ForgotPasswordLinks.Where(x => x != null && x.Link.Equals(Link) && x.IsActive == true).FirstOrDefaultAsync();
            return ResponseHandler.GetResponse("Success", "verify", "200", data, null);
        }

        public async Task<ResponseModel> ResetPassword(string Link, string NewPass)
        {
            var AssociatedLink = await _dContext.ForgotPasswordLinks.Where(x => x != null && x.Link.Equals(Link) && x.IsActive == true).FirstOrDefaultAsync();
            if (AssociatedLink != null)
            {
                if (AssociatedLink.ExpiryDate > DateTime.Now)
                {
                    var AssociatedUser = await Get(AssociatedLink.UserId);
                    AssociatedUser.Password = Encryption.EncodePasswordToBase64(NewPass);
                    _dContext.Entry(AssociatedUser).State = EntityState.Modified;

                    AssociatedLink.IsActive = false;
                    _dContext.Entry(AssociatedLink).State = System.Data.Entity.EntityState.Modified;
                    await _dContext.SaveChangesAsync();

                    return new ResponseModel { Code = "200", Status = "Success", Message = "Your password has been reset successfully." };
                }
                else
                {
                    return new ResponseModel { Code = "400", Status = "Failed", Message = "Your password reset link has expired." };
                }
            }
            else
            {
                return new ResponseModel { Code = "400", Status = "Failed", Message = "Reset password link is invalid." };
            }
        }

        public async Task<ResponseModel> InsertAreaOfInterestAsync(List<UserAreaOfInterest> entity)
        {
            if (entity.Count == 0)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.UserAreaOfInterest.AddRange(entity);
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", ConstantMessages.RegistrationSuccess, "200", entity, null);
        }

        public async Task<ResponseModel> DeleteAreaOfInterestAsync(Guid UserId)
        {
            if (UserId == Guid.Empty)
                return ResponseHandler.GetResponse("Failed", "UserId is null", "404", null, null);

            var entity = await _dContext.UserAreaOfInterest.Where(x => x.UserId == UserId && x.IsActive == true).ToListAsync();
            if(entity.Count == 0)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.UserAreaOfInterest.RemoveRange(entity);
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", ConstantMessages.RegistrationSuccess, "200", entity, null);
        }

        public async Task<IQueryable<AreaOfInterest>> GetAllAreaOfInterest()
        {
            var data = await _dContext.AreaOfInterests.Where(x => x.IsActive == true).ToListAsync();
            return data.AsQueryable();
        }

        public async Task<UserStatus> GetUserStatus(Guid Id)
        {
            var user = await _dContext.User.Where(x => x.UserId.Equals(Id) && x.IsActive == true).Select(x => x.UserStatus).FirstOrDefaultAsync();
            return user;
        }

        public async Task<ResponseModel> EditUserById(User user)
        {
            if (user == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            var data = _dContext.User.Find(user.UserId);

            if (data != null)
            {
                data.IsActive = user.IsActive;
                //   data.UserStatus = user.UserStatus == 0 ? UserStatus.UnVerified : UserStatus.Verified;
                data.UserStatus = user.UserStatus;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString();
                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();


                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }
            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

        }

        public async Task<IQueryable<AreaOfInterest>> GetAreaOfInterestList(Guid Id)
        {
            var AreaOfInterests = await (from c in _dContext.AreaOfInterests
                                              join pc in _dContext.UserAreaOfInterest on c.AreaOfInterestId equals pc.AreaOfInterestId
                                              where pc.UserId.Equals(Id) && pc.IsActive == true
                                              select new
                                              {
                                                  c.AreaOfInterestId,
                                                  c.InterestTitle
                                              }).ToListAsync();

            List<AreaOfInterest> AreaOfInterestList = new List<AreaOfInterest>();

            foreach (var item in AreaOfInterests)
            {
                AreaOfInterestList.Add(new AreaOfInterest { AreaOfInterestId = item.AreaOfInterestId, InterestTitle = item.InterestTitle });
            }

            if (AreaOfInterests.Count != 0)
                return AreaOfInterestList.AsQueryable();
            else
                return null;

        }

    }
}
