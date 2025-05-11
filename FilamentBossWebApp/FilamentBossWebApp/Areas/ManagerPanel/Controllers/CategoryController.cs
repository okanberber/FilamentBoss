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
    public class CategoryController : Controller
    {
        // Veritabanı işlemleri için db bağlantısı
        FilamentBossDbModel db = new FilamentBossDbModel();

        // Index Action: Kategori listesini görüntülemek için kullanılır
        public ActionResult Index()
        {
            // Giriş yapan kullanıcının Supplier_ID'sini alır
            Manager manager = (Manager)Session["ManagerSession"];
            int supplierId = manager.Supplier_ID;

            // Veritabanından, silinmemiş ve giriş yapan kullanıcının tedarikçisine ait kategorileri alır
            var categories = db.Categories.Where(x => !x.IsDeleted && x.Supplier_ID == supplierId).ToList();

            // Kategorileri View'e gönderir
            return View(categories);
        }

        // Create Action (GET): Kategori ekleme formunu gösterir
        [HttpGet]
        public ActionResult Create()
        {
            // Giriş yapan kullanıcının Supplier_ID'sini ViewBag aracılığıyla View'e gönderir
            ViewBag.Supplier_ID = ((Manager)Session["ManagerSession"]).Supplier_ID;
            return View();
        }

        // Create Action (POST): Kategori eklemek için veriyi alır
        [HttpPost]
        public ActionResult Create(Category model)
        {
            // Model doğrulama kontrolü yapılır
            if (ModelState.IsValid)
            {
                try
                {
                    // Veritabanına yeni kategori ekler
                    db.Categories.Add(model);
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Başarıyla ekledikten sonra, Index sayfasına yönlendirir
                    return RedirectToAction("Index", "Category");
                }
                catch
                {
                    // Hata durumunda mesaj gösterilir
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }

        // Edit Action (GET): Kategori düzenleme formunu gösterir
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // ID parametresinin null olmaması kontrol edilir
            if (id != null)
            {
                // Veritabanından düzenlenecek kategoriyi alır
                Category c = db.Categories.Find(id);
                if (c != null)
                {
                    // Kategoriyi View'a gönderir
                    return View(c);
                }
            }
            // Eğer kategori bulunamazsa, Index sayfasına yönlendirir
            return RedirectToAction("Index", "Category");
        }

        // Edit Action (POST): Kategori düzenlemesi için veriyi alır
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            // Model doğrulama kontrolü yapılır
            if (ModelState.IsValid)
            {
                try
                {
                    // Kategoriyi veritabanında günceller
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Başarıyla güncelleme yaptıktan sonra mesaj gönderir ve Index sayfasına yönlendirir
                    TempData["mesaj"] = "Kategori güncelleme başarılı";
                    return RedirectToAction("Index", "Category");
                }
                catch
                {
                    // Hata durumunda mesaj gösterilir
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }

        // Delete Action: Kategori silme işlemini gerçekleştirir
        public ActionResult Delete(int? id)
        {
            // ID parametresinin null olmaması kontrol edilir
            if (id != null)
            {
                // Veritabanından silinecek kategoriyi bulur
                Category c = db.Categories.Find(id);
                if (c != null)
                {
                    // Kategorinin silindiğini işaretler (IsDeleted ve IsActive bayrakları ile)
                    c.IsDeleted = true;
                    c.IsActive = false;
                    db.SaveChanges(); // Veritabanında değişiklikleri kaydeder

                    // Silme işleminden sonra başarılı mesajını gösterir
                    TempData["mesaj"] = "Kategori silme işlemi başarılı";
                }
            }
            // Silme işleminden sonra Index sayfasına yönlendirir
            return RedirectToAction("Index", "Category");
        }
    }
}
