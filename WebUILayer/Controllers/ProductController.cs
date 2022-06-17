using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Entity;
using EntityLayer.Modal;

namespace WebUILayer.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        SystemContext db = new SystemContext();

        public ActionResult Index()
        {
            var product = db.Products.Where(x => !x.DeletionStatüs).Include(x => x.ProductCategory).Include(x => x.ProductBrand).ToList();

            //var whist = db.Whistlists.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).ToList();

           
            if (product.Count != 0)
            {
                ViewBag.message = product.Count + " adet ürün listelenmiştir";
            }
            else
            {
                ViewBag.message = "Listelenecek ürün bulunamamıştır";
            }

            return View(product);
        }

        public ActionResult Add()
        {
            

            List<SelectListItem> category = (from x in db.ProductCategories.Where(x=>!x.DeletionStatüs)
                                             select
                                             new SelectListItem
                                             {
                                                 Text = x.Title,
                                                 Value = x.Id.ToString()
                                                 
                                             }).ToList();
            ViewBag.ProductCategoryId = category;
          

            List<SelectListItem> brand = (from x in db.ProductBrands.Where(x => !x.DeletionStatüs)
                                          select
                                             new SelectListItem
                                             {
                                                 Text = x.Title,
                                                 Value = x.Id.ToString()
                                                 
                                             }).ToList();
            ViewBag.ProductBrandId = brand;



            return View();
        }

        [HttpPost]
        public ActionResult Add(Product prd ,List<HttpPostedFileBase> images)
        {
            prd.CreatedTime = DateTime.Now;
            prd.UpdatedTime = DateTime.Now;
            prd.DeletionStatüs = false;
            string[] text = { "A", "B", "C", "D", "E", "F" };
            Random rnd = new Random();
            int t,number1;
            t = rnd.Next(0, 6);
            number1 = rnd.Next(1000000, 10000000);
            prd.ProductCode = text[t]+ number1.ToString();
            var code = db.Products.Where(x => x.ProductCode == prd.ProductCode && !x.DeletionStatüs).FirstOrDefault();
            if (code != null)
            {
                
                    t = rnd.Next(0, 6);
                    number1 = rnd.Next(1000000, 10000000);
                    prd.ProductCode = text[t] + number1.ToString();

                
            }


            foreach (HttpPostedFileBase pFile in images)
            {
                if (images != null)
                {
                    // string photoName = Path.GetFileName(pFile.FileName);
                    //string url = "~/File/Product/" + photoName;
                    //pFile.SaveAs(Server.MapPath(url));
                
                   
                    //string fileName = Path.GetFileName(pFile.FileName);

                    string photoName = Path.GetFileName(pFile.FileName);
                    string url = "/File/Brand/" + photoName;
                    pFile.SaveAs(Server.MapPath(url));

                  

                    ProductImage img = new ProductImage();
                    img.ImageUrl = url;
                    img.CreatedTime = DateTime.Now;
                    img.UpdatedTime = DateTime.Now;
                    img.DeletionStatüs = false;
                    db.ProductImages.Add(img);
                    
                }
            }




            db.Products.Add(prd);
            if (prd != null)
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

        public ActionResult Delete(int id)
        {
            var del = db.Products.Find(id);
            del.DeletionStatüs = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Update(int id)
        {


            List<SelectListItem> category = (from x in db.ProductCategories.Where(x => !x.DeletionStatüs)
                                             select
                                             new SelectListItem
                                             {
                                                 Text = x.Title,
                                                 Value = x.Id.ToString()

                                             }).ToList();
            ViewBag.ProductCategoryId = category;


            List<SelectListItem> brand = (from x in db.ProductBrands.Where(x => !x.DeletionStatüs)
                                          select
                                          new SelectListItem
                                          {
                                              Text = x.Title,
                                              Value = x.Id.ToString()

                                          }).ToList();
            ViewBag.ProductBrandId = brand;

            var product = db.Products.Find(id);

            return View(product);

        }

        [HttpPost]
        public ActionResult Update(Product prd,List<HttpPostedFileBase> images)
        {
            var product = db.Products.Find(prd.Id);
            product.UpdatedTime = DateTime.Now;
            product.Description = prd.Description;
            product.Price = prd.Price;
            product.Stock = prd.Stock;
            product.Title = prd.Title;
            product.ProductCategoryId = prd.ProductCategoryId;
            product.ProductBrandId = prd.ProductBrandId;

            ProductImage img = new ProductImage();
            var updateImage = db.ProductImages.Where(x => x.ProductId == prd.Id && !x.DeletionStatüs).ToList();
            foreach (HttpPostedFileBase pFile in images)
            {
                if (images != null)
                {
                    string photoName = Path.GetFileName(pFile.FileName);
                    string url = "/File/Product/" + photoName;
                    pFile.SaveAs(Server.MapPath(url));

                    

                    foreach (var update in updateImage)
                    {
                        
                        update.ImageUrl = url;
                        update.UpdatedTime = DateTime.Now;

                        db.SaveChanges();

                    }
                }
            }



            if (prd != null)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Bir Hata Oluştu Lütfen Tekrar Deneyiniz";
                return View();
            }
        }

        public ActionResult ProductDetails(int id)
        {
            ViewBag.image = db.ProductImages.Where(x => x.ProductId == id && !x.DeletionStatüs).Select(x=>x.ImageUrl).ToList();
            var product = db.Products.Find(id);
            

            return View(product);
        }

     
     
    }
}