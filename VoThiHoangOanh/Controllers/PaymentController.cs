using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;

namespace VoThiHoangOanh.Controllers
{
    public class PaymentController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();

        // GET: Payment
        public ActionResult Index()
        {
            if(Session["idUser"]==null)
            {
                return RedirectToAction("Login", "Home");

            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                //gan du lieu cho bang Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebBanHangEntities.Orders.Add(objOrder);
                //luu vao bang Order
                objWebBanHangEntities.SaveChanges();
                //Lay OrderId vua tao luu vao bang OrderDetail
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objWebBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}

        
    
