using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(x => x.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());

        }

        [HttpGet]
        public ActionResult YeniMusteri() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var musteriler = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteriler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(TblMusteriler p1)
        {
            var mus = db.TblMusteriler.Find(p1.MusteriId);
            mus.MusteriAd = p1.MusteriAd;
            mus.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}