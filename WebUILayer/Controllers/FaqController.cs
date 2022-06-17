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
    public class FaqController : Controller
    {
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var faq = db.Faqs.Where(x => !x.DeletionStatüs).ToList();
            if (faq.Count != 0)
            {
                ViewBag.message = faq.Count + " adet S.S.S listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek S.S.S bulunamadı";

            }
            return View(faq);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Faq f, HttpPostedFileBase File)
        {
            var category = db.Faqs.Where(x => x.Title == f.Title && !x.DeletionStatüs).FirstOrDefault();
            if (category != null)
            {
                ViewBag.message = "Sistemde Aynı S.S.S Mevcuttur";
                return View();
            }
            else
            {
               
                    f.CreatedTime = DateTime.Now;
                    f.UpdatedTime = DateTime.Now;
                    f.DeletionStatüs = false;
                    db.Faqs.Add(f);

                    if (f != null)
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
            var del = db.Faqs.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {
            var upd = db.Faqs.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(Faq f, HttpPostedFileBase File)
        {

            var upd = db.Faqs.Find(f.Id);
            upd.Title = f.Title;
            upd.Description = f.Description;
            upd.UpdatedTime = DateTime.Now;

            var faq = db.Faqs.Where(x => x.Title == f.Title && !x.DeletionStatüs);

            if (faq.Count()>1)
            {
                ViewBag.message = "Sistemde Aynı İsimde S.S.S Mevcuttur";
                return View();
            }
            else
            {
               

                if (f != null)
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