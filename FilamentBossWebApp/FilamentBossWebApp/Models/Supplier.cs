using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilamentBossWebApp.Models
{
    public class Supplier:Entity
    {
        public Supplier()
        {
            IsDeleted = false;
        }

        [Display(Name = "Isim")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(maximumLength: 50, ErrorMessage = "Bu alan en fazla 50 karakter alabilir")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}