using CancerDirectoryAPI.Attributes;
using Newtonsoft.Json;
using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Service.UserServices;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CancerDirectoryAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET: User

        [HttpPost]
        public async Task<string> Signup(UserModel userModel)
        {
            try
            {
                var EmailAdressCheck = Common.IsValidEmail(userModel.Email);
                if (!EmailAdressCheck.Status.Equals("Success"))
                    return JsonConvert.SerializeObject(EmailAdressCheck);

                var user = (User)(await _userService.VerifyEmail(userModel.Email)).Body;
                if (user != null)
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.EmailExist, "400", null, ""));

                User NewUser = new User
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email.ToLower(),
                    //AreadOfInterestId = userModel.AreadOfInterestId,
                    CountryId = userModel.CountryId,
                    StateId = userModel.StateId,
                    //CityId = userModel.CityId,
                    City = userModel.City,
                    Zip = userModel.Zip,
                    Bio = userModel.Bio,
                    Specialty = userModel.Specialty, //by saad
                    WorkAt = userModel.WorkAt,
                    Address = userModel.Address,
                    ProfileImage = Images.IsBase64(userModel.ProfileImage) == false ? null : Images.Base64ToImage(userModel.ProfileImage, Server),
                    //ProfileImage = userModel.ProfileImage == "" ? null : Images.Base64ToImage(userModel.ProfileImage, Server),
                    PhoneNumber = userModel.PhoneNumber,
                    UserStatus = UserStatus.UnVerified,
                    UserType = UserType.User,
                    Password = Encryption.EncodePasswordToBase64(userModel.Password),
                    CreatedDate = DateTime.Now,
                    TokenIssueDate = DateTime.Now,
                    TokenExpiryDate = DateTime.Now,
                    IsActive = true
                };

                var Result = await _userService.InsertUser(NewUser);
                if (Result.Code == "200")
                {
                    Guid UserId = ((User)Result.Body).UserId;
                    var response = await _userService.InsertUserAreaOfInterest(UserId, userModel.AreaOfInterestIds);

                    var responseLogin = await _userService.Login(userModel.Email, userModel.Password, UserType.User, "", "");
                    if (responseLogin.Code == "200")
                    {
                        var result = new ResponseModel { Code = "200", Body = responseLogin.Body, Message = ConstantMessages.RegistrationSuccess, Status = "Success", AccessToken = "Token will find here" };
                        return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                    else
                        return JsonConvert.SerializeObject(responseLogin, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                else
                    return JsonConvert.SerializeObject(Result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }

        }

        [HttpPost]
        [CacheFilter(120, 120, false)]
        public async Task<string> Login(string email, string password, string devicetype, string devicetoken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var EmailAdressCheck = Common.IsValidEmail(email.ToLower());
                    if (!EmailAdressCheck.Status.Equals("Success"))
                        return JsonConvert.SerializeObject(EmailAdressCheck);

                    var Result = await _userService.Login(email.ToLower(), password, UserType.User, devicetype, devicetoken);
                    //var user = (User)(Result.Body);
                    //user.ProfileImage = ""; //working...

                    var data = new ResponseModel { Code = Result.Code, Status = Result.Status, Message = Result.Message, Body = Result.Body, AccessToken = Result.AccessToken };
                    return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                else
                {
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        public async Task<string> Logout()
        {
            try
            {
                var Result = await _userService.Logout(UserId);
                return JsonConvert.SerializeObject(Result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> ChangePassword(ChangePasswordDTO model)
        {
            try
            {
                model.UserId = UserId;
                var result = await _userService.ChangePassword(model);
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [CacheFilter(120, 120, false)]
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var EmailAdressCheck = Common.IsValidEmail(email);
                    if (!EmailAdressCheck.Status.Equals("Success"))
                        return JsonConvert.SerializeObject(EmailAdressCheck);

                    var user = (User)(await _userService.VerifyEmail(email)).Body;
                    if (user == null)
                        return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.EmailDoesNotExist, "400", null, ""));

                    var DynamicLink = Encryption.GenerateRandomString(32).ToUpper();
                    ForgotPasswordLinks PasswordLinks = new ForgotPasswordLinks
                    {
                        Email = email,
                        Link = DynamicLink,
                        UserId = user.UserId,
                        CreatedDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddDays(2),
                        IsActive = true
                    };

                    var reset_url = System.Configuration.ConfigurationManager.AppSettings["liveUrl"] + "User/VerifyLink/?id=" + DynamicLink;
                    var _htmlBody = Mailer.ResetPasswordPopulateBody(reset_url, "~/content/templates/resetpassword.html");

                    ResponseModel EmailResult = Mailer.SendEmail(email, _htmlBody, "Reset your Password of Cancer Directory App");
                    if (EmailResult.Code == "200")
                    {
                        var Result = await _userService.InsertForgotLink(PasswordLinks);
                        if (Result.Code == "200")
                            return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Success", ConstantMessages.ChangePasswordMail, "200", null, ""));
                        else
                            return JsonConvert.SerializeObject(Result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(EmailResult, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.ModelIsNotValid, "400", null, ""));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        public async Task<ActionResult> VerifyLink(string id)
        {
            try
            {
                var verify = await _userService.VerifyForgotPasswordLink(id);
                if (verify.Code == "200")
                {
                    ViewBag.Link = id;
                    return View("ResetPassword");
                }
                else
                {
                    ViewBag.Status = "Oh! The reset password link has expired. Please use Forgot Password option from the app once more and we will send you a new link for resetting your password.";
                    return View("Confirmation");
                }
            }
            catch (Exception ex)
            {
                return Json(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword()
        {
            try
            {
                string Link = Request.Form["Link"];
                var PasswordValidation = Common.CheckPasswordValid(Request.Form["NewPass"], Request.Form["ConfirmPass"]);
                if (!PasswordValidation.Status.Equals("Success"))
                {
                    ViewBag.Link = Link;
                    ViewBag.Error = PasswordValidation.Message;
                    return View("ResetPassword");
                }

                var Result = await _userService.ResetPassword(Link, Request.Form["NewPass"]);
                ViewBag.Status = Result.Message;
                return View("Confirmation");
            }
            catch (Exception ex)
            {
                return Json(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, null));
            }
        }

        [HttpGet]
        public async Task<string> GetAllAreaOfInterest()
        {
            try
            {
                var Result = await _userService.GetAllAreaOfInterest();
                return JsonConvert.SerializeObject(Result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetUserProfile()
        {
            try
            {
                var data = await _userService.GetUserBy(UserId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> UpdateUserProfile(UserModel userModel)
        {
            try
            {
                if (UserId == null)
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "User Not Found", "404", null, ""));

                var user = (User)(await _userService.GetUserBy(UserId)).Body;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                //user.AreadOfInterestId = userModel.AreadOfInterestId;
                user.CountryId = userModel.CountryId;
                user.StateId = userModel.StateId;
                user.City = userModel.City;
                user.Zip = userModel.Zip;
                user.Bio = userModel.Bio;
                user.Specialty = userModel.Specialty;

                user.WorkAt = userModel.WorkAt;
                user.Address = userModel.Address;
                user.ProfileImage = Images.IsBase64(userModel.ProfileImage) == false ? user.ProfileImage : Images.Base64ToImage(userModel.ProfileImage, Server);
                user.PhoneNumber = userModel.PhoneNumber;
                user.ModifiedDate = DateTime.Now;

                var data = await _userService.UpdateUser(user);
                if (data.Code == "200")
                {
                    if (userModel.AreaOfInterestIds.Count != 0)
                    {
                        User user1 = (User)data.Body;
                        var response = await _userService.UpdateUserAreaOfInterest(user1.UserId, userModel.AreaOfInterestIds);

                        User user2 = (User)response.Body;

                        user1.DisplayAreaOfInterestIds = user2.DisplayAreaOfInterestIds;
                        user1.DisplayAreaOfInterestTitle = user2.DisplayAreaOfInterestTitle;

                        var result = new ResponseModel { Code = response.Code, Status = response.Status, Message = response.Message, Body = user1, AccessToken = data.AccessToken };
                        return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Please provide area of interest", "404", null, ""));
                    }
                }
                else
                    return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> ContactUs(string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = (User)(await _userService.GetUserBy(UserId)).Body;

                    if (user == null)
                        return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.UserDoesNotExist, "404", null, ""));

                    var _htmlBody = Mailer.ContactUsPopulateBody(user.FirstName + " " + user.LastName, user.Email, message, "~/content/templates/contactus.html");

                    var toemail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
                    ResponseModel EmailResult = Mailer.SendEmail(toemail, _htmlBody, subject);
                    if (EmailResult.Code == "200")
                    {
                        return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Success", "OK", "200", "Your message successfully send", UserToken));
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(EmailResult);
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.ModelIsNotValid, "400", null, ""));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }


        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetUserStatus()
        {
            try
            {
                var Result = await _userService.GetUserStatus(UserId);
                return JsonConvert.SerializeObject(Result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }
    }
}