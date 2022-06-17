using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Modal;
using WebUILayer.Models;

namespace WebUILayer.Controllers
{
    public class SalesController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: Sales
        public ActionResult Index()
        {
            var model = new SalesViewModel();



            var items = db.Sales.Where(x => !x.DeletionStatüs).Include(x => x.Product).Include(x => x.Product.ProductBrand).Include(x => x.Product.ProductCategory).Include(x => x.EndUser).ToList();

            foreach (var item in items)
            {
                model.SalesItems.Add(new SalesItem { Sale = item, Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }

            ViewBag.message = "Satış Listesi";

            return View(model);
        }
    }
}