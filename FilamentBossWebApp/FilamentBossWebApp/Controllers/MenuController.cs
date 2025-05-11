using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Controllers
{
    public class MenuController : Controller
    {
        FilamentBossDbModel db = new FilamentBossDbModel();
        // GET: Menu
        public ActionResult _Index()
        {
            return View(db.Categories.Where(x => x.IsDeleted == false).ToList());
        }
    }
}