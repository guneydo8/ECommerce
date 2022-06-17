using EntityLayer.Modal;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;

namespace WebUILayer.Controllers
{
    public class UserLoginController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: UserLogin
        public ActionResult Login()
        {
            ViewBag.banner = "Giriş Ekranı";
            return View();
        }


        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var login = db.EndUsers.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (login != null)
            {
                FormsAuthentication.SetAuthCookie(Email, false);
                Session["userlogin"] = login;
                return RedirectToAction("Index", "UserHome");

            }
            else
            {
                ViewBag.message = "Hatalı mail veya şifre lütfen tekrar deneyiniz";
                return View();
            }


        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }



        public ActionResult Register()
        {
            ViewBag.banner = "Kayıt Ekranı";
            return View();
        }

        [HttpPost]
        public ActionResult Register(EndUser csm, string confirmPassword)
        {

            var email = db.EndUsers.Where(x => x.Email == csm.Email && !x.DeletionStatüs).FirstOrDefault();
            if (email == null)
            {
                if (csm.Password == confirmPassword)
                {


                    csm.CreatedTime = DateTime.Now;
                    csm.UpdatedTime = DateTime.Now;
                    csm.DeletionStatüs = false;
                    db.EndUsers.Add(csm);
                    db.SaveChanges();
                    ViewBag.message = "Sisteme başarılı bir şekilde kayıt oldunuz giriş ekranına yönlendiriliyorsunuz.";

                    return RedirectToAction("Login", "UserLogin");

                }
                else
                {
                    ViewBag.errormessage = "Şifreler uyuşmuyor lütfen tekrar deneyiniz.";
                    return View(csm);

                }

            }
            else
            {
                ViewBag.errormessage = "Mail adresi sistemde kayıtlı lütfen başka bir mail adresi kullanın.";
                return View();
            }




        }

        public ActionResult Recovery()
        {
           ViewBag.banner = "Şifre Sıfırlama Ekranı";
            return View();
            
        }
        

        [HttpPost]
        public ActionResult Recovery(string email)
        {
            var user = db.EndUsers.Where(x => x.Email == email).FirstOrDefault();
            ViewBag.banner = "Şifre Sıfırlama Ekranı";

            if (user != null)
            {
                Random rnd = new Random();
                int password = rnd.Next();
                user.Password = Convert.ToString(password);

                if (ModelState.IsValid)
                {
                    SmtpClient client = new SmtpClient("ni-trio-win.guzelhosting.com", 587);
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("support@360meka.com", "Guz3lH0st!ng360M3K@");
                    client.EnableSsl = false;
                    client.Credentials = credentials;
                    try
                    {

                        db.SaveChanges();
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("deneme0607123@gmail.com", "YÖNETİM - DESTEK BİRİMİ");
                        mailMessage.To.Add(new MailAddress(email, "YÖNETİM - MAIL"));
                        mailMessage.Subject = "Şifreniz Güncellenmiştir";
                        mailMessage.Body = "Yeni Şifreniz:" + password;
                        mailMessage.IsBodyHtml = true;
                        client.Send(mailMessage);
                        ViewBag.sendmessage = "Şifreniz mail adresinize gönderilmştir.";


                    }
                    catch
                    {
                        ViewBag.message = "Mail Gönderirken Bir Hata oluştu";
                    }


                }
            }
            else
            {
                ViewBag.message = "Lütfen Sisteme Kayıt Olduğunuz Mail Adresi ile Giriş Yapın";
            }

            
            return View();
        }
    }
}