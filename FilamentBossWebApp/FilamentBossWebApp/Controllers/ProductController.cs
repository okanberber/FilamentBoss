using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Controllers
{
    public class ProductController : Controller
    {
        FilamentBossDbModel db = new FilamentBossDbModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }
    }
}