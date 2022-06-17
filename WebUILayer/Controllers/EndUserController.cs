using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class EndUserController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: EndUser
        public ActionResult Index()
        {
            var enduser = db.EndUsers.Where(x => !x.DeletionStatüs).ToList();

            if (enduser.Count != 0)
            {
                ViewBag.message = enduser.Count + " adet müşteri listelenmiştir";
            }
            else
            {
                ViewBag.message = "Listelenecek müşteri bulunamamıştır";
            }

            return View(enduser);
        }
    }
}