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
    
    public class OrderHistoryController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: OrtderHistory
        public ActionResult Index()
        {
            ViewBag.banner = "Sipariş Geçmişim";
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            var model = new OrderHistoryViewModel();

            var history = db.Sales.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).OrderByDescending(x => x.Id).ToList();

            foreach (var item in history)
            {
                model.OrderHistoryItems.Add(new OrderHistoryItem { Sale = item, Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }

            return View(model);
        }
    }
}