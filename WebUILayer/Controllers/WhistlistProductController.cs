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
    public class WhistlistProductController : Controller
    {
        // GET: ChartProduct
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var model = new WhistlistViewModel();
            var items = db.Whistlists.Where(x => !x.DeletionStatüs).Include(x => x.Product).Include(x => x.Product.ProductBrand).Include(x => x.Product.ProductCategory).Include(x => x.EndUser).ToList();


            foreach (var item in items)
            {

                model.WhistlistItems.Add(new WhistlistItemModel { Whistlist = item, Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }


            ViewBag.message = "Favorideki Ürünler";
            return View(model);
        }
    }
}