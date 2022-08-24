using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;

namespace VoThiHoangOanh.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var listOrder = objWebBanHangEntities.Orders.ToList();
            return View(listOrder);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = objWebBanHangEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(order);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objorder = objWebBanHangEntities.Orders.Where(n => n.Id == Id).FirstOrDefault();

            return View(objorder);
        }
        [HttpPost]
        public ActionResult Delete(Order objor)
        {

            var objorder = objWebBanHangEntities.Orders.Where(n => n.Id == objor.Id).FirstOrDefault();
            objWebBanHangEntities.Orders.Remove(objorder);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}