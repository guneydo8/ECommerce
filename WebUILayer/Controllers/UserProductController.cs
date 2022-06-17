using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Entity;
using EntityLayer.Modal;
using WebUILayer.Models;

namespace WebUILayer.Controllers
{
    public class UserProductController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserPtoduct
        public ActionResult Index(Product prd)
        {
            ViewBag.banner = "Ürünler";
            var model = new ProductListModel();
            //ViewBag.category= db.ProductCategories.Where(x => !x.DeletionStatüs).Select(x=>x.Title).ToList();
            model.ProductCategories = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();
            model.Brands = db.ProductBrands.Where(x => !x.DeletionStatüs).ToList();
            var latest = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).OrderByDescending(x => x.Id).ToList().Take(4);
            foreach (var item in latest)
            {
                model.LatestProduct.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }

            //var rating = db.ProductComments.Where(x => !x.DeletionStatüs).ToList();

            //foreach (var item in rating)
            //{
            //    model.ProductComments.Add(new CommentListItemModel { Product = item.Product, ProductComments = db.ProductComments.Where(x => x.ProductId == item.ProductId).ToList() });
            //}




            var products = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).ToList();
            foreach (var item in products)
            {
                model.Products.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }
            //ViewBag.image1 = db.ProductImages.Where(x => !x.DeletionStatüs).Select(x => x.ImageUrl).ToList();

            ViewBag.loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;


            return View(model);
        }



        public ActionResult SingleProduct(int id)
        {
            var model = new ProductListModel();
            model.SinleProduct = db.Products.Where(x => x.Id == id && !x.DeletionStatüs).ToList();
            var product = db.Products.Where(x => !x.DeletionStatüs && x.Id == id).FirstOrDefault();
          
            var products = db.Products.Where(x => !x.DeletionStatüs && x.ProductCategoryId == product.ProductCategoryId).Include(x=>x.ProductBrand).Include(x=>x.ProductCategory).ToList();
            foreach (var item in products)
            {
                model.Products.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }
            ViewBag.loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
           
            ViewBag.images = db.ProductImages.Where(x => !x.DeletionStatüs && x.ProductId == id).ToList();
            ViewBag.singleimage = db.ProductImages.Where(x => !x.DeletionStatüs && x.ProductId == id).Select(x => x.ImageUrl).FirstOrDefault();
            ViewBag.banner =  product.Title;
            ViewBag.relatedProducts = db.Products.Where(x => !x.DeletionStatüs && x.ProductCategoryId == product.ProductCategoryId).ToList();

            ViewBag.comment = db.ProductComments.Where(x => !x.DeletionStatüs && x.ProductId == id).OrderByDescending(x => x.Id).ToList();
            ViewBag.commentCount = db.ProductComments.Where(x => !x.DeletionStatüs && x.ProductId == id).Count();

            if (ViewBag.commentCount > 0)
            {
                ViewBag.ratingAvarage = db.ProductComments.Where(x => !x.DeletionStatüs && x.ProductId == id).Average(x => x.Rating);
            }
            
           

         
           
            return View(model);

        }

        [HttpPost]
        public ActionResult SingleProduct(ProductComment prdcomment,int id)
        {
           
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            prdcomment.CreatedTime = DateTime.Now;
            prdcomment.UpdatedTime = DateTime.Now;
            prdcomment.DeletionStatüs = false;
            prdcomment.EndUserId = loginuser.Id;
            prdcomment.ProductId = id;
            prdcomment.Name = loginuser.Name;
            prdcomment.Surname = loginuser.Surname;
            db.ProductComments.Add(prdcomment);
            db.SaveChanges();
            return RedirectToAction("Index");

            
            
        }


        public ActionResult Brands(int id,Product prd)
        {

           
            var model = new ProductListModel();
            //ViewBag.category= db.ProductCategories.Where(x => !x.DeletionStatüs).Select(x=>x.Title).ToList();
            model.ProductCategories = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();
            model.Brands = db.ProductBrands.Where(x => !x.DeletionStatüs).ToList();
            var latest = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).OrderByDescending(x => x.Id).ToList().Take(4);
            foreach (var item in latest)
            {
                model.LatestProduct.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }

            //var rating = db.ProductComments.Where(x => !x.DeletionStatüs).ToList();

            //foreach (var item in rating)
            //{
            //    model.ProductComments.Add(new CommentListItemModel { Product = item.Product, ProductComments = db.ProductComments.Where(x => x.ProductId == item.ProductId).ToList() });
            //}




            var products = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0 && x.ProductBrandId==id).Include(x=>x.ProductBrand).ToList();
            foreach (var item in products)
            {
                model.Products.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }
            //ViewBag.image1 = db.ProductImages.Where(x => !x.DeletionStatüs).Select(x => x.ImageUrl).ToList();
            ViewBag.banner = "Ürünler";
            ViewBag.loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;


            return View(model);

        }

        public ActionResult Category(int id,Product prd)
        {


            var model = new ProductListModel();
            //ViewBag.category= db.ProductCategories.Where(x => !x.DeletionStatüs).Select(x=>x.Title).ToList();
            model.ProductCategories = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();
            model.Brands = db.ProductBrands.Where(x => !x.DeletionStatüs).ToList();
            var latest = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).OrderByDescending(x => x.Id).ToList().Take(4);
            foreach (var item in latest)
            {
                model.LatestProduct.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }

            //var rating = db.ProductComments.Where(x => !x.DeletionStatüs).ToList();

            //foreach (var item in rating)
            //{
            //    model.ProductComments.Add(new CommentListItemModel { Product = item.Product, ProductComments = db.ProductComments.Where(x => x.ProductId == item.ProductId).ToList() });
            //}




            var products = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0 && x.ProductCategoryId == id).Include(x => x.ProductCategory).ToList();
            foreach (var item in products)
            {
                model.Products.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }
            //ViewBag.image1 = db.ProductImages.Where(x => !x.DeletionStatüs).Select(x => x.ImageUrl).ToList();
            ViewBag.banner = "Ürünler";
            ViewBag.loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;


            return View(model);

        }

     

        public ActionResult quickview()
        {
            return View();
        }
    }
}