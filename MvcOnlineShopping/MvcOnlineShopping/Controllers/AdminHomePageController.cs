using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineShopping.Models.Entity;
namespace MvcOnlineShopping.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class AdminHomePageController : Controller
    {
        // GET: AdminHomePage        
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var userMail = (string)Session["Email"];
            var degerler = db.TBLAdmin.Where(z => z.Email == userMail).FirstOrDefault();
            ViewBag.name = db.TBLAdmin.Where(z => z.Email == userMail).Select(x => x.Name).FirstOrDefault();
            ViewBag.surname = db.TBLAdmin.Where(z => z.Email == userMail).Select(x => x.Surname).FirstOrDefault();
            ViewBag.image = db.TBLAdmin.Where(z => z.Email == userMail).Select(x => x.Image).FirstOrDefault();
            Session["image"] = db.TBLAdmin.Where(z => z.Email == userMail).Select(x => x.Image).FirstOrDefault();
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(TBLAdmin a)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLAdmin.Where(z => z.Email == userMail).FirstOrDefault();
            user.Name = a.Name;
            user.Surname = a.Surname;
            user.Email = a.Email;
            user.Password = a.Password;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}