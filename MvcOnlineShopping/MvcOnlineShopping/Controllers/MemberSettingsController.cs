using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineShopping.Models.Entity;

namespace MvcOnlineShopping.Controllers
{
    public class MemberSettingsController : Controller
    {
        // GET: MemberSettings
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(string Member)
        {
            ViewBag.value = Member;
            return View();
        }
        public ActionResult UpdatePassword(TBLMember a, string Password2)
        {
            var values = db.TBLMember.Where(x => x.Password == a.Password).FirstOrDefault();
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.Email, false);
                values.Password = Password2;
                db.SaveChanges();
                return RedirectToAction("Index", "MemberSettings", new { Member = "1" });
            }
            else
            {
                return RedirectToAction("Index", "MemberSettings", new { Member = "2" });
            }

        }
    }
}