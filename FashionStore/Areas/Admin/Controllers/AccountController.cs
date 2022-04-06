using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        private MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            var accounts = db.TaiKhoans.ToList();
            return View(accounts);
        }

        public ActionResult Create()
        {
            return View(new TaiKhoan());
        }

        [HttpPost]
        public ActionResult Create(TaiKhoan taiKhoan)
        {
            db.TaiKhoans.InsertOnSubmit(taiKhoan);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(string tentaikhoan)
        {
            var item = db.TaiKhoans.FirstOrDefault(m => m.TenTK == tentaikhoan);
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(TaiKhoan taiKhoan)
        {
            var taikhoandb = db.TaiKhoans.FirstOrDefault(m => m.TenTK == taiKhoan.TenTK);
            taikhoandb.TenTK = taiKhoan.TenTK;
            taikhoandb.MatKhau = taiKhoan.MatKhau;
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string tentaikhoan)
        {
            var item = db.TaiKhoans.FirstOrDefault(m => m.TenTK == tentaikhoan);
            db.TaiKhoans.DeleteOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}
