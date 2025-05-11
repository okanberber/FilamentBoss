using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Areas.ManagerPanel.Filters; // Filter sınıfı için gerekli namespace
using FilamentBossWebApp.Models; // Model sınıflarının kullanılabilmesi için gerekli namespace

namespace FilamentBossWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter] // Bu filtre, sadece giriş yapmış kullanıcıların bu controller'a erişmesini sağlar.
    public class BrandController : Controller
    {
        // Veritabanı işlemleri için db bağlantısı
        FilamentBossDbModel db = new FilamentBossDbModel();

        // Index Action: Marka listesini görüntülemek için kullanılır
        public ActionResult Index()
        {
            // Giriş yapan kullanıcının Supplier_ID'sini alır
            Manager manager = (Manager)Session["ManagerSession"];
            int supplierId = manager.Supplier_ID;

            // Veritabanından, silinmemiş ve giriş yapan kullanıcının tedarikçisine ait markaları alır
            var brands = db.Brands.Where(x => !x.IsDeleted && x.Supplier_ID == supplierId).ToList();

            // Markaları View'e gönderir
            return View(brands);
        }

        // Create Action (GET): Marka ekleme formunu gösterir
        [HttpGet]
        public ActionResult Create()
        {
            // Giriş yapan kullanıcının Supplier_ID'sini ViewBag aracılığıyla View'e gönderir
            ViewBag.Supplier_ID = ((Manager)Session["ManagerSession"]).Supplier_ID;
            return View();
        }

        // Create Action (POST): Marka eklemek için veriyi alır
        [HttpPost]
        public ActionResult Create(Brand model)
        {
            // Model doğrulama kontrolü yapılır
            if (ModelState.IsValid)
            {
                try
                {
                    // Veritabanına yeni marka ekler
                    db.Brands.Add(model);
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Başarıyla ekledikten sonra, Index sayfasına yönlendirir
                    return RedirectToAction("Index", "Brand");
                }
                catch
                {
                    // Hata durumunda mesaj gösterilir
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }

        // Edit Action (GET): Marka düzenleme formunu gösterir
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // ID parametresinin null olmaması kontrol edilir
            if (id != null)
            {
                // Veritabanından düzenlenecek markayı alır
                Brand c = db.Brands.Find(id);
                if (c != null)
                {
                    // Markayı View'a gönderir
                    return View(c);
                }
            }
            // Eğer marka bulunamazsa, Index sayfasına yönlendirir
            return RedirectToAction("Index", "Brand");
        }

        // Edit Action (POST): Marka düzenlemesi için veriyi alır
        [HttpPost]
        public ActionResult Edit(Brand model)
        {
            // Model doğrulama kontrolü yapılır
            if (ModelState.IsValid)
            {
                try
                {
                    // Markayı veritabanında günceller
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Başarıyla güncelleme yaptıktan sonra mesaj gönderir ve Index sayfasına yönlendirir
                    TempData["mesaj"] = "Kategori güncelleme başarılı";
                    return RedirectToAction("Index", "Brand");
                }
                catch
                {
                    // Hata durumunda mesaj gösterilir
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }

        // Delete Action: Marka silme işlemini gerçekleştirir
        public ActionResult Delete(int? id)
        {
            // ID parametresinin null olmaması kontrol edilir
            if (id != null)
            {
                // Veritabanından silinecek markayı bulur
                Brand c = db.Brands.Find(id);
                if (c != null)
                {
                    // Markanın silindiğini işaretler (IsDeleted ve IsActive bayrakları ile)
                    c.IsDeleted = true;
                    c.IsActive = false;
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Silme işleminden sonra başarılı mesajını gösterir
                    TempData["mesaj"] = "Marka silme işlemi başarılı";
                }
            }
            // Silme işleminden sonra Index sayfasına yönlendirir
            return RedirectToAction("Index", "Brand");
        }
    }
}
