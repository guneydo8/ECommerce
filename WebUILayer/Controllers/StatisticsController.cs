using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Entity;
using EntityLayer.Modal;
namespace WebUILayer.Controllers
{
    public class StatisticsController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: Statistics
        public ActionResult Index()
        {

            //ürün sayısı
            ViewBag.product = db.Products.Where(x => !x.DeletionStatüs).Count();

            //kategori sayısı
            ViewBag.category = db.ProductCategories.Where(x => !x.DeletionStatüs).Count();
            //marka sayısı
            ViewBag.brand= db.ProductBrands.Where(x => !x.DeletionStatüs).Count();
            //yorumlanan ürün sayısı
            ViewBag.comment= db.ProductComments.Where(x => !x.DeletionStatüs).Count();
            //müşteri sayısı
            ViewBag.enduser= db.EndUsers.Where(x => !x.DeletionStatüs).Count();
            //mesaj sayısı
            ViewBag.message= db.Contacts.Where(x => !x.DeletionStatüs).Count();
            //toplam stok sayısı
            ViewBag.stock = db.Products.Where(x => !x.DeletionStatüs).Select(x => x.Stock).Sum();
            //satış sayısı
            ViewBag.sale= db.Sales.Where(x => !x.DeletionStatüs).Count();
            //duyuru sayısı
            ViewBag.announcement = db.Announcements.Where(x => !x.DeletionStatüs).Count();
            //favori sayısı
            ViewBag.whistlist = db.Whistlists.Where(x => !x.DeletionStatüs).Count();
            //sepet sayısı
            ViewBag.cart = db.Carts.Where(x => !x.DeletionStatüs).Count();
            //karsılastırmadakı ürün sayısı
            ViewBag.compare = db.Compares.Where(x => !x.DeletionStatüs).Count();

            //En Yüksek Fiyatlı Ürün
            ViewBag.highprice = db.Products.Where(x => !x.DeletionStatüs).OrderByDescending(x => x.Price).FirstOrDefault().Title;
            //En Düşük Fiyatlı Ürün
            ViewBag.lowprice = db.Products.Where(x => !x.DeletionStatüs).OrderBy(x => x.Price).FirstOrDefault().Title;
            //Ürün Görsel Sayısı
            ViewBag.image = db.ProductImages.Where(x => !x.DeletionStatüs).Count();

            //Sıkca Sorulanlar sayısı
            ViewBag.faq= db.Faqs.Where(x => !x.DeletionStatüs).Count();

          
           

            return View();
        }
    }
}