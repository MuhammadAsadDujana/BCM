using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CancerDirectoryAPI.Controllers
{
    public class BaseController : Controller
    {
        public Guid UserId;

        public string UserToken;

        public User CurrentUser;
    }
}