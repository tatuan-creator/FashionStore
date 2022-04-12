using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        public HoaDon hoaDon = new HoaDon();
        public ActionResult Index()
        {   
            
            var all_sp = db.SanPhams.ToList();
            return View(all_sp);
        }
        
        public ActionResult Login()
        {
            return View(new KhachHang());
        }
        [HttpPost]
        public ActionResult Login(KhachHang khachHang)
        {
            var loginCustomer = db.KhachHangs.FirstOrDefault(m => m.TenDangNhap == khachHang.TenDangNhap);
            if(loginCustomer != null)
            {
                if (khachHang.TenDangNhap == loginCustomer.TenDangNhap && khachHang.MatKhau == loginCustomer.MatKhau)
                {
                    Session["KhachHang"] = loginCustomer;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Thông tin tài khoản sai";
                }
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản không tồn tại";
            }    
            return View(new KhachHang());
        }
        public ActionResult Create()
        {
            return View(new KhachHang());
        }

        [HttpPost]
        public ActionResult Create(KhachHang khachHang)
        {
            khachHang.MaTT = 1;
            khachHang.TongTien = 0;
            db.KhachHangs.InsertOnSubmit(khachHang);
            db.SubmitChanges();
            return RedirectToAction("Login");
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            var kh = Session["KhachHang"] as KhachHang;
            if(kh == null)
            {
                return RedirectToAction("Login");
            }
            //kh.CT_GioHangs.ToList();

            bool checkProductIsExistsInCart = false;
            CT_GioHang gioHang = new CT_GioHang();
            gioHang.MaSP = id;
            gioHang.MaKH = kh.MaKH;
            gioHang.SoLuong = quantity;

            List<CT_GioHang> carts = db.CT_GioHangs.ToList();
            foreach(var item in carts)
            {
                if(item.MaSP == id && item.MaKH == kh.MaKH)
                {
                    checkProductIsExistsInCart = true;
                    item.SoLuong += quantity;
                }
            }
            if(!checkProductIsExistsInCart)
            {
                db.CT_GioHangs.InsertOnSubmit(gioHang);
            }

            db.SubmitChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult ShopingBag()
        {
            var kh = Session["KhachHang"] as KhachHang;
            if (kh == null)
            {
                return RedirectToAction("Login");
            }

            var lstShopingBag = db.CT_GioHangs.Where(m => m.MaKH == kh.MaKH);
            foreach(var item in lstShopingBag)
            {
                item.ThanhTien = item.SanPham.DonGia * item.SoLuong;
            }
            ViewBag.customer = kh.TenKH;
            return View(lstShopingBag);
        }

        public ActionResult Delete(int id)
        {
            var cart = db.CT_GioHangs.FirstOrDefault(m => m.CartID == id);
            db.CT_GioHangs.DeleteOnSubmit(cart);
            db.SubmitChanges();
            return RedirectToAction("ShopingBag");
        }

        public ActionResult Buy(int id)
        {
            ViewBag.httt = db.HinhThucThanhToans.Where(m => m.TrangThaiTT.TenTrangThai == "Hoạt Động").ToList();
            var cart = db.CT_GioHangs.FirstOrDefault(m => m.CartID == id);
            var hd = new HoaDon();
            hd.NgayLapHD = DateTime.Now;
            hd.MaKH = cart.MaKH;
            hd.TongTienHang = cart.SanPham.DonGia * cart.SoLuong;
            hd.PhiVanChuyen = 15000;
            hd.GiamGia = 0;
            hd.TongHoaDon = hd.TongTienHang + hd.PhiVanChuyen - hd.GiamGia;

            return View(hd);
        }
        [HttpPost]
        public ActionResult Buy(HoaDon gethoaDon, int id)
        {
            var cart = db.CT_GioHangs.FirstOrDefault(m => m.CartID == id);
            var hd = new HoaDon();
            hd.NgayLapHD = DateTime.Now;
            hd.MaKH = cart.MaKH;
            hd.TongTienHang = cart.SanPham.DonGia * cart.SoLuong;
            hd.PhiVanChuyen = 15000;
            hd.GiamGia = 0;
            hd.TongHoaDon = hd.TongTienHang + hd.PhiVanChuyen - hd.GiamGia;
            hd.MaHT = gethoaDon.MaHT;
            db.HoaDons.InsertOnSubmit(hd);
            db.CT_GioHangs.DeleteOnSubmit(cart);
            db.SubmitChanges();
            return RedirectToAction("ShopingBag");
        }
    }
}