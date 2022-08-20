using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;
using static VoThiHoangOanh.Common;

namespace VoThiHoangOanh.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangEntities7 objWebBanHangEntities = new WebBanHangEntities7();

        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objWebBanHangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebBanHangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        
        [ValidateInput(false)]

        [HttpPost]
        public ActionResult Create(Product objProduct)

        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        //ten hinh
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        //jpg
                        fileName = fileName + extension;
                        //ten hinh.jpg
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWebBanHangEntities.Products.Add(objProduct);
                    objWebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch 
                {
                    return View(objProduct);

                }
                
            }

            return View(objProduct);
        }

        void LoadData()
        {
            Common objCommon = new Common();
            // lấy dữ liệu dưới db
            var lstCat = objWebBanHangEntities.Categories.ToList();
            //convert  sang select list dạng value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");
            // lấy dữ diệu thương hiệu dưới db
            var lstBrand = objWebBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loai san pham
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dang value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();

            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebBanHangEntities.Products.Remove(objProduct);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();

            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();

            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product objProduct)
        {

            this.LoadData();
            if (objProduct.ImageUpLoad!=null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                String extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            objWebBanHangEntities.Entry(objProduct).State = System.Data.Entity.EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");

        }

       
    }
}