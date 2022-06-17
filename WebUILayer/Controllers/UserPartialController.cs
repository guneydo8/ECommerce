using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUILayer.Models;

namespace WebUILayer.Controllers
{
    public class UserPartialController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserPartial
        public ActionResult PartialCategory()
        {
            var category = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();
            return PartialView(category);
        }

        public ActionResult PartialTopCategory()
        {
            ViewBag.category = db.ProductCategories.Where(x => !x.DeletionStatüs).OrderByDescending(x => x.CreatedTime).Select(x => x.ImageUrl).Take(4);
            var category = db.ProductCategories.Where(x => !x.DeletionStatüs).OrderByDescending(x=>x.CreatedTime).ToList();
            return PartialView(category);

        }

        public ActionResult FooterContact()
        {
            var contact = db.ContactInformations.Where(x => !x.DeletionStatüs).FirstOrDefault();
            return PartialView(contact);
        }

        public ActionResult SupportContact()
        {
            var contact = db.ContactInformations.Where(x => !x.DeletionStatüs).FirstOrDefault();
            return PartialView(contact);
        }

        public ActionResult CartBanner()
        {
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;

            
            var model = new ChartViewModel();

            
            var items = db.Carts.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).ToList();


            foreach (var item in items)
            {

                model.CartListItems.Add(new CartListItemModel { Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }
            return PartialView(model);
        }

  
 
        public ActionResult Search(string search)
        {
            return PartialView();
        }
        public ActionResult SearchAll(string search)
        {

            ViewBag.searchtext = search;


           var products = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0 && x.Title == search || x.Title.Contains(search)).ToList();


            var model = new SearchViewModel
            {


               
                ProductBrands = db.ProductBrands.Where(x => !x.DeletionStatüs && x.Title == search || x.Title.Contains(search)).ToList(),
                ProductCategories = db.ProductCategories.Where(x => !x.DeletionStatüs && x.Title == search || x.Title.Contains(search)).ToList()
            };


            foreach (var item in products)
            {
                model.ProductItemModels.Add(new ProductItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });

            }
            return PartialView(model);
        }


        public ActionResult FooterCategory()
        {

            var category = db.ProductCategories.Where(x => !x.DeletionStatüs).OrderByDescending(x => x.Id).ToList();

            return PartialView(category);

        }

        public ActionResult FooterBrand()
        {
            var brand = db.ProductBrands.Where(x => !x.DeletionStatüs).OrderByDescending(x => x.Id).ToList();
            return PartialView(brand);
        }
    }
}