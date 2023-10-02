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
    public class OrdersController : Controller
    {
        // GET: Orders
        DBONLINESHOPPINGEntities db = new DBONLINESHOPPINGEntities();
        public ActionResult Index(String p, int sayfa = 1)
        {
            var values = from k in db.TBLOrder select k;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.TBLProduct.Name.Contains(p));
            }
            return View(values.ToList().ToPagedList(sayfa, 5));
        }
    }
}