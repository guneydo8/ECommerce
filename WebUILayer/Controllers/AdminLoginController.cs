using EntityLayer.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EntityLayer.Entity;
using System.Web.Helpers;
using System.Net.Mail;

namespace WebUILayer.Controllers
{

    public class AdminLoginController : Controller
    {
        SystemContext db = new SystemContext();
        // GET: AdminLogin



        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Admins.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

            if (email == "" || password == "")
            {
                ViewBag.message = "Lütfen Giriş Bilgilerinizi Giriniz";
                return View();

            }
            else
            {
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["adminlogin"] = user;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.message = "Hatalı kullanıcı adı veya şifre";
                    return View();
                }

            }





        }


        [HttpGet]
        public ActionResult Recovery() => View();

        [HttpPost]
        public ActionResult Recovery(string email)
        {
            var user = db.Admins.Where(x => x.Email == email).FirstOrDefault();

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



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}