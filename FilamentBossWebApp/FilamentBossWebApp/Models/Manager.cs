using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilamentBossWebApp.Models
{
    public class Manager:Entity
    {
        public int ManagerRole_ID { get; set; }
        
        [ForeignKey("ManagerRole_ID")]
        public virtual ManagerRole ManagerRole { get; set; }
        public int Supplier_ID { get; set; }

        [ForeignKey("Supplier_ID")]
        public virtual Supplier Supplier { get; set; }

        [Display(Name="Isim")]
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [StringLength(maximumLength:50,ErrorMessage ="Bu alan en fazla 50 karakter alabilir")]
        public string Name { get; set; }
        
        [Display(Name="Soyİsim")]
        [StringLength(maximumLength:50,ErrorMessage ="Bu alan en fazla 50 karakter alabilir")]
        public string Surname { get; set; }
        
        [Display(Name="E-Posta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [StringLength(maximumLength:200,MinimumLength =5,ErrorMessage ="Bu alan 5 - 200 karakter arasında olabilir")]
        public string Mail { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Bu alan 5 - 30 karaakter arasında olabilir")]
        public string  Password { get; set; }

        public bool IsActive { get; set; }
    }
}