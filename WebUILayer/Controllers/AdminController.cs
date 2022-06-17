using EntityLayer.Modal;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class AdminController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: Admin
        public ActionResult Update(int id)
        {
            var adm = db.Admins.Find(id);
            return View(adm);
        }
        [HttpPost]
        public ActionResult Update(Admin adm,int id,string Password,string newPassword,string validationPassword)
        {
            var psw = db.Admins.Where(x => !x.DeletionStatüs && x.Id == id &&x.Password==adm.Password).FirstOrDefault();
            var upd = db.Admins.Find(id);
            upd.Name = adm.Name;
            upd.Surname = adm.Surname;

            if (Password == "")
            {
                db.SaveChanges();
                ViewBag.message = "Bilgileriniz Güncellenmiştir";

            }
            else
            {

            
            if (psw!=null)
            {
                if (newPassword == validationPassword)
                {
                        if(newPassword =="" && validationPassword == "")
                        {
                            ViewBag.errormessage = "Yeni şifre alanı boş bırakılamaz";
                        }
                        else
                        {
                            if (newPassword == Password)
                            {
                                ViewBag.errormessage = "Yeni şifre eski şifre ile aynı olamaz";
                            }
                            else
                            {
                                upd.Password = newPassword;
                                db.SaveChanges();
                                ViewBag.message = "Bilgileriniz Güncellenmiştir";

                            }
                           

                          
                        }

                       

                }
                else
                {
                    ViewBag.errormessage = "Şifreler uyuşmuyor";
                }

            }
            else
            {
                ViewBag.errormessage = "Mevcut şifreniz hatalı lütfen tekrar deneyiniz";
            }
            }
            return View(upd);

        }
    }
}