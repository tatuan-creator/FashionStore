using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            var hoaDon = db.HoaDons.ToList();
            return View(hoaDon);
        }
    }
}