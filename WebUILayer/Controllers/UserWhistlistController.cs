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
    public class UserWhistlistController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserWhistlist
        public ActionResult Index()
        {
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            ViewBag.banner = "Favorilerim";
            var whist = db.Whistlists.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x=>x.Product).Include(x=>x.Product.ProductBrand).ToList();
            var model = new WhistlistViewModel();

            foreach (var item in whist)
            {

                model.WhistlistItems.Add(new WhistlistItemModel { Whistlist=item,Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }

            
            

            return View(model);
        }


      

     
        public ActionResult AddWhistlist(int id,Whistlist whs)
        {
            var product = db.Products.Where(x => x.Id == id && !x.DeletionStatüs).FirstOrDefault();
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            whs.CreatedTime = DateTime.Now;
            whs.UpdatedTime = DateTime.Now;
            whs.DeletionStatüs = false;
            whs.EndUserId = loginuser.Id;
            whs.ProductId = id;
            whs.Price = product.Price;
            
            db.Whistlists.Add(whs);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult RemoveWhist(int id)
        {
            var rmv = db.Whistlists.Where(x => x.Id == id).FirstOrDefault();

            rmv.DeletionStatüs = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}