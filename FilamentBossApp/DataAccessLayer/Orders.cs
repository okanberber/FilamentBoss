using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Orders
    {
        public string Name { get; set; }           // Ürünün adı
        public decimal Price { get; set; }         // Ürünün fiyatı
        public short Stock { get; set; }           // Ürünün stok durumu
        public int Supplier_ID { get; set; }       // Tedarikçi ID'si
        public int Category_ID { get; set; }       // Kategori ID'si
        public int Brand_ID { get; set; }          // Marka ID'si
        public DateTime CreationTime { get; set; } // Ürünün oluşturulma tarihi
        public bool IsActive { get; set; }         // Ürünün aktif olup olmadığı
    }
}
