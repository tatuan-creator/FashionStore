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
        public int login = -1;
        MyDataDataContext db = new MyDataDataContext();
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
                    Session["login"] = loginCustomer;
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
    }
}