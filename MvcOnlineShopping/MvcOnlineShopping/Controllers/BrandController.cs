using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineShopping.Models.Entity;
namespace MvcOnlineShopping.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var brands = from k in db.TBLBrand where k.Status==true select k;
            if (!string.IsNullOrEmpty(p))
            {
                brands = brands.Where(m => m.Name.Contains(p));
            }
            return View(brands.ToList().ToPagedList(sayfa, 5));
        }
        [HttpGet]
        public ActionResult AddingBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddingBrand(TBLBrand b)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            b.Status = true;
            db.TBLBrand.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveBrand(int Id)
        {
            var brand = db.TBLBrand.Find(Id);
            brand.Status=false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FetchBrand(int Id)
        {
            var brand = db.TBLBrand.Find(Id);
            return View("FetchBrand", brand);
        }
        public ActionResult UpdateBrand(TBLBrand b)
        {
            var brand = db.TBLBrand.Find(b.Id);
            brand.Name = b.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PasiveBrand()
        {
            var values = db.TBLBrand.Where(x => x.Status == false).ToList();
            return View(values);
        }
        public ActionResult ActiveBrand(int Id)
        {
            var ktg = db.TBLBrand.Find(Id);
            ktg.Status = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}