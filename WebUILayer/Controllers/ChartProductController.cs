using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUILayer.Models;
using EntityLayer.Entity;
using EntityLayer.Modal;
using System.Data.Entity;

namespace WebUILayer.Controllers
{
    public class ChartProductController : Controller
    {
        // GET: ChartProduct
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var model = new ChartViewModel();
            var items = db.Carts.Where(x => !x.DeletionStatüs).Include(x => x.Product).Include(x => x.Product.ProductBrand).Include(x=>x.Product.ProductCategory).Include(x=>x.EndUser).ToList();

            foreach (var item in items)
            {

                model.CartListItems.Add(new CartListItemModel { Cart = item, Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }

            ViewBag.message = "Sepetteki Ürünler";
            return View(model);
        }
    }
}