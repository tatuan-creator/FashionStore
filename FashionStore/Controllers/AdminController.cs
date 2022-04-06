using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult DangNhap()
        {
            var listadmin = data.TaiKhoans.ToList();
            return View(listadmin);
        }
    }
}