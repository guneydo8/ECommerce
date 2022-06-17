using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class UserFaqController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserFaq
        public ActionResult Index()
        {
            var faq = db.Faqs.Where(x => !x.DeletionStatüs).ToList();
            ViewBag.banner = "Sıkça Sorulan Sorular";
            return View(faq);
        }
    }
}