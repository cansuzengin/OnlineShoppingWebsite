using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineShopping.Models.Entity;
using MvcOnlineShopping.Models.Classes;

namespace MvcOnlineShopping.Controllers
{
    public class ConfirmCartController : Controller
    {
        // GET: ConfirmCart
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(int id)
        {
            ViewBag.id = id;

            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            Class1 cs = new Class1();
            cs.value3 = db.TBLBasket.Where(x => x.MemberId == user.Id).ToList();
            cs.value8 = db.TBLOrder.Where(x => x.MemberId == user.Id).ToList();
            cs.value9 = db.TBLCargo.ToList();
            cs.value10 = db.TBLAddress.ToList();
            var value = db.TBLBasket.Where(x => x.MemberId == user.Id).Sum(x => x.Price);
            ViewBag.value = value;
            return View(cs);
        }
        [HttpGet]
        public ActionResult OrderCheck(int id)
        {
            ViewBag.product = db.TBLProduct.Where(x => x.CategoryId == id).ToList();
            ViewBag.best = db.best_like_product().ToList();

            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            ViewBag.like = db.TBLLike.Where(x => x.MemberId == user.Id).Select(y => y.ProductId).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult OrderCheck(TBLOrder o)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();


            ViewBag.list = db.TBLBasket.Where(x => x.MemberId == user.Id).ToList();
            foreach (var order in ViewBag.list)
            {
                o.MemberId = user.Id;
                o.Date = DateTime.Now;
                o.ProductId = order.ProductId;
                o.Price = order.Price;
                o.Piece = order.Piece;
                o.Size = order.Size;
                db.TBLOrder.Add(o);
                db.TBLBasket.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("OrderCheck", "ConfirmCart");
        }
    }
}