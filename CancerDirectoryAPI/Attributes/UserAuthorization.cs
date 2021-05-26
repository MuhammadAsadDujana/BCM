using CancerDirectoryAPI.Controllers;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Service.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CancerDirectoryAPI.Attributes
{
    public class UserAuthorization : ActionFilterAttribute
    {
        public async override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try
            {
                BaseController baseController = (BaseController)actionContext.Controller;
                baseController.UserToken = actionContext.HttpContext.Request.Headers["Token"];

                if (!string.IsNullOrEmpty(baseController.UserToken))
                {
                    var Data = TokkenManager.ValidateToken(baseController.UserToken);
                    baseController.UserId = Guid.Parse(Data.Value);
                    UserService UserDataAccess = new UserService();
                    baseController.CurrentUser = (User)(await UserDataAccess.GetUserBy(baseController.UserId)).Body ;
                    if (baseController.CurrentUser == null)
                    {
                        actionContext.HttpContext.Response.StatusCode = 400;
                        actionContext.HttpContext.Response.StatusDescription = "User is not Autheticated";
                    }
                    else
                    {
                        if (baseController.CurrentUser.TokenExpiryDate < DateTime.Now)
                        {
                            actionContext.HttpContext.Response.StatusCode = 400;
                            actionContext.HttpContext.Response.StatusDescription = "Token Expired";
                        }
                    }
                }
                else
                {
                    actionContext.HttpContext.Response.StatusCode = 400;
                    actionContext.HttpContext.Response.StatusDescription = "User is not Authorized";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "IDX10223: Lifetime validation failed. The token is expired. ValidTo: '[PII is hidden]', Current time: '[PII is hidden]'.")
                {
                    actionContext.HttpContext.Response.StatusCode = 400;
                    actionContext.HttpContext.Response.StatusDescription = "Token Expired";
                }
                else
                {
                    actionContext.HttpContext.Response.StatusCode = 400;
                    actionContext.HttpContext.Response.StatusDescription = ex.Message.ToString();
                }
            }
        }

    }
}