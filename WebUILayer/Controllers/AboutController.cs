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
    public class AboutController : Controller
    {
        // GET: About


        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var category = db.Abouts.Where(x => !x.DeletionStatüs).ToList();
            if (category.Count != 0)
            {
                ViewBag.message = category.Count + " adet öğe listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek öğe bulunamadı";

            }
            return View(category);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(About abt, HttpPostedFileBase File)
        {
          
                if (File != null)
                {

                    string photoName = Path.GetFileName(File.FileName);
                    string url = "/File/About/" + photoName;
                    File.SaveAs(Server.MapPath(url));
                    abt.ImageUrl = url;
                    abt.CreatedTime = DateTime.Now;
                    abt.UpdatedTime = DateTime.Now;
                    abt.DeletionStatüs = false;
                    db.Abouts.Add(abt);

                    if (abt != null)
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
                else
                {
                    ViewBag.message = "Resim Yüklenemedi Lütfen Tekrar Deneyiniz";
                    return View();

                }

            

        }

        public ActionResult Delete(int id)
        {
            var del = db.Abouts.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {
            var upd = db.Abouts.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(About abt, HttpPostedFileBase File)
        {

            var upd = db.Abouts.Find(abt.Id);
            upd.Title = abt.Title;
            upd.UpdatedTime = DateTime.Now;
            upd.Description = abt.Description;
          

            
                if (ModelState.IsValid)
                {
                    if (File != null)
                    {
                        string photoName = Path.GetFileName(File.FileName);
                        string url = "/File/About/" + photoName;
                        File.SaveAs(Server.MapPath(url));
                        upd.ImageUrl = url;
                    }
                }

                if (abt != null)
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