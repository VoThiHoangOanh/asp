using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User _user)
        {
            //Kiem tra va luu vao database
            if (ModelState.IsValid)
            {
                var check = objWebBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check==null)
                {
                    _user.Pssword = GetMD5(_user.Pssword);
                    objWebBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebBanHangEntities.Users.Add(_user);
                    objWebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string pssword)
        {
            if (ModelState.IsValid)
            {


                var f_pssword = GetMD5(pssword);
                var data = objWebBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Pssword.Equals(f_pssword)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //create a string MDS
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i=0;i<targetData.Length;i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }


    }

}
