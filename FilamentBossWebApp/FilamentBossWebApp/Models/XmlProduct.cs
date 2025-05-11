using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilamentBossWebApp.Models
{
    public class XmlProduct
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public int Piece { get; set; }
        public decimal Price { get; set; }
        public decimal Diameter { get; set; }
        public string Color { get; set; }
    }
}