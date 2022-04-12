using FashionStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionStore.Areas.Admin.Controllers
{
    public class PayController : Controller
    {
        // GET: Admin/Pay
        private MyDataDataContext db = new MyDataDataContext();
        public List<ThanhToan> GetDS()
        {
            List<ThanhToan> tt = new List<ThanhToan>();
            var PayTT = db.TrangThaiTTs.ToList();
            var PayHT = db.HinhThucThanhToans.ToList();
            foreach(var item in PayHT)
            {
                ThanhToan thanhtoan = new ThanhToan();
                thanhtoan.MaHT = item.MaHT;
                thanhtoan.TenHT = item.TenHT;
                thanhtoan.MaTT = (int)item.MaTT;
                thanhtoan.TenTT = PayTT.FirstOrDefault(p => p.MaTT == item.MaTT).TenTrangThai;
                tt.Add(thanhtoan);
            }
            return (tt);
        }
        public ActionResult Index()
        {
            var dsTT = GetDS();
            return View(dsTT);
        }

        public ActionResult Create()
        {
            ViewBag.PayStatus = db.TrangThaiTTs.ToList();
            return View(new HinhThucThanhToan());
        }
        [HttpPost]
        public ActionResult Create(HinhThucThanhToan ht)
        {
            db.HinhThucThanhToans.InsertOnSubmit(ht);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            ViewBag.PayStatus = db.TrangThaiTTs.ToList();
            var item = db.HinhThucThanhToans.FirstOrDefault(m => m.MaHT == id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(HinhThucThanhToan ht)
        {
            var taikhoandb = db.HinhThucThanhToans.FirstOrDefault(m => m.MaHT == ht.MaHT);
            taikhoandb.TenHT = ht.TenHT;
            taikhoandb.MaTT = ht.MaTT;
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.HinhThucThanhToans.FirstOrDefault(p => p.MaHT == id);
            db.HinhThucThanhToans.DeleteOnSubmit(item);
            return RedirectToAction("Index");
        }

    }
}