using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        private MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            List<KhachHang> customers = db.KhachHangs.ToList();
            return View(customers);
        }

        public ActionResult Create()
        {
            ViewBag.CustomerStatus = db.TrangThaiKHs.ToList();
            return View(new KhachHang());
        }

        [HttpPost]
        public ActionResult Create(KhachHang khachHang)
        {
            khachHang.TongTien = 0;
            db.KhachHangs.InsertOnSubmit(khachHang);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var item = db.KhachHangs.FirstOrDefault(m => m.MaKH == id);
            ViewBag.CustomerStatus = db.TrangThaiKHs.ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(KhachHang khachHang)
        {
            var item = db.KhachHangs.FirstOrDefault(m => m.MaKH == khachHang.MaKH);
            item.TenKH = khachHang.TenKH;
            item.DiaChi = khachHang.DiaChi;
            item.SDT = khachHang.SDT;
            item.Email = khachHang.Email;
            item.TongTien = khachHang.TongTien;
            item.TenDangNhap = khachHang.TenDangNhap;
            item.MatKhau = khachHang.MatKhau;
            item.MaTT = khachHang.MaTT;
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.KhachHangs.FirstOrDefault(m => m.MaKH == id);
            db.KhachHangs.DeleteOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
}