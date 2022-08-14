using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;

namespace VoThiHoangOanh.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var lstProduct = objWebBanHangEntities.Products.ToList();
            return View(lstProduct);
        }
    }
}