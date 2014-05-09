using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular_SPA.DAL.Models {


   [Table("Zugang")]
   public class WebUser {

      [Key]
      [StringLength(7)]
      public string zu_IZNr { get; set; }

      [StringLength(54)]
      public string zu_KurzName { get; set; }

      [StringLength(24)]
      public string zu_Passwort { get; set; }
   }
}
