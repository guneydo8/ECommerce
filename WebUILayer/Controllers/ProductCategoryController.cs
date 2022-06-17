using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Modal;
using EntityLayer.Entity;
using System.IO;

namespace WebUILayer.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var category = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();
            if (category.Count != 0)
            {
                ViewBag.message = category.Count + " adet kategori listelenmiştir";

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
        public ActionResult Add(ProductCategory ctg, HttpPostedFileBase File)
        {
            var category = db.ProductCategories.Where(x => x.Title == ctg.Title && !x.DeletionStatüs).FirstOrDefault();
            if (category != null)
            {
                ViewBag.message = "Sistemde Aynı İsimde Kategori Mevcuttur";
                return View();
            }
            else
            {
                if (File != null)
                {

                    string photoName = Path.GetFileName(File.FileName);
                    string url = "/File/Category/" + photoName;
                    File.SaveAs(Server.MapPath(url));

                    ctg.ImageUrl = url;
                    ctg.CreatedTime = DateTime.Now;
                    ctg.UpdatedTime = DateTime.Now;
                    ctg.DeletionStatüs = false;
                    db.ProductCategories.Add(ctg);

                    if (ctg != null)
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
            var del = db.ProductCategories.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Update(int id)
        {
            var upd = db.ProductCategories.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(ProductCategory ctg, HttpPostedFileBase File)
        {

            var upd = db.ProductCategories.Find(ctg.Id);
            upd.Title = ctg.Title;
            upd.UpdatedTime = DateTime.Now;

            var category = db.ProductCategories.Where(x => x.Title == ctg.Title && !x.DeletionStatüs);

            if (category.Count() > 1)
            {
                ViewBag.message = "Sistemde Aynı İsimde Kategori Mevcuttur";
                return View();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    if (File != null)
                    {
                        string photoName = Path.GetFileName(File.FileName);
                        string url = "/File/Category/" + photoName;
                        File.SaveAs(Server.MapPath(url));
                        upd.ImageUrl = url;
                    }
                }


                if (ctg != null)
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
        public ActionResult View(int id)
        {
            var product = db.Products.Where(x => !x.DeletionStatüs && x.ProductCategoryId == id).ToList();
            if (product.Count != 0)
            {
                ViewBag.message = product.Count + " adet ürün listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek öğe bulunamadı";

            }
            return View(product);

        }
    }
}