using EntityLayer.Modal;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUILayer.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        SystemContext db = new SystemContext();
        public ActionResult Update()
        {
            ViewBag.banner = "Hesabım";
            var user = Session["userlogin"] as EntityLayer.Entity.EndUser;
            ViewBag.usr = user.Name + " " + user.Surname;
            ViewBag.date = user.BornDate.ToString("d");
            var upd = db.EndUsers.Find(user.Id);

           
            return View(upd);
        }


        [HttpPost]
        public ActionResult Update(EndUser csm, string Password, string newPassword, string validationPassword)
        {
            ViewBag.banner = "Hesabım";
            var user = Session["userlogin"] as EntityLayer.Entity.EndUser;
            var psw = db.EndUsers.Where(x => !x.DeletionStatüs && x.Id == user.Id && x.Password == user.Password).FirstOrDefault();
            var upd = db.EndUsers.Find(user.Id);
            ViewBag.usr = user.Name + " " + user.Surname;
            upd.BornDate = csm.BornDate;
            upd.Name = csm.Name;
            upd.Surname = csm.Surname;
            upd.Country = csm.Country;
            upd.City = csm.City;
            upd.Adress = csm.Adress;
            upd.Phone = csm.Phone;
            
          
           
           


            if (Password == "")
            {
                db.SaveChanges();
                ViewBag.message = "Bilgileriniz Güncellenmiştir";
                Session.Abandon();
                return RedirectToAction("Login", "UserLogin");

            }
            else
            {


                if (psw != null)
                {
                    if (newPassword == validationPassword)
                    {
                        if (newPassword == "" && validationPassword == "")
                        {
                            ViewBag.errormessage = "Yeni şifre alanı boş bırakılamaz";
                            return View();
                        }
                        else
                        {
                            if (newPassword == Password)
                            {
                                ViewBag.errormessage = "Yeni şifre eski şifre ile aynı olamaz";
                                return View();
                            }
                            else
                            {
                                upd.Password = newPassword;
                                db.SaveChanges();
                                ViewBag.message = "Bilgileriniz Güncellenmiştir";
                                Session.Abandon();
                                return RedirectToAction("Login", "UserLogin");
                                

                            }



                        }
                        
                    }
                    else
                    {
                        ViewBag.errormessage = "Şifreler uyuşmuyor";
                        return View();
                    }

                }
                else
                {
                    ViewBag.errormessage = "Mevcut şifreniz hatalı lütfen tekrar deneyiniz";
                    return View();
                }
            }
            
            

        }
       
        
    }
}