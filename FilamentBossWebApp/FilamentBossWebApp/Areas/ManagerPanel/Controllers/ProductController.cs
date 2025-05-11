using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilamentBossWebApp.Areas.ManagerPanel.Filters;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter] // Yalnızca giriş yapmış yöneticilerin erişebileceği bir filtre
    public class ProductController : Controller
    {
        FilamentBossDbModel db = new FilamentBossDbModel(); // Veritabanı bağlamı

        // Ürünlerin listelendiği ana sayfa
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["ManagerSession"]; // Oturumdaki yöneticiyi al
            int supplierId = manager.Supplier_ID; // Yöneticinin Supplier_ID'sini al

            // Yöneticinin bağlı olduğu tedarikçi ID'sine göre aktif ve silinmemiş ürünleri getir
            var products = db.Products.Where(x => !x.IsDeleted && x.Supplier_ID == supplierId).ToList();

            return View(products); // Ürünleri view'e gönder
        }

        // Ürün detayları sayfası
        public ActionResult Details(int id)
        {
            return View(); // Detayları göstermek için sayfayı döndürür
        }

        // Yeni ürün eklemek için GET isteği
        [HttpGet]
        public ActionResult Create()
        {
            // Kategori ve markaları dropdown list olarak view'e gönder
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Supplier_ID = ((Manager)Session["ManagerSession"]).Supplier_ID; // Yöneticinin Supplier_ID'sini view'e gönder
            return View(); // Create view'ini döndürür
        }

        // Yeni ürün eklemek için POST isteği
        [HttpPost]
        public ActionResult Create(Product Model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid) // Model doğrulaması başarılıysa
            {
                try
                {
                    bool isvalidimage = true;
                    if (image != null)
                    {
                        FileInfo fi = new FileInfo(image.FileName);
                        string extension = fi.Extension;
                        // Sadece .jpg, .jpeg ve .png uzantılı dosyaların yüklenmesine izin verir
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            string name = Guid.NewGuid().ToString() + extension;
                            Model.Image = name;
                            image.SaveAs(Server.MapPath("~/Assets/ProductImages/" + name)); // Resmi sunucuya kaydeder
                        }
                        else
                        {
                            isvalidimage = false;
                            ViewBag.mesaj = "Resim uzantısı .jpg, .jpeg, .png olabilir"; // Geçersiz uzantı hatası
                        }
                    }
                    else
                    {
                        Model.Image = "none.jpg"; // Eğer resim yüklenmemişse, varsayılan resim adı
                    }
                    if (isvalidimage)
                    {
                        Model.CreationTime = DateTime.Now; // Ürünün oluşturulma zamanını ayarlar
                        db.Products.Add(Model); // Ürünü veritabanına ekler
                        db.SaveChanges(); // Değişiklikleri kaydeder
                        TempData["mesaj"] = "Ürün ekleme başarılı"; // Başarı mesajı
                        return RedirectToAction("Index", "Product"); // Ürünler sayfasına yönlendirir
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Ürün eklenirken bir hata oluştu"; // Hata mesajı
                }
            }

            // Kategori ve markaları tekrar view'e gönder
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Supplier_ID = ((Manager)Session["ManagerSession"]).Supplier_ID;
            return View(Model); // Ürün ekleme formunu geri döndürür
        }

        // Ürün düzenleme sayfası
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Product p = db.Products.Find(id); // Ürünü id ile bulur
                if (p != null)
                {
                    if (!p.IsDeleted) // Ürün silinmemişse
                    {
                        // Kategori ve markaları dropdown list olarak view'e gönder
                        ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name", p.Category_ID);
                        ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name", p.Brand_ID);
                        ViewBag.Supplier_ID = ((Manager)Session["ManagerSession"]).Supplier_ID;
                        return View(p); // Ürün bilgilerini düzenlemek için view'i döndürür
                    }
                    else
                    {
                        TempData["systemerror"] = "Ürün silinmiş"; // Hata mesajı
                        return RedirectToAction("Error", "System");
                    }
                }
                else
                {
                    TempData["systemerror"] = "Ürün Bulunamadı"; // Hata mesajı
                    return RedirectToAction("Error", "System");
                }
            }
            else
            {
                return RedirectToAction("Index", "Product"); // Eğer id parametresi yoksa, ürünler sayfasına yönlendirir
            }
        }

        // Ürün düzenleme işlemi için POST isteği
        [HttpPost]
        public ActionResult Edit(Product Model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid) // Model doğrulaması başarılıysa
            {
                try
                {
                    bool isvalidimage = true;
                    if (image != null)
                    {
                        FileInfo fi = new FileInfo(image.FileName);
                        string extension = fi.Extension;
                        // Yalnızca geçerli resim uzantıları kabul edilir
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            string name = Guid.NewGuid().ToString() + extension;
                            Model.Image = name;
                            image.SaveAs(Server.MapPath("~/Assets/ProductImages/" + name)); // Resmi kaydeder
                        }
                        else
                        {
                            isvalidimage = false;
                            ViewBag.mesaj = "Resim uzantısı .jpg, .jpeg, .png olabilir"; // Hata mesajı
                        }
                    }

                    if (isvalidimage)
                    {
                        db.Entry(Model).State = System.Data.Entity.EntityState.Modified; // Ürün düzenleniyor
                        db.SaveChanges(); // Değişiklikleri kaydeder
                        TempData["mesaj"] = "Ürün Düzenleme başarılı"; // Başarı mesajı
                        return RedirectToAction("Index", "Product"); // Ürünler sayfasına yönlendirir
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Ürün düzenlenirken bir hata oluştu"; // Hata mesajı
                }
            }
            return View(Model); // Eğer doğrulama başarısızsa, formu tekrar gösterir
        }

        // Ürün silme işlemi
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Product p = db.Products.Find(id); // Ürünü id ile bulur
                if (p != null)
                {
                    p.IsDeleted = true; // Ürünü silindi olarak işaretler
                    p.IsActive = false; // Ürünü pasif yapar
                    p.IsRecent = false; // Ürünü yeni olarak işaretler
                    db.SaveChanges(); // Değişiklikleri kaydeder
                    TempData["mesaj"] = "Ürün silindi"; // Silme mesajı
                    return RedirectToAction("Index", "Product"); // Ürünler sayfasına yönlendirir
                }
                else
                {
                    TempData["systemerror"] = "Ürün Bulunamadı"; // Hata mesajı
                    return RedirectToAction("Error", "System");
                }
            }
            else
            {
                return RedirectToAction("Index", "Product"); // id parametresi yoksa, ürünler sayfasına yönlendirir
            }
        }

        // Test sayfası
        public ActionResult Test()
        {
            return View(); // Test sayfasını döndürür
        }

        // Dinamik ürün ekleme (partial view) için GET isteği
        [HttpGet]
        public ActionResult _Create()
        {
            // Kategori ve markaları dropdown list olarak view'e gönder
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            return View(); // _Create partial view'ini döndürür
        }
    }
}
