using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class CustomerAccountController : Controller
    {
        // GET: Admin/CustomerAccount
        private MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index(int id)
        {
            var account = db.KhachHangs.FirstOrDefault(m => m.MaKH == id);
            return View(account);
        }

        public ActionResult Update(int id)
        {
            var account = db.KhachHangs.FirstOrDefault(m => m.MaKH == id);
            return View(account);
        }
        [HttpPost]
        public ActionResult Update(KhachHang khachHang)
        {
            var item = db.KhachHangs.FirstOrDefault(m => m.MaKH == khachHang.MaKH);
            item.TenDangNhap = khachHang.TenDangNhap;
            item.MatKhau = khachHang.MatKhau;
            db.SubmitChanges();
            return RedirectToAction("Index",khachHang.MaKH);
        }
    }
}