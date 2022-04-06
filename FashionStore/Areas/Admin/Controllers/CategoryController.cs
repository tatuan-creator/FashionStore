using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            var categories = db.NhomHangs.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View(new NhomHang());
        }

        [HttpPost]
        public ActionResult Create(NhomHang nhomHang)
        {
            db.NhomHangs.InsertOnSubmit(nhomHang);  
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var item = db.NhomHangs.FirstOrDefault(m => m.MaNH == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(NhomHang nhomHang)
        {
            var item = db.NhomHangs.FirstOrDefault(m => m.MaNH == nhomHang.MaNH);
            item.TenNH = nhomHang.TenNH;
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.NhomHangs.FirstOrDefault(m => m.MaNH == id);
            db.NhomHangs.DeleteOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}