using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.UserServices
{
    [Export(typeof(IUserService))]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;


        public UserService() 
        {
            _userRepository = new UserRepository();
            _logRepository = new LogRepository();
        }
        public UserService(IUserRepository userRepository , ILogRepository logRepository)
        {
            this._userRepository = userRepository;
            this._logRepository = logRepository;
        }
        public ResponseModel Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var model = await _userRepository.GetAll();
                return model;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }
        public async Task<ResponseModel> GetUserBy(Guid Id)
        {
            try
            {
                var user = await _userRepository.Get(Id);
                if (user == null)
                {
            //        await _logRepository.InsertAsync(new Logs() { UserId = Id, Operation = "GetUserBy", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }


                //   await _logRepository.InsertAsync(new Logs() { UserId = Id, Operation = "GetUserBy", Description = "Get User Profile", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get User Profile", "200", user, user.AccessToken);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = Id, Operation = "GetUserBy", Description = "Exception: " + ex.Message, EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public string GetUserByName(string role)
        {
            try
            {
                var user = _userRepository.GetByName(role);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }
        public async Task<ResponseModel> InsertUser(User user)
        {
            try
            {
                var response = await _userRepository.InsertAsync(user);
                if (response.Code == "200")
                {
                    //List<UserAreaOfInterest> UserAreaOfInterest = new List<UserAreaOfInterest>();
                    //foreach (var item in ai)
                    //{
                    //    UserAreaOfInterest.Add(new UserAreaOfInterest { UserId = ((User)response.Body).UserId, AreaOfInterestId = item.AreaOfInterestId, IsActive = true, CreatedDate = DateTime.Now });
                    //}

                    //var ai_response = await _userRepository.InsertAreaOfInterestAsync(UserAreaOfInterest);
                    //if (ai_response.Code != "200")
                    //{
                    //    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "InsertAreaOfInterestAsync", Description = "Area of Interest insertion in database failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                    //    //return ResponseHandler.GetResponse("Success", ConstantMessages.RegistrationSuccess, "200", user, null);
                    //}

                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "InsertUser", Description = "User record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Success", ConstantMessages.RegistrationSuccess, "200", user, null);
                }
                else
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "InsertUser", Description = "User insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", ConstantMessages.RegistrationFailed, "408", user, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "InsertUser", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> UpdateUser(User user)
        {
            try
            {
                var response = await _userRepository.UpdateAsync(user);
                if (response.Code == "200")
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "UpdateAsync", Description = response.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Logout", "200", user, user.AccessToken);
                }
                else
                {

                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "UpdateAsync", Description = "User update Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", ConstantMessages.RegistrationFailed, "408", user, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "UpdateUser", Description = "Exception: " + ex.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", user.FirstName, user.LastName), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> Login(string email, string password, UserType userType, string devicetype, string devicetoken)
        {
            try
            {
                var users = await _userRepository.GetAll();
                if (users.Count() > 0)
                {
                    var IsvalidEmail = users.Where(x=> x.Email == email && x.UserType == userType).FirstOrDefault();
                    if (IsvalidEmail == null)
                    {
                        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Email address doesn't exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "Email address doesn't exists", "404", null, "");
                    }

                    var user = users.Where(x => x.Email == email && x.Password == Encryption.EncodePasswordToBase64(password) && x.UserType == userType).FirstOrDefault();
                    if (user == null)
                    {
                        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Password doesn't match", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "Unable to login. Either username or password is incorrect", "404", null, "");
                    }

                    //user.DisplayCountryName = user.Country.Name;
                    //user.DisplayStateTitle = user.State.StateTitle;

                    var ai = await _userRepository.GetAllAreaOfInterest();
                    if(user.UserAreaOfInterest.Count != 0)
                    {
                        foreach (var item in user.UserAreaOfInterest)
                        {
                            var InterestTitle = ai.Where(x => x.AreaOfInterestId == item.AreaOfInterestId).Select(z => z.InterestTitle).FirstOrDefault();
                            if (InterestTitle != null)
                            {
                                if (user.DisplayAreaOfInterestTitle == null)
                                {
                                    user.DisplayAreaOfInterestTitle = InterestTitle.ToString();
                                    user.DisplayAreaOfInterestIds = item.AreaOfInterestId.ToString();
                                }
                                else
                                {
                                    user.DisplayAreaOfInterestTitle += ", " + InterestTitle.ToString();
                                    user.DisplayAreaOfInterestIds += ", " + item.AreaOfInterestId.ToString();
                                }
                            }

                            //if (item.AreaOfInterest != null)
                            //{
                            //    if (user.DispalyAreaOfInterestTitle == null)
                            //    {
                            //        user.DispalyAreaOfInterestTitle = item.AreaOfInterest.InterestTitle;
                            //    }
                            //    else
                            //    {
                            //        user.DispalyAreaOfInterestTitle += ", " + item.AreaOfInterest.InterestTitle;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        user.DisplayAreaOfInterestTitle = "";
                        user.DisplayAreaOfInterestIds = "";
                    }
                    //user.DispalyAreaOfInterestTitle = user.AreaOfInterest.InterestTitle;

                    user.AccessToken = TokkenManager.GenerateToken(user.UserId.ToString());
                    user.TokenIssueDate = DateTime.Now;
                    user.TokenExpiryDate = DateTime.Now.AddDays(7);
                    user.DeviceType = devicetype;
                    user.DeviceToken = devicetoken;

                    await _userRepository.UpdateAsync(user);

                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "Login", EventType = EventType.Login, Description = string.Format("email login through = {0}, password entring {1} , user type {2}", email, password, userType), CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Login", "200", user, user.AccessToken);
                }
                else
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "User Authentication Failed", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                    return ResponseHandler.GetResponse("Failed", ConstantMessages.UserAuthenticationFailed, "404", null, "");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> Logout(Guid UserId)
        {
            try
            {
                var user = await _userRepository.Get(UserId);
                if (user != null)
                {
                    user.AccessToken = null;
                    user.TokenExpiryDate = user.TokenIssueDate;
                    await _userRepository.UpdateAsync(user);

                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "Logout", EventType = EventType.Logout, Description = "Logout", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Logout", "200", null, "");
                }
                else
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Logout", EventType = EventType.Logout, Description = "User Authentication Failed", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                    return ResponseHandler.GetResponse("Failed", ConstantMessages.UserAuthenticationFailed, "404", null, "");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Logout", EventType = EventType.Logout, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> ChangePassword(ChangePasswordDTO password)
        {
            try
            {
                var user = await _userRepository.Get(password.UserId);
                if (user == null || user.Password != Encryption.EncodePasswordToBase64(password.OldPassword))
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "ChangePassword", EventType = EventType.ChangePassowrd, Description = "Old password is not correct", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                    return new ResponseModel { Code = "400", Status = "Failed", Message = "Current password is not correct." };
                }

                if (password.NewPassword != password.ConfirmPassword)
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "ChangePassword", EventType = EventType.ChangePassowrd, Description = "Password and confirm password do not match", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                    return new ResponseModel { Code = "400", Status = "Failed", Message = "Password and confirm password do not match." };
                }

                if(password.NewPassword == password.OldPassword)
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "ChangePassword", EventType = EventType.ChangePassowrd, Description = "Password and confirm password do not match", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                    return new ResponseModel { Code = "400", Status = "Failed", Message = "New password should be different." };
                }

                user.Password = Encryption.EncodePasswordToBase64(password.NewPassword);
                await _userRepository.UpdateAsync(user);

                await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "ChangePassword", EventType = EventType.ChangePassowrd, Description = "Password Successfully updated", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Password has been Changed", "200", user, user.AccessToken);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "ChangePassowrd", EventType = EventType.ChangePassowrd, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> VerifyEmail(string EmailAddress)
        {
            try
            {
                var user = await _userRepository.VerifyEmail(EmailAddress);

                await _logRepository.InsertAsync(new Logs() { UserId = user.UserId, Operation = "VerifyEmail", EventType = EventType.VerifyEmail, Description = "User email verified", CreatedDate = DateTime.Now, CreatedBy = user.FirstName + "" + user.LastName, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Very User Email", "200", user, user.AccessToken);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "VerifyEmail", EventType = EventType.VerifyEmail, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> InsertForgotLink(ForgotPasswordLinks entity)
        {
            try
            {
                var response = await _userRepository.InsertForgotLinkAsync(entity);
                if (response.Code == "200")
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "InsertForgotLink", Description = "Forgot link inserted", EventType = EventType.ForgotEmail, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", entity, null);
                }
                else
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "InsertForgotLink", Description = "Forgot link insertion Failed", EventType = EventType.ForgotEmail, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "InsertForgotLink", Description = "Exception: " + ex.Message, EventType = EventType.ForgotEmail, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> VerifyForgotPasswordLink(string Link)
        {
            try
            {
                var response = await _userRepository.VerifyForgotPasswordLink(Link);

                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "VerifyForgotPasswordLink", EventType = EventType.VerifyForgotPasswordLink, Description = "Forgot Linkl verified", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "VerifyForgotPasswordLink", EventType = EventType.VerifyForgotPasswordLink, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> ResetPassword(string Link, string NewPassword)
        {
            try
            {
                var response = await _userRepository.ResetPassword(Link, NewPassword);

                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "ResetPassword", EventType = EventType.ResetPassword, Description = response.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "ResetPassword", EventType = EventType.ResetPassword, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> GetAllAreaOfInterest()
        {
            try
            {
                var data = await _userRepository.GetAllAreaOfInterest();
                if (data == null)
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllAreaOfInterest", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllAreaOfInterest", Description = "Successfully get area of interest list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllAreaOfInterest", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetUserStatus(Guid Id)
        {
            try
            {
                var user = await _userRepository.GetUserStatus(Id);

                await _logRepository.InsertAsync(new Logs() { UserId = Id, Operation = "GetUserStatus", Description = "Get User Status", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get User Status", "200", user, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = Id, Operation = "GetUserStatus", Description = "Exception: " + ex.Message, EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> InsertUserAreaOfInterest(Guid UserId, List<Guid> AreaOfInterestIds)
        {
            try
            {
                List<UserAreaOfInterest> UserAreaOfInterest = new List<UserAreaOfInterest>();
                for (int i = 0; i < AreaOfInterestIds.Count; i++)
                {
                    UserAreaOfInterest.Add(new UserAreaOfInterest { UserId = UserId, AreaOfInterestId = AreaOfInterestIds[i], IsActive = true, CreatedDate = DateTime.Now });
                }

                var ai_response = await _userRepository.InsertAreaOfInterestAsync(UserAreaOfInterest);

                await _logRepository.InsertAsync(new Logs() { UserId = UserId, Operation = "UpdateUserAreaOfInterest", Description = ai_response.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", null, "");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = UserId, Operation = "UpdateUserAreaOfInterest", Description = "Exception: " + ex.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> UpdateUserAreaOfInterest(Guid UserId, List<Guid> AreaOfInterestIds)
        {
            try
            {
                var response = await _userRepository.DeleteAreaOfInterestAsync(UserId);

                var DisplayAreaOfInterestIds = string.Empty;
                var DisplayAreaOfInterestTitle = string.Empty;

                var areaofinterest = new User();
                
                var ai = await _userRepository.GetAllAreaOfInterest();

                List<UserAreaOfInterest> UserAreaOfInterest = new List<UserAreaOfInterest>();
                for (int i = 0; i < AreaOfInterestIds.Count; i++)
                {
                    UserAreaOfInterest.Add(new UserAreaOfInterest { UserId = UserId, AreaOfInterestId = AreaOfInterestIds[i], IsActive = true, CreatedDate = DateTime.Now });

                    var InterestTitle = ai.Where(x => x.AreaOfInterestId == AreaOfInterestIds[i]).Select(z => z.InterestTitle).FirstOrDefault();
                    if (areaofinterest.DisplayAreaOfInterestTitle == null)
                    {
                        areaofinterest.DisplayAreaOfInterestTitle = InterestTitle.ToString();
                        areaofinterest.DisplayAreaOfInterestIds = AreaOfInterestIds[i].ToString();
                    }
                    else
                    {
                        areaofinterest.DisplayAreaOfInterestTitle += ", " + InterestTitle.ToString();
                        areaofinterest.DisplayAreaOfInterestIds += ", " + AreaOfInterestIds[i].ToString();
                    }
                }

                var ai_response = await _userRepository.InsertAreaOfInterestAsync(UserAreaOfInterest);

                await _logRepository.InsertAsync(new Logs() { UserId = UserId, Operation = "UpdateUserAreaOfInterest", Description = response.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", areaofinterest, "");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = UserId, Operation = "UpdateUserAreaOfInterest", Description = "Exception: " + ex.Message, EventType = EventType.Update, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> EditUserStatus(User user)
        {


            try
            {
                var response = await _userRepository.EditUserById(user);
                if (response.Code == "200")
                {
                    //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Edited", "200", user, null);
                }
                else
                {
                    //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", user, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> GetAreaOfInterestById(Guid Id)
        {
            try
            {
                var data = await _userRepository.GetAreaOfInterestList(Id);
                if (data == null)
                {
                    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }


    }
}
