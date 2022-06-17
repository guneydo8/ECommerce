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
    public class UserCartController : Controller
    {
        // GET: UserCart

        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            ViewBag.banner = "Sepetim";
            var model = new ChartViewModel();
            ViewBag.user= Session["userlogin"] as EntityLayer.Entity.EndUser;
            

            model.Carts = db.Carts.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).ToList();
            //var items = db.Products.Where(x => !x.DeletionStatüs && x.Stock > 0).ToList();
            var items=db.Carts.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).ToList();

            foreach (var item in items)
            {

                model.CartListItems.Add(new CartListItemModel { Cart=item,Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });
               
            }

            return View(model);


        }

        public ActionResult AddCart(int id,Cart cr)
        {
            var cartList = db.Carts.Where(x => x.ProductId == id).FirstOrDefault();

           
                var product = db.Products.Where(x => x.Id == id && !x.DeletionStatüs).FirstOrDefault();
                var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
                cr.ProductId = id;
                cr.EndUserId = loginuser.Id;
                cr.Price = product.Price;
                cr.Quantity = 1;
                cr.Total = cr.Quantity * product.Price;
                cr.UpdatedTime = DateTime.Now;
                cr.CreatedTime = DateTime.Now;
                cr.DeletionStatüs = false;
                db.Carts.Add(cr);
                db.SaveChanges();
            

    
            return RedirectToAction("Index");

        }


        public ActionResult RemoveCart(int id)
        {
            var rmv = db.Carts.Where(x => x.Id == id).FirstOrDefault();

            rmv.DeletionStatüs = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCart(int quantity,int item)
        {
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;

            var cart = db.Carts.Find(item);
            cart.Quantity = quantity;
            cart.Total = quantity * cart.Price;
            db.SaveChanges();
            return View();

        }

   

    }
}