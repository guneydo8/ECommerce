using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUILayer.Models;
using EntityLayer.Modal;
using System.Data.Entity;
using EntityLayer.Entity;

namespace WebUILayer.Controllers
{
    public class UserCheckOutController : Controller
    {
        // GET: UserCheckOut
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            ViewBag.banner = "Siparişi Onayla";

            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            var model = new CheckViewModel();
            
            model.EndUsers = db.EndUsers.Where(x => !x.DeletionStatüs && x.Id == loginuser.Id).ToList();

            var items = db.Carts.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).ToList();

            foreach (var item in items)
            {
                model.Products.Add(new CheckListItem { Cart=item,Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });
                

            }

            return View(model);
        }


        public ActionResult UserSale()
        {
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            var products = db.Carts.Where(x => x.EndUserId == loginuser.Id == !x.DeletionStatüs).ToList();

            foreach (var item in products)
            {
                Sale sl = new Sale();
                sl.CreatedTime = DateTime.Now;
                sl.DeletionStatüs = false;
                sl.EndUserId = loginuser.Id;
                sl.Price = item.Price;
                sl.ProductId = item.ProductId;
                sl.Quantity = item.Quantity;
                sl.Total = item.Total;
                sl.UpdatedTime = DateTime.Now;
                db.Sales.Add(sl);
                db.SaveChanges();


                var product = db.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();

                product.Stock = product.Stock - item.Quantity;


                Cart cr = new Cart();

                var remove = db.Carts.Where(x => x.Id == item.Id).FirstOrDefault();

                remove.DeletionStatüs = true;

                db.SaveChanges();
                
                

            }

            return RedirectToAction("Index", "UserProduct");
        }


    }
}