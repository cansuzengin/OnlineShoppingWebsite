using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineShopping.Models.Entity;

namespace MvcOnlineShopping.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(string Admin)
        {
            ViewBag.value = Admin;
            return View();
        }
        public ActionResult UpdatePassword(TBLAdmin a, string Password2)
        {
            var values = db.TBLAdmin.Where(x => x.Password == a.Password).FirstOrDefault();
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.Email, false);
                values.Password = Password2;
                db.SaveChanges();
                return RedirectToAction("Index", "Settings", new { Admin = "1" });
            }
            else
            {
                return RedirectToAction("Index", "Settings", new { Admin = "2" });
            }

        }
    }
}