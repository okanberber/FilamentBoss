using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Filters;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Controllers
{
    public class HomeController : Controller
    {
        FilamentBossDbModel db = new FilamentBossDbModel();
        public ActionResult Index(int? Categoryid)
        {
            var products = db.Products.Where(x => x.IsDeleted == false && x.IsActive == true);

            if (Categoryid.HasValue)
            {
                products = products.Where(x => x.Category_ID == Categoryid.Value);
            }

            return View(products.ToList());
        }
        [MemberLoginRequiredFilter]
        public ActionResult _GetCartCount()
        {
            int mid = (Session["member"] as Member).ID;
            int count = db.Carts.Count(x => x.Member_ID == mid);
            ViewBag.count = count;
            return View();
        }
    }
}