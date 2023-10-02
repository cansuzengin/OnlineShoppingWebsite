using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineShopping.Models.Entity;
using MvcOnlineShopping.Models.Classes;

namespace MvcOnlineShopping.Controllers
{
    public class MemberHomePageController : Controller
    {
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        // GET: MemberHomePage
        [AllowAnonymous]
        public ActionResult HomePage(string Message)
        {
            ViewBag.value = Message;
            return View();
        }
        [HttpGet]
        public ActionResult Profiles()
        {
            var userMail = (string)Session["Email"];
            var degerler = db.TBLMember.Where(z => z.Email == userMail).FirstOrDefault();
            ViewBag.name = db.TBLMember.Where(z => z.Email == userMail).Select(x => x.Name).FirstOrDefault();
            ViewBag.surname = db.TBLMember.Where(z => z.Email == userMail).Select(x => x.Surname).FirstOrDefault();
            ViewBag.image = db.TBLMember.Where(z => z.Email == userMail).Select(x => x.Image).FirstOrDefault();
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Profiles(TBLMember m)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(z => z.Email == userMail).FirstOrDefault();
            user.Name = m.Name;
            user.Surname = m.Surname;
            user.Email = m.Email;
            user.Password = m.Password;
            db.SaveChanges();
            return RedirectToAction("Profiles");
        }
        [Authorize]
        public ActionResult ShoppingPage(int? gender, int? category, int? brand, string color)
        {
            var pro = db.TBLProduct.ToList();
            var com = db.TBLComments.ToList();

            foreach (var value in pro)
            {
                var keep = 0;
                foreach (var value2 in com)
                {
                    if (value.Id == value2.ProductId)
                    {
                        keep = 1;
                    }
                }
                if (keep == 1)
                {
                    var star = db.TBLComments.Where(x => x.ProductId == value.Id).Average(y => y.Rating).Value;
                    if (star != null)
                    {
                        value.Star = (int?)star;
                        db.SaveChanges();
                    }
                }

            }
            Class1 cs = new Class1();
            if (category != null && gender != null && brand != null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.CategoryId == category && x.GenderId == gender && x.BrandId == brand).ToList();
            }
            else if (category != null && gender != null && brand == null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.CategoryId == category && x.GenderId == gender).ToList();
            }
            else if (category != null && gender == null && brand != null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.CategoryId == category && x.BrandId == brand).ToList();
            }
            else if (category == null && gender != null && brand != null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.GenderId == gender && x.BrandId == brand).ToList();
            }
            else if (category != null && gender == null && brand == null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.CategoryId == category).ToList();
            }
            else if (category == null && gender != null && brand == null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.GenderId == gender).ToList();
            }
            else if (category == null && gender == null && brand != null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.BrandId == brand).ToList();
            }
            else if (category == null && gender == null && brand == null && color != null)
            {
                cs.value1 = db.TBLProduct.Where(x => x.Color == color).ToList();
            }
            else
            {
                cs.value1 = db.TBLProduct.ToList();
            }
            cs.value2 = db.TBLBrand.ToList();
            cs.value4 = db.TBLCategory.ToList();
            cs.value6 = db.TBLGender.ToList();

            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            ViewBag.like = db.TBLLike.Where(x => x.MemberId == user.Id).Select(y => y.ProductId).ToList();

            return View(cs);
        }
        [Authorize]
        public ActionResult ShoppingHomePage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact(string Message)
        {
            ViewBag.value = Message;
            return View();
        }
        [HttpPost]
        public ActionResult Contact(TBLMessage m)
        {
            db.TBLMessage.Add(m);
            db.SaveChanges();
            return RedirectToAction("HomePage", "MemberHomePage", new { Message = "1" });
        }
        [Authorize]
        public ActionResult ShoppingCart()
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            Class1 cs = new Class1();
            cs.value3 = db.TBLBasket.Where(x => x.MemberId == user.Id).ToList();
            var value = db.TBLBasket.Where(x => x.MemberId == user.Id).Sum(x => x.Price);
            int? id = db.TBLBasket.Where(x => x.MemberId == user.Id).Select(y => y.CategoryId).FirstOrDefault();
            int? id2 = db.TBLBasket.Where(x => x.MemberId == user.Id).Select(y => y.TBLProduct.GenderId).FirstOrDefault();
            ViewBag.value = value;

            ViewBag.like = db.TBLLike.Where(x => x.MemberId == user.Id).Select(y => y.ProductId).ToList();
            ViewBag.id = id;
            ViewBag.id2 = id2;

            if (id == 7)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 4).ToList();
            }
            else if (id == 9)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 4 || x.CategoryId == 3).ToList();
            }
            else if (id == 4)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 7 || x.CategoryId == 9).ToList();
            }
            else if (id == 1)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 6).ToList();
            }
            else if (id == 5)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 4).ToList();
            }
            else if (id == 6)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 1 || x.CategoryId == 4).ToList();
            }
            else if (id == 3)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 10).ToList();
            }
            else if (id == 10)
            {
                ViewBag.kombin = db.TBLProduct.Where(x => x.CategoryId == 3).ToList();
            }

            return View(cs);
        }
        public ActionResult AddingCart(int id)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            var cart = db.TBLProduct.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.basket = db.TBLBasket.Where(x => x.MemberId == user.Id).ToList();
            int sayac = 0;
            foreach (var value in ViewBag.basket)
            {
                if (value.ProductId == id)
                {
                    sayac++;
                    value.Piece = value.Piece + 1;
                    db.SaveChanges();
                }
            }
            if (sayac == 0)
            {
                TBLBasket b = new TBLBasket();
                b.CategoryId = cart.CategoryId;
                b.ProductId = cart.Id;
                b.MemberId = user.Id;
                b.Piece = 1;
                b.Size = "M";
                b.Price = cart.Price;
                b.DateOfAdding = DateTime.Now;
                db.TBLBasket.Add(b);
                db.SaveChanges();
            }

            return RedirectToAction("ShoppingCart");
        }
        public ActionResult AddingLike(int id)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            var cart = db.TBLProduct.Where(x => x.Id == id).FirstOrDefault();

            var liked = db.TBLLike.Where(x => x.MemberId == user.Id).ToList();

            var keep = 0;
            foreach (var value in liked)
            {
                if (value.ProductId == id)
                {
                    keep = 1;
                    var pro = db.TBLLike.Where(x => x.MemberId == user.Id && x.ProductId == id).FirstOrDefault();
                    db.TBLLike.Remove(pro);
                    db.SaveChanges();
                }
            }
            if (keep == 0)
            {
                TBLLike b = new TBLLike();
                b.ProductId = cart.Id;
                b.MemberId = user.Id;
                db.TBLLike.Add(b);
                db.SaveChanges();
            }

            return RedirectToAction("Favorites");
        }
        [Authorize]
        public ActionResult Favorites()
        {
            Class1 cs = new Class1();
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            cs.value1 = db.TBLProduct.ToList();
            cs.value5 = db.TBLLike.Where(x => x.MemberId == user.Id).ToList();
            return View(cs);
        }
        [Authorize]
        public ActionResult Orders()
        {
            Class1 cs = new Class1();

            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            var sum = db.TBLOrder.Where(x => x.MemberId == user.Id).Sum(x => x.Price);
            ViewBag.value = sum;
            cs.value1 = db.TBLProduct.ToList();
            cs.value8 = db.TBLOrder.Where(x => x.MemberId == user.Id).ToList();
            return View(cs);
        }
        public ActionResult RemoveCart(int Id)
        {
            var cart = db.TBLBasket.Find(Id);
            db.TBLBasket.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("ShoppingCart");
        }
        public ActionResult RemoveLike(int Id)
        {
            var like = db.TBLLike.Find(Id);
            db.TBLLike.Remove(like);
            db.SaveChanges();
            return RedirectToAction("Favorites");
        }
        public ActionResult RemoveOrder(int Id)
        {
            var order = db.TBLOrder.Find(Id);
            db.TBLOrder.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Orders");
        }
        [HttpGet]
        public ActionResult Address()
        {
            Class1 cs = new Class1();
            cs.value10 = db.TBLAddress.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Address(TBLAddress a)
        {
            var address = db.TBLAddress.Find(a.Id);
            address.City = a.City;
            address.District = a.District;
            address.Address = a.Address;
            db.SaveChanges();
            return RedirectToAction("Address");
        }
        public ActionResult RemoveAddress(int id)
        {
            var address = db.TBLAddress.Find(id);
            db.TBLAddress.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Address");
        }
        public ActionResult AddAddress(TBLAddress a)
        {
            var userMail = (string)Session["Email"];
            var user = db.TBLMember.Where(x => x.Email == userMail).FirstOrDefault();
            a.MemberId = user.Id;
            db.TBLAddress.Add(a);
            db.SaveChanges();
            return RedirectToAction("Address");
        }
    }
}