using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineShopping.Models.Entity;
using MvcOnlineShopping.Models;
namespace MvcOnlineShopping.Controllers
{
    [AllowAnonymous]
    public class MemberLoginController : Controller
    {
        // GET: MemberLogin
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        [HttpGet]
        public ActionResult Login(string Member)
        {
            ViewBag.value2 = Member;
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLMember m)
        {
            var values = db.TBLMember.FirstOrDefault(x => x.Email == m.Email && x.Password == m.Password);
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.Email, false);
                Session["Email"] = values.Email.ToString();
                Session["Id"] = values.Id.ToString();
                Session["Name"] = values.Name.ToString();
                Session["Surname"] = values.Surname.ToString();
                return RedirectToAction("ShoppingHomePage", "MemberHomePage", new { Member = "0" });
            }
            else
            {
                return RedirectToAction("Login", "MemberLogin", new { Member = "1" });
            }
        }
        [HttpGet]
        public ActionResult Register(string Member)
        {
            ViewBag.value1 = Member;
            return View();
        }
        [HttpPost]
        public ActionResult Register(TBLMember m)
        {
            db.TBLMember.Add(m);
            db.SaveChanges();
            return RedirectToAction("Login", "MemberLogin", new { Member = "2" });
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "MemberLogin");
        }
        SendMail sendmail = new SendMail();
        [HttpGet]
        public ActionResult ResetPassword(string Member)
        {
            ViewBag.value = Member;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(TBLMember m)
        {
            var bilgiler = db.TBLMember.Where(x => x.Email == m.Email && x.Recovery == m.Recovery).FirstOrDefault();
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                bilgiler.Password = m.Password;
                db.SaveChanges();
                return RedirectToAction("ResetPassword", "MemberLogin", new { Member = "3" });
            }
            else
            {

                return RedirectToAction("ResetPassword", "MemberLogin", new { Member = "2" });
            }
        }
    }
}