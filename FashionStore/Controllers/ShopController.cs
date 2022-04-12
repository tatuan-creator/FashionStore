using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            ViewBag.Category = db.NhomHangs.ToList();
            return View();
        }   
    }
}