using EntityLayer.Entity;
using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class ContactInformationController : Controller
    {
        // GET: ContactInformation
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var contact = db.ContactInformations.Where(x => !x.DeletionStatüs).ToList();
            if (contact.Count != 0)
            {
                ViewBag.message = contact.Count + " adet öğe listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek öğe bulunamadı";

            }
            return View(contact);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ContactInformation cnc)
        {
            var category = db.ContactInformations.Where(x => x.Email == cnc.Email && !x.DeletionStatüs).FirstOrDefault();
            if (category != null)
            {
                ViewBag.message = "Sistemde Aynı Mail Mevcuttur";
                return View();
            }
            else
            {
                
                    cnc.CreatedTime = DateTime.Now;
                    cnc.UpdatedTime = DateTime.Now;
                    cnc.DeletionStatüs = false;
                    db.ContactInformations.Add(cnc);

                    if (cnc != null)
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.message = "Bir Hata Oluştu Lütfen Tekrar Deneyiniz";
                        return View();
                    }
           

            }

        }

        public ActionResult Delete(int id)
        {
            var del = db.ContactInformations.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {
            var upd = db.ContactInformations.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(ContactInformation cnc)
        {

            var upd = db.ContactInformations.Find(cnc.Id);
            upd.Country = cnc.Country;
            upd.City = cnc.City;
            upd.Adress = cnc.Adress;
            upd.Email = cnc.Email;
            upd.Phone = cnc.Phone;
            upd.MapUrl = cnc.MapUrl;
            upd.UpdatedTime = DateTime.Now;

            var contact = db.ContactInformations.Where(x => x.Email == cnc.Email && !x.DeletionStatüs);

            if (contact.Count() > 1)
            {
                ViewBag.message = "Sistemde Aynı Mail Mevcuttur";
                return View();
            }
            else
            {
                

                if (cnc != null)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Bir Hata Oluştu Lütfen Tekrar Deneyiniz";
                    return View();
                }
            }



        }
    }
}