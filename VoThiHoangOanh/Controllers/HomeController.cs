using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;


namespace VoThiHoangOanh.Controllers
{
    
    public class HomeController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();

            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();

            return View(objHomeModel);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
    }

}
