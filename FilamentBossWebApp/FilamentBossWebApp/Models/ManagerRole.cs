using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace FilamentBossWebApp.Models
{
    public class ManagerRole:Entity
    {
        [Display(Name="Rol")]
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        public string Name { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }

    }
}