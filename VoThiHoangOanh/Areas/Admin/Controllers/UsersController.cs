using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;

namespace VoThiHoangOanh.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}