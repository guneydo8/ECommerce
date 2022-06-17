using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Entity;
using EntityLayer.Modal;
using WebUILayer.Models;
namespace WebUILayer.Controllers
{
    public class UserHomeController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserHome
        public ActionResult Index()
        {
            ViewBag.banner = "Ana Sayfa";

            ViewBag.loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;

            var model = new ProductListModel();

            model.Brands = db.ProductBrands.Where(x => !x.DeletionStatüs).ToList();
            model.ProductCategories = db.ProductCategories.Where(x => !x.DeletionStatüs).ToList();

            var latest = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).OrderByDescending(x => x.Id).ToList().Take(6);
            foreach (var item in latest)
            {
                model.LatestProduct.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }

            var products = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).ToList();
            foreach (var item in products)
            {
                model.Products.Add(new ProductListItemModel { Product = item, Images = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });
            }

            return View(model);
        }
    }
}