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
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var anc = db.Announcements.Where(x => !x.DeletionStatüs).ToList();
            if (anc.Count != 0)
            {
                ViewBag.message = anc.Count + " adet Duyuru listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek Duyuru bulunamadı";

            }
            return View(anc);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Announcement anc, HttpPostedFileBase File)
        {
            var annnouncement = db.Announcements.Where(x => x.Title == anc.Title && !x.DeletionStatüs).FirstOrDefault();
            if (annnouncement != null)
            {
                ViewBag.message = "Sistemde Aynı Duyuru Mevcuttur";
                return View();
            }
            else
            {
                if (File != null)
                {

                    string photoName = Path.GetFileName(File.FileName);
                    string url = "/File/Announcement/" + photoName;
                    File.SaveAs(Server.MapPath(url));
                    anc.ImageUrl = url;
                    anc.CreatedTime = DateTime.Now;
                    anc.UpdatedTime = DateTime.Now;
                    anc.DeletionStatüs = false;
                    db.Announcements.Add(anc);

                    if (anc != null)
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

        }

        public ActionResult Delete(int id)
        {
            var del = db.Announcements.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {
            var upd = db.Announcements.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(Announcement anc, HttpPostedFileBase File)
        {

            var upd = db.Announcements.Find(anc.Id);
            upd.Title = anc.Title;
            upd.Description = anc.Description;
            upd.UpdatedTime = DateTime.Now;

            var announcement = db.Announcements.Where(x => x.Title == anc.Title && !x.DeletionStatüs);

            if (announcement.Count() > 1)
            {
                ViewBag.message = "Sistemde Aynı İsimde Duyuru Mevcuttur";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (File != null)
                    {
                        string photoName = Path.GetFileName(File.FileName);
                        string url = "/File/Announcement/" + photoName;
                        File.SaveAs(Server.MapPath(url));
                        upd.ImageUrl = anc.ImageUrl;
                    }
                }

                if (anc != null)
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