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
    public class ProductBrandController : Controller
    {

        // GET: ProductCategory
        SystemContext db = new SystemContext();

        
        public ActionResult Index()
        {
            var brand = db.ProductBrands.Where(x => !x.DeletionStatüs).ToList();
            if (brand.Count != 0)
            {
                ViewBag.message = brand.Count + " adet marka listelenmiştir";

            }
            else
            {
                ViewBag.message = "Listelenecek öğe bulunamadı";

            }
            return View(brand);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductBrand brd,HttpPostedFileBase File)
        {
            var brand = db.ProductBrands.Where(x => x.Title == brd.Title && !x.DeletionStatüs).FirstOrDefault();
           
            if (brand != null)
            {
                ViewBag.message = "Sistemde aynı marka mevcuttur";
                return View();
            }
            else
            {
                if (File != null)
                {

                    string photoName = Path.GetFileName(File.FileName);
                    string url = "/File/Brand/" + photoName;
                    File.SaveAs(Server.MapPath(url));
                   
                    brd.ImageUrl = url;
                    brd.CreatedTime = DateTime.Now;
                    brd.UpdatedTime = DateTime.Now;
                    brd.DeletionStatüs = false;
                    db.ProductBrands.Add(brd);

                    if (brd != null)
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
            var del = db.ProductBrands.Find(id);
            del.DeletionStatüs = true;

            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {
            var upd = db.ProductBrands.Find(id);

            return View(upd);
        }

        [HttpPost]
        public ActionResult Update(ProductBrand brd,HttpPostedFileBase File)
        {
            var a = brd.ImageUrl;
            var upd = db.ProductBrands.Find(brd.Id);
            upd.Title = brd.Title;
            upd.UpdatedTime = DateTime.Now;
            //var brand = db.ProductBrands.Where(x => x.Title == brd.Title && !x.DeletionStatüs).FirstOrDefault();
            var brand = db.ProductBrands.Where(x => x.Title == brd.Title && !x.DeletionStatüs);
            if (brand.Count()>1)
            {
                ViewBag.message = "Sistemde aynı marka mevcuttur";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (File != null)
                    {
                        string photoName = Path.GetFileName(File.FileName);
                        string url = "/File/Brand/" + photoName;
                        File.SaveAs(Server.MapPath(url));
                        upd.ImageUrl = url;
                    }
                }

            

                if (brd != null)
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
            var product = db.Products.Where(x => !x.DeletionStatüs && x.ProductBrandId == id).ToList();
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