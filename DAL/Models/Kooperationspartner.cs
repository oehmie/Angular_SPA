
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Angular_SPA.DAL.Models {

   [Table("kooperationspartner")]
   public class Kooperationspartner {
      [Key]
      public int kp_ID { get; set; }
      public string kp_Firma1 { get; set; }
      public string kp_Firma2 { get; set; }
      public string kp_Firma3 { get; set; }
      public string kp_Name { get; set; }
      public string kp_Strasse { get; set; }
      public string kp_Ort { get; set; }
      public string kp_Tel { get; set; }
      public string kp_Fax { get; set; }
      public string kp_eMail { get; set; }
      public string kp_Web { get; set; }
      public string kp_ProduktName { get; set; }
      public string kp_Kontakt { get; set; }

      
   }
}
