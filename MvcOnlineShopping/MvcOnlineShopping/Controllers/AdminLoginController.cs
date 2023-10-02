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
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        [HttpGet]
        public ActionResult Login(string Admin)
        {
            ViewBag.value = Admin;
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLAdmin a)
        {
            var values = db.TBLAdmin.FirstOrDefault(x => x.Email == a.Email && x.Password == a.Password);
            if(values!=null)
            {
                FormsAuthentication.SetAuthCookie(values.Email,false);
                Session["Email"] = values.Email.ToString();
                return RedirectToAction("Index", "AdminHomePage", new { Admin = "0" });
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin", new { Admin = "1" });
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
        SendMail sendmail = new SendMail();
        [HttpGet]
        public ActionResult ResetPassword(string Admin)
        {
            ViewBag.value = Admin;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(TBLAdmin a)
        {
            //sendmail.Google(m.Email, m.Password, m.Email);
            //return RedirectToAction("ResetPassword", "AdminLogin", new { Admin = "3" });
            var bilgiler = db.TBLAdmin.Where(x => x.Email == a.Email && x.Recovery == a.Recovery).FirstOrDefault();
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                bilgiler.Password = a.Password;
                db.SaveChanges();
                return RedirectToAction("ResetPassword", "AdminLogin", new { Admin = "3" });
            }
            else
            {

                return RedirectToAction("ResetPassword", "AdminLogin", new { Admin = "2" });
            }
        }
    }
}