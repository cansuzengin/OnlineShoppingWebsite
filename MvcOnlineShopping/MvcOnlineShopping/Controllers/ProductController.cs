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
    public class ProductController : Controller
    {
        // GET: Product
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var products = from k in db.TBLProduct select k;
            if (!string.IsNullOrEmpty(p))
            {
                products = products.Where(m => m.Name.Contains(p));
            }
            return View(products.ToList().ToPagedList(sayfa, 5));
        }
        [HttpGet]
        public ActionResult AddingProduct()
        {
            List<SelectListItem> category = (from i in db.TBLCategory.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Name,
                                                 Value = i.Id.ToString()
                                             }).ToList();
            ViewBag.values3 = category;
            List<SelectListItem> brand = (from i in db.TBLBrand.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Name,
                                                 Value = i.Id.ToString()
                                             }).ToList();
            ViewBag.values4 = brand;
            List<SelectListItem> gender = (from i in db.TBLGender.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.Id.ToString()
                                          }).ToList();
            ViewBag.values5 = gender;
            return View();
        }
        [HttpPost]
        public ActionResult AddingProduct(TBLProduct p)
        {
            var ctg = db.TBLCategory.Where(k => k.Id == p.TBLCategory.Id).FirstOrDefault();
            var gnd = db.TBLGender.Where(k => k.Id == p.TBLGender.Id).FirstOrDefault();
            var brd = db.TBLBrand.Where(k => k.Id == p.TBLBrand.Id).FirstOrDefault();
            p.TBLCategory = ctg;
            p.TBLGender = gnd;
            p.TBLBrand = brd;
            db.TBLProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveProduct(int Id)
        {
            var product = db.TBLProduct.Find(Id);
            db.TBLProduct.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FetchProduct(int Id)
        {
            var product = db.TBLProduct.Find(Id);
            List<SelectListItem> category = (from i in db.TBLCategory.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.Name,
                                                  Value = i.Id.ToString()
                                              }).ToList();
            ViewBag.values5 = category;
            List<SelectListItem> brand = (from i in db.TBLBrand.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.Name,
                                                  Value = i.Id.ToString()
                                              }).ToList();
            ViewBag.values6 = brand;
            return View("FetchProduct", product);
        }
        public ActionResult UpdateProduct(TBLProduct p)
        {
            var product = db.TBLProduct.Find(p.Id);
            product.Name = p.Name;
            product.Price = p.Price;
            product.DateOfAdding = p.DateOfAdding;
            product.Color = p.Color;
            product.Stock = p.Stock;
            var category = db.TBLCategory.Where(k => k.Id == p.TBLCategory.Id).FirstOrDefault();
            var brand = db.TBLBrand.Where(k => k.Id == p.TBLBrand.Id).FirstOrDefault();
            product.CategoryId = category.Id;
            product.BrandId = brand.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ImageProduct(int Id)
        {
            var products = db.TBLProduct.Where(x => x.Id == Id).ToList();
            ViewBag.product = products.Select(z => z.Name).FirstOrDefault();
            return View(products);
        }
    }
}