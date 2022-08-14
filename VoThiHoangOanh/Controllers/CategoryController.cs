using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;
namespace VoThiHoangOanh.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();
        public ActionResult Index()
        {
            var lstCategory = objWebBanHangEntities.Categories.ToList();
            return View(lstCategory);
        }
    }
    
}
