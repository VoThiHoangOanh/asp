using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;

namespace VoThiHoangOanh.Controllers
{
    
    public class ProductController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}