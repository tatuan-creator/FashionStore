using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private MyDataDataContext db = new MyDataDataContext();
        private const string SAVE_PATH = "/Content/images/";

        // GET: Admin/Product
        public ActionResult Index()
        {
            var products = db.SanPhams.ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ViewBag.ProductStatus = db.TrangThaiSPs.ToList();
            var banCategory = db.TrangThaiNHs.FirstOrDefault(m => m.TenTT == "Hoạt Động");
            ViewBag.Categories = db.NhomHangs.Where(m => m.MaTT == banCategory.MaTT).ToList();
            return View(new SanPham());
        }

        [HttpPost]
        public ActionResult Create(SanPham sanPham, HttpPostedFileBase ImageUpload)
        {
            if(ImageUpload != null)
            {
                string name = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                string fileName = name + DateTime.Now.ToString("dd0mm0yyyy0hh0mm0ss0mmm") + extension;
                sanPham.Hinh = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath(SAVE_PATH), fileName));
            }
            db.SanPhams.InsertOnSubmit(sanPham);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var item = db.SanPhams.FirstOrDefault(m => m.MaSP == id);
            var banCategory = db.TrangThaiNHs.FirstOrDefault(m => m.TenTT == "Hoạt Động");
            ViewBag.Categories = db.NhomHangs.Where(m => m.MaTT == banCategory.MaTT).ToList();
            ViewBag.ProductStatus = db.TrangThaiSPs.ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(SanPham sanPham, HttpPostedFileBase ImageUpload)
        {
            var sanPhamDb = db.SanPhams.FirstOrDefault(m => m.MaSP == sanPham.MaSP);

            if (ImageUpload != null)
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath(SAVE_PATH), sanPhamDb.Hinh));

                string name = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                string fileName = name + DateTime.Now.ToString("dd0mm0yyyy0hh0mm0ss0mmm") + extension;
                sanPhamDb.Hinh = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath(SAVE_PATH), fileName));
            }
            sanPhamDb.TenSP = sanPham.TenSP;
            sanPhamDb.DonGia = sanPham.DonGia;
            sanPhamDb.DonViTinh = sanPham.DonViTinh;
            sanPhamDb.MaNH = sanPham.MaNH;
            sanPhamDb.SoLuongTon = sanPham.SoLuongTon;
            sanPhamDb.MaTT = sanPham.MaTT;

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
            System.IO.File.Delete(Path.Combine(Server.MapPath(SAVE_PATH), item.Hinh));
            db.SanPhams.DeleteOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}