using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Areas.ManagerPanel.Data;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Areas.ManagerPanel.Controllers
{
    public class LoginController : Controller
    {
        FilamentBossDbModel db = new FilamentBossDbModel(); // Veritabanı bağlamı

        // Ana giriş sayfası (GET)
        public ActionResult Index()
        {
            // Eğer daha önce giriş yapan kullanıcının bilgileri tarayıcıda kaydedildiyse (cookie), onları kontrol eder
            if (Request.Cookies["ManagerCookie"] != null)
            {
                HttpCookie SavedCookie = Request.Cookies["ManagerCookie"];
                string mail = SavedCookie.Values["mail"];
                string password = SavedCookie.Values["password"];

                // Veritabanında, mail ve şifre ile eşleşen bir kullanıcı arar
                Manager m = db.Managers.FirstOrDefault(x => x.Mail == mail && x.Password == password);
                if (m != null)
                {
                    if (m.IsActive) // Eğer kullanıcı aktifse
                    {
                        Session["ManagerSession"] = m; // Yönetici oturumunu başlatır
                        return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendirir
                    }
                }
            }

            return View(); // Giriş sayfasını geri döner
        }

        // Kullanıcı giriş işlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF koruması için
        public ActionResult Index(ManagerLoginViewModel model)
        {
            if (ModelState.IsValid) // Model doğrulama işlemi başarılıysa
            {
                // Veritabanında, mail ve şifre ile eşleşen bir kullanıcı arar
                Manager m = db.Managers.FirstOrDefault(x => x.Mail == model.Mail && x.Password == model.Password);
                if (m != null)
                {
                    if (m.IsActive) // Eğer kullanıcı aktifse
                    {
                        // "Beni hatırla" seçeneği işaretlenmişse, giriş bilgilerini cookie'ye kaydeder
                        if (model.RememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("ManagerCookie");
                            cookie["mail"] = model.Mail; // Mail bilgisini cookie'ye ekler
                            cookie["password"] = model.Password; // Şifreyi cookie'ye ekler
                            cookie.Expires = DateTime.Now.AddDays(3); // Cookie'yi 3 gün süreyle geçerli yapar
                            Response.Cookies.Add(cookie); // Cookie'yi tarayıcıya gönderir
                        }
                        Session["ManagerSession"] = m; // Yönetici oturumunu başlatır
                        return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendirir
                    }
                    else
                    {
                        // Eğer kullanıcı aktif değilse, hata mesajı gönderir
                        ViewBag.mesaj = "Kullanıcı hesabınız askıya alınmıştır";
                    }
                }
                else
                {
                    // Eğer kullanıcı bulunamazsa, hata mesajı gönderir
                    ViewBag.mesaj = "Kullanıcı Bulunamadı";
                }
            }
            return View(model); // Modeli geri gönderir, böylece form verileri tekrar görüntülenebilir
        }

        // Çıkış işlemi
        public ActionResult LogOut()
        {
            Session["ManagerSession"] = null; // Yönetici oturumunu sonlandırır
            if (Request.Cookies["ManagerCookie"] != null)
            {
                HttpCookie SavedCookie = Request.Cookies["ManagerCookie"];
                SavedCookie.Expires = DateTime.Now.AddDays(-1); // Cookie'yi geçersiz kılar
                Response.Cookies.Add(SavedCookie); // Cookie'yi tarayıcıdan siler
            }
            return RedirectToAction("Index", "Login"); // Giriş sayfasına yönlendirir
        }
    }
}
