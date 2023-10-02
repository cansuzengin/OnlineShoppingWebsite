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
    public class AdminController : Controller
    {
        // GET: Admin
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var admins = from k in db.TBLAdmin select k;
            if (!string.IsNullOrEmpty(p))
            {
                admins = admins.Where(m => m.Name.Contains(p));
            }
            return View(admins.ToList().ToPagedList(sayfa, 5));
        }
        [HttpGet]
        public ActionResult AddingAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddingAdmin(TBLAdmin a)
        {
            db.TBLAdmin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveAdmin(int Id)
        {
            var admin = db.TBLAdmin.Find(Id);
            db.TBLAdmin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FetchAdmin(int Id)
        {
            var admin = db.TBLAdmin.Find(Id);
            return View("FetchAdmin", admin);
        }
        public ActionResult UpdateAdmin(TBLAdmin a)
        {
            var admin = db.TBLAdmin.Find(a.Id);
            admin.Name = a.Name;
            admin.Surname = a.Surname;
            admin.Email = a.Email;
            admin.Password = a.Password;
            admin.Authority = a.Authority;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}