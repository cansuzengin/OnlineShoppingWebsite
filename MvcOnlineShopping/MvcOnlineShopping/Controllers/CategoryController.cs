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
   
    public class CategoryController : Controller
    {
        // GET: Category
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var categories = from k in db.TBLCategory where k.Status==true select k;
            if (!string.IsNullOrEmpty(p))
            {
                categories = categories.Where(m => m.Name.Contains(p));
            }
            return View(categories.ToList().ToPagedList(sayfa,5));
        }
        [HttpGet]
        public ActionResult AddingCategory()
        {
            List<SelectListItem> category = (from i in db.TBLGender.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.values = category;
            return View();
        }
        [HttpPost]
        public ActionResult AddingCategory(TBLCategory p)
        {
            p.Status = true;
            db.TBLCategory.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveCategory(int Id)
        {
            var value = db.TBLCategory.Find(Id);
            value.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FetchCategory(int id)
        {
            var category = db.TBLCategory.Find(id);
            return View("FetchCategory",category);
        }
        public ActionResult UpdateCategory(TBLCategory c)
        {
            var category = db.TBLCategory.Find(c.Id);
            category.Name = c.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PasiveCategory()
        {
            var values = db.TBLCategory.Where(x => x.Status == false).ToList();
            return View(values);
        }
        public ActionResult ActiveCategory(int Id)
        {
            var ktg = db.TBLCategory.Find(Id);
            ktg.Status = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}