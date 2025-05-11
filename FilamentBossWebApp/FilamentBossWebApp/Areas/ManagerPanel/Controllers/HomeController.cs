using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using FilamentBossWebApp.Areas.ManagerPanel.Filters; // Yönetici girişi kontrolü için kullanılan filter sınıfı
using FilamentBossWebApp.Models; // Modelleri kullanabilmek için gerekli namespace

namespace FilamentBossWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter] // Bu filtre, yalnızca yönetici olarak giriş yapmış kullanıcıların erişebileceği controller'a işaret eder
    public class HomeController : Controller
    {
        // Bronz, Silver, Gold için XML dosyalarının son güncelleme zamanlarını tutan statik değişkenler
        private static DateTime lastBronzUpdate = DateTime.MinValue;
        private static DateTime lastSilverUpdate = DateTime.MinValue;
        private static DateTime lastGoldUpdate = DateTime.MinValue;

        // Index Action: Ana sayfa, tedarikçiye ait XML dosyasını yükler ve gösterir
        public ActionResult Index()
        {
            // Giriş yapan yönetici bilgisini Session'dan alır
            var currentManager = (Manager)Session["ManagerSession"];
            if (currentManager == null)
            {
                // Eğer yönetici bilgisi yoksa giriş sayfasına yönlendirir
                return RedirectToAction("Index", "Login");
            }

            // Supplier_ID'ye göre XML dosyasının yolunu belirler
            string xmlPath = GetXmlPathBySupplier(currentManager.Supplier_ID);

            if (string.IsNullOrEmpty(xmlPath))
            {
                // Geçerli bir XML yolu bulunamazsa hata mesajı gösterir
                ViewBag.ErrorMessage = "Geçerli bir XML dosyası bulunamadı.";
                return View();
            }

            // XML dosyasının son değiştirilme zamanını alır
            DateTime lastModified = GetXmlLastModified(xmlPath);
            string message = null;

            // XML dosyasının güncellenip güncellenmediğini kontrol eder
            if (HasXmlBeenUpdated(lastModified, currentManager.Supplier_ID))
            {
                message = "Güncellenmiş veriler mevcut.";
            }

            // XML dosyasındaki ürünleri yükler
            var products = LoadProductsFromXml(xmlPath);

            // Güncelleme mesajını ViewBag ile View'a gönderir
            ViewBag.UpdateMessage = message;
            return View(products);
        }

        // Supplier_ID'ye göre XML dosyasının yolunu döndüren metot
        private string GetXmlPathBySupplier(int supplierId)
        {
            switch (supplierId)
            {
                case 1: // Bronz için yol
                    return @"C:\Export\Bronz.xml";
                case 2: // Silver için yol
                    return @"C:\Export\Silver.xml";
                case 3: // Gold için yol
                    return @"C:\Export\Gold.xml";
                default:
                    // Geçerli bir yol bulunamazsa null döner
                    return null;
            }
        }

        // XML dosyasının son değiştirilme zamanını almak için kullanılan metot
        private DateTime GetXmlLastModified(string xmlPath)
        {
            try
            {
                // Dosyanın son değiştirilme zamanını döndürür
                return System.IO.File.GetLastWriteTime(xmlPath);
            }
            catch (Exception)
            {
                // Hata durumunda minimum tarih döndürülür
                return DateTime.MinValue;
            }
        }

        // XML dosyasının güncellenip güncellenmediğini kontrol eder
        private bool HasXmlBeenUpdated(DateTime lastModified, int supplierId)
        {
            bool isUpdated = false;

            switch (supplierId)
            {
                case 1: // Bronz
                    // Eğer dosyanın son değiştirilme zamanı daha önce kaydedilen zamandan büyükse güncellenmiştir
                    if (lastModified > lastBronzUpdate)
                    {
                        lastBronzUpdate = lastModified; // Güncellenen tarihi kaydeder
                        isUpdated = true;
                    }
                    break;
                case 2: // Silver
                    if (lastModified > lastSilverUpdate)
                    {
                        lastSilverUpdate = lastModified;
                        isUpdated = true;
                    }
                    break;
                case 3: // Gold
                    if (lastModified > lastGoldUpdate)
                    {
                        lastGoldUpdate = lastModified;
                        isUpdated = true;
                    }
                    break;
            }

            return isUpdated;
        }

        // XML dosyasındaki ürünleri yükleyen metot
        private List<XmlProduct> LoadProductsFromXml(string xmlPath)
        {
            // XML dosyasını okur ve XML verilerini listeye dönüştürür
            var xDoc = XDocument.Load(xmlPath);
            var products = xDoc.Descendants("Products")
                .Select(x => new XmlProduct
                {
                    ID = (int?)x.Element("ID") ?? 0, // ID değerini alır, yoksa 0 döner
                    CategoryID = (int?)x.Element("CategoryID") ?? 0, // CategoryID alır
                    BrandID = (int?)x.Element("BrandID") ?? 0, // BrandID alır
                    CategoryName = (string)x.Element("CategoryName") ?? string.Empty, // Kategori ismini alır
                    BrandName = (string)x.Element("BrandName") ?? string.Empty, // Marka ismini alır
                    ProductName = (string)x.Element("ProductName") ?? string.Empty, // Ürün ismini alır
                    Piece = (int?)x.Element("Piece") ?? 0, // Ürün adedi alır
                    Price = (decimal?)x.Element("Price") ?? 0m, // Ürün fiyatını alır
                    Diameter = (decimal?)x.Element("Diameter") ?? 0m, // Ürün çapını alır
                    Color = (string)x.Element("Color") ?? string.Empty // Ürün rengini alır
                })
                .ToList();

            return products;
        }

        // Yenileme butonuna tıklandığında XML dosyasını tekrar yükler
        [HttpPost]
        public ActionResult ReloadXml()
        {
            var currentManager = (Manager)Session["ManagerSession"];
            if (currentManager == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Supplier_ID'ye göre XML dosyasının yolunu belirler
            string xmlPath = GetXmlPathBySupplier(currentManager.Supplier_ID);

            if (string.IsNullOrEmpty(xmlPath))
            {
                ViewBag.ErrorMessage = "Geçerli bir XML dosyası bulunamadı.";
                return View("Index");
            }

            // XML dosyasını okur ve ürünleri yükler
            var products = LoadProductsFromXml(xmlPath);
            ViewBag.UpdateMessage = "Veriler güncellendi."; // Güncelleme mesajı gösterir

            // Güncellenmiş veriler ile Index sayfasını tekrar render eder
            return View("Index", products);
        }
    }
}
