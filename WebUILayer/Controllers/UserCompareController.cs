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
    public class UserCompareController : Controller
    {
        // GET: UserCompare
        SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            ViewBag.banner = "Ürün karşılaştırma sayfası";

            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
         
            var cmp = db.Compares.Where(x => !x.DeletionStatüs && x.EndUserId == loginuser.Id).Include(x => x.Product).Include(x => x.Product.ProductBrand).ToList();
            var model = new CompareViewModel();

            foreach (var item in cmp)
            {

                model.CompareListItemModels.Add(new CompareListItemModel { Compare=item,Product = item.Product, ProductImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList() });

            }




            return View(model);
        }



        public ActionResult AddCompare(int id, Compare cmp)
        {
           
            var loginuser = Session["userlogin"] as EntityLayer.Entity.EndUser;
            cmp.CreatedTime = DateTime.Now;
            cmp.UpdatedTime = DateTime.Now;
            cmp.DeletionStatüs = false;
            cmp.EndUserId = loginuser.Id;
            cmp.ProductId = id;
            

            db.Compares.Add(cmp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult RemoveCompare(int id)
        {
            var rmv = db.Compares.Where(x => x.Id == id).FirstOrDefault();

            rmv.DeletionStatüs = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}