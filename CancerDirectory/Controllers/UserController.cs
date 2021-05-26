using Newtonsoft.Json;
using OA.Data.Common;
using OA.Service.UserServices;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OA.Data.Helper;
using System.Diagnostics;
using System.Net;
using OA.Data.DTO_Model;
using System.Text;
using System.Web.Security;

namespace CancerDirectory.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
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
        //Login View Controller


        public ActionResult dtt()
        {
            return View();
        }

        public ActionResult Login()
        {

            HttpCookie cookie = Request.Cookies["UserCookie"];
            if(cookie != null)
            {
                string encryptedPass = cookie["password"].ToString();
                byte[] pass = Convert.FromBase64String(encryptedPass);
                string decryptedPass = ASCIIEncoding.ASCII.GetString(pass);
                ViewBag.email = cookie["email"].ToString();
                ViewBag.password = decryptedPass.ToString();
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> Login(string email, string password, bool RememberMe)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var EmailAdressCheck = Common.IsValidEmail(email.ToLower());
                    if (!EmailAdressCheck.Status.Equals("Success"))
                        return JsonConvert.SerializeObject(EmailAdressCheck);


                    var Result = await _userService.Login(email.ToLower(), password, UserType.Admin, "", "");
                    var User = (User)Result.Body;

                    if (Result.Status == "Success")
                    {
                        Session["Token"] = Result.AccessToken;
                        Session["UserId"] = User.UserId;
                        FormsAuthentication.SetAuthCookie(User.FirstName, false);

                        HttpCookie cookie = new HttpCookie("UserCookie");
                        if (RememberMe == true)
                        {
                            byte[] pass = ASCIIEncoding.ASCII.GetBytes(password);
                            string encryptedPass = Convert.ToBase64String(pass);
                            cookie["email"] = email;
                            cookie["password"] = encryptedPass;
                            cookie.Expires = DateTime.Now.AddHours(3);
                            HttpContext.Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Response.Cookies.Add(cookie);
                        }
                    }

                    var data = new ResponseModel { Code = Result.Code, Status = Result.Status, Message = Result.Message, Body = Result.Body, AccessToken = Result.AccessToken };
                    //    return JsonConvert.SerializeObject(data);
                    return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                else
                {
                    return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
                }
            }

            catch (Exception ex)
            {
                //  return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> ChangePassword(ChangePasswordDTO model)
        {
            try
            {
                //if (Session["UserId"] == null)
                //    return RedirectToAction("Login");

                var PasswordValidation = Common.CheckPasswordValid(model.NewPassword, model.ConfirmPassword);
                if (!PasswordValidation.Status.Equals("Success"))
                {
                  //  ViewBag.Error = PasswordValidation.Message;
                    return JsonConvert.SerializeObject(PasswordValidation);
                }

                model.UserId = (Guid) Session["UserId"];
                return JsonConvert.SerializeObject(await _userService.ChangePassword(model));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
     //   [ValidateInput(false)]
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

                    ResponseModel EmailResult = Mailer.SendEmail(email, _htmlBody, "Reset your Password - BCM");
                    if (EmailResult.Code == "200")
                    {
                        var Result = await _userService.InsertForgotLink(PasswordLinks);
                        if (Result.Code == "200")
                            return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Success", ConstantMessages.ChangePasswordMail, "200", null, ""));
                        else
                            return JsonConvert.SerializeObject(Result);
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


        //[HttpGet]
        //public async Task<ActionResult> VerifyLink(string id)
        //{
        //    try
        //    {
        //        var verify = await _userService.VerifyForgotPasswordLink(id);
        //        if (verify.Code == "200")
        //        {
        //            ViewBag.Link = id;
        //            return View("ResetPassword");
        //        }
        //        else
        //        {
        //            ViewBag.Status = "Sorry, Your Password reset link has expired.";
        //            return View("Confirmation");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, null));
        //    }
        //}



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


        //[HttpPost]
        //public async Task<string> UpdatePassword(ChangePasswordDTO model)
        //{
        //    try
        //    {
        //        model.UserId = (Guid) Session["UserId"];
        //        return JsonConvert.SerializeObject(await _userService.ChangePassword(model));
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> UpdatePassword()
        {
            try
            {
                string Link = Request.Form["Link"];
                var PasswordValidation = Common.CheckPasswordValid(Request.Form["NewPass"], Request.Form["ConfirmPass"]);
                if (!PasswordValidation.Status.Equals("Success"))
                {
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
        public JsonResult Logout()
        {
            HttpContext.Session.Clear();
            FormsAuthentication.SignOut();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult MainDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ViewUser(Guid userId)
        {
            try
            {
                if(userId != null)
                {
                    var Result = await _userService.GetUserBy(userId);

                    var _AreaOfInterestList = await _userService.GetAreaOfInterestById(userId);
                    ViewBag.AreaOfInterestList = _AreaOfInterestList.Body;

                    return View(Result.Body);
                }
               
            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.Message);
            }
                return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> ViewUser(User user)
        {
            try
            {
                if (user != null)
                {
                    var Result = await _userService.EditUserStatus(user);
                    if(Result.Code == "200")
                    {
                        var data = new ResponseModel { Code = Result.Code, Status = Result.Status, Message = Result.Message, Body = Result.Body, AccessToken = Result.AccessToken };
                        return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                    //return View(Result.Body);
                  //  return JsonConvert.SerializeObject(true, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
            }
            catch (Exception ex)
            {
              
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }

        }

        //action not in 
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditUser(Guid userId)
        {
            try
            {
                if (User != null)
                {
                    var Result = await _userService.GetUserBy(userId);

                    return View(Result.Body);
                }

            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.Message);
            }
            return View();
        }

        //action not in 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> EditUser(User user)
        {
            try
            {
                if (user != null)
                {
                    //var Result = await _userService.GetUserBy(userId);

                    //return View(Result.Body);
                }

            }
            catch (Exception ex)
            {

                Trace.TraceError(ex.Message);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> ManagemenListAndsearch()
        {
            try
            {
                var Result = await _userService.GetAllUsers();
                var users = Result.OrderByDescending(x => x.CreatedDate).ToList();

                
                //foreach (var item in users)
                //{
                //    if(!String.IsNullOrEmpty(item.ProfileImage))
                //    {
                //        item.ProfileImage = TripleDESCryptography.Decrypt(item.ProfileImage);
                //    }
                    
                //}

                return View(users);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            return View();
        }        

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> markVerifiedUser(IEnumerable<string> checkedList)
        {
            try
            {
                var result = new ResponseModel();
                if(checkedList != null)
                {
                    foreach (var item in checkedList)
                    {
                        var Userid = new Guid(item);
                        var user = (User)(await _userService.GetUserBy(Userid)).Body;
                        user.UserStatus = UserStatus.Verified;
                        var data = await _userService.UpdateUser(user);
                        if(data.Code == "200")
                        {
                            //var _htmlBody = Mailer. ContactUsPopulateBody(user.FirstName + " " + user.LastName, user.Email, message, "~/content/templates/contactus.html");
                            var _htmlBody = Mailer.VerifyUserByAdmin(user.LastName, "~/content/templates/verifyuserbyadmin.html");
                            var toemail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
                            //ResponseModel EmailResult = Mailer.SendEmail(toemail, _htmlBody, subject);
                            ResponseModel emailResult = Mailer.SendEmail(user.Email, _htmlBody, "You're now a Verified user");
                        }
                        result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = user.AccessToken };
                    }
                    //var model = await _userService.GetAllUsers();
                    //var users = model.ToList();
                    return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
                //    return JsonConvert.SerializeObject(result);
                //}

                //return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", ConstantMessages.ModelIsNotValid, "400", null, ""));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
       
        }

    }
}