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

        public ActionResult Update(int id)
        {
            var item = db.SanPhams.FirstOrDefault(m => m.MaSP == id);
            ViewBag.Categories = db.NhomHangs.ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(SanPham sanPham, HttpPostedFileBase ImageUpload)
        {
            var sanPhamDb = db.SanPhams.FirstOrDefault(m => m.MaSP == sanPham.MaSP);
            sanPhamDb.TenSP = sanPham.TenSP;
            sanPhamDb.DonGia = sanPham.DonGia;
            sanPhamDb.DonViTinh = sanPham.DonViTinh;
            sanPhamDb.MaNH = sanPham.MaNH;
            sanPhamDb.SoLuongTon = sanPham.SoLuongTon;

            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var item = db.SanPhams.FirstOrDefault(m => m.MaSP == id);
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            var item = db.SanPhams.FirstOrDefault(m => m.MaSP == id);
            db.SanPhams.DeleteOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}
