using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineShopping.Models.Entity;

namespace MvcOnlineShopping.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index()
        {
            var value1 = db.total_number_of_products().FirstOrDefault();
            var value2 = db.total_number_of_categories().FirstOrDefault();
            var value3 = db.total_number_of_brands().FirstOrDefault();
            var value4 = db.number_of_message().FirstOrDefault();
            var value5 = db.best_products().FirstOrDefault();
            var value6 = db.active_member().FirstOrDefault();
            var value7 = db.category_with_most_products().FirstOrDefault();
            var value8 = db.brand_with_most_products().FirstOrDefault();
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.value3 = value3;
            ViewBag.value4 = value4;
            ViewBag.value5 = value5;
            ViewBag.value6 = value6;
            ViewBag.value7 = value7;
            ViewBag.value8 = value8;
            return View();
        }
    }
}