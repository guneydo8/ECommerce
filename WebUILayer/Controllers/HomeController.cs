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
    public class HomeController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.totalsale = db.Sales.Where(x => !x.DeletionStatüs).Sum(x => x.Price);
            ViewBag.product = db.Products.Where(x => !x.DeletionStatüs).Count();
            ViewBag.customer = db.EndUsers.Where(x => !x.DeletionStatüs).Count();
            ViewBag.image = db.ProductImages.Where(x => !x.DeletionStatüs).Count();

            var model = new HomeViewModel();

            var latestproduct = db.Products.Where(x => !x.DeletionStatüs).OrderByDescending(x => x.Id).Include(x=>x.ProductBrand).ToList().Take(5);

            foreach (var item in latestproduct)
            {

                model.LatestPRoduct.Add(new ProductList { Product = item, ProductImages = db.ProductImages.Where(x => x.ProductId == item.Id).ToList() });

            }

          
           
           



            return View(model);
        }
    }
}