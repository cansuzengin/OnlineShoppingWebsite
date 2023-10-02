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
    public class MemberController : Controller
    {
        // GET: Member
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var members = from k in db.TBLMember select k;
            if (!string.IsNullOrEmpty(p))
            {
                members = members.Where(m => m.Name.Contains(p));
            }
            return View(members.ToList().ToPagedList(sayfa, 5));
        }
        [HttpGet]
        public ActionResult AddingMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddingMember(TBLMember m)
        {
            db.TBLMember.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveMember(int Id)
        {
            var member = db.TBLMember.Find(Id);
            db.TBLMember.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FetchMember(int Id)
        {
            var member = db.TBLMember.Find(Id);
            return View("FetchMember", member);
        }
        public ActionResult UpdateMember(TBLMember m)
        {
            var member = db.TBLMember.Find(m.Id);
            member.Name = m.Name;
            member.Surname = m.Surname;
            member.Email = m.Email;
            member.Password = m.Password;
            member.Image = m.Image;
            member.Telephone = m.Telephone;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}