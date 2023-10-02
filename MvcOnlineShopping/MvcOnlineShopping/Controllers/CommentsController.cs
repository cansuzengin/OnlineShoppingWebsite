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
    public class CommentsController : Controller
    {
        // GET: Comments
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var comments = from k in db.TBLComments where k.Status == false select k;
            return View(comments.ToList().ToPagedList(sayfa, 5));
        }
        public ActionResult Comment(int Id)
        {
            var comment = db.TBLComments.Find(Id);
            comment.Status = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ActiveComment()
        {
            var values = db.TBLComments.Where(x => x.Status == true).ToList();
            return View(values);
        }
        public ActionResult PasiveComment(int Id)
        {
            var comment = db.TBLComments.Find(Id);
            comment.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}