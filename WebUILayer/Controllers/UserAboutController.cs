using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class UserAboutController : Controller
    {
        // GET: UserAbout
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var about = db.Abouts.Where(x => !x.DeletionStatüs).ToList();

            ViewBag.image = db.Abouts.Where(x => !x.DeletionStatüs).Select(y => y.ImageUrl).ToList();
            ViewBag.banner = "Hakkımızda";

            return View(about);
        }
    }
}