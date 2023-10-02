using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineShopping.Models.Entity;
using MvcOnlineShopping.Models.Classes;

namespace MvcOnlineShopping.Controllers
{
    public class ProductPageController : Controller
    {
        // GET: ProductPage
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        [HttpGet]
        public ActionResult Index(int Id)
        {
            Class1 cs = new Class1();
            cs.value1 = db.TBLProduct.Where(x => x.Id == Id).ToList();
            cs.value7 = db.TBLComments.Where(x => x.ProductId == Id && x.Status == true).ToList();

            var products = db.TBLProduct.Where(x => x.Id == Id).ToList();
            ViewBag.product2 = products.Select(z => z.Id).FirstOrDefault();

            ViewBag.product = db.TBLProduct.ToList();
            ViewBag.best = db.best_like_product().ToList();

            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            ViewBag.like = db.TBLLike.Where(x => x.MemberId == user.Id).Select(y => y.ProductId).ToList();

            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLComments c, int Id)
        {
            db.TBLComments.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "ProductPage", new { Id = Id });
        }
        public ActionResult AddCart(TBLBasket b)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            ViewBag.basket = db.TBLBasket.Where(x => x.MemberId == user.Id).ToList();
            int sayac = 0;
            foreach (var value in ViewBag.basket)
            {
                if (value.ProductId == b.ProductId)
                {
                    sayac++;
                    value.Piece = value.Piece + b.Piece;
                    db.SaveChanges();
                }
            }
            if(sayac==0)
            {
                db.TBLBasket.Add(b);
                db.SaveChanges();
            }
            
            return RedirectToAction("ShoppingCart","MemberHomePage");
        }
    }
}