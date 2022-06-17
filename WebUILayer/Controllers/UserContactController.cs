using EntityLayer.Modal;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class UserContactController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserContact
        public ActionResult Index()
        {
            var contact = db.ContactInformations.Where(x => !x.DeletionStatüs).ToList();
            ViewBag.banner = "Bizimle İletişime Geçin";
            ViewBag.user = Session["userlogin"] as EntityLayer.Entity.EndUser;
            //var user = Session["userlogin"] as EntityLayer.Entity.EndUser;
            ViewBag.mapurl = db.ContactInformations.Where(x => !x.DeletionStatüs).Select(x => x.MapUrl).FirstOrDefault();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Index(Contact cnc)
        {
            ViewBag.mapurl = db.ContactInformations.Where(x => !x.DeletionStatüs).Select(x => x.MapUrl).FirstOrDefault();
            var contact = db.ContactInformations.Where(x => !x.DeletionStatüs).ToList();
            ViewBag.banner = "Bizimle İletişime Geçin";
            ViewBag.user = Session["userlogin"] as EntityLayer.Entity.EndUser;
            cnc.CreatedTime = DateTime.Now;
            cnc.UpdatedTime = DateTime.Now;
            cnc.DeletionStatüs = false;
            if (cnc != null)
            {

                db.Contacts.Add(cnc);
                db.SaveChanges();
                ViewBag.message = "Mailiniz bize ulaşmıştır en kısa zamanda size dönüş sağlanacaktır";

            }
            else
            {
                ViewBag.message = "Mail gönderirken bir hata oluştu lütfen tekrar deneyiniz";
            }
          
            return View(contact);
            

        }
    }
}