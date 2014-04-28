
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Angular_SPA.DAL.Attribute;


namespace Angular_SPA.DAL.Models {

   [Table("kooperationspartner")]
   public class Kooperationspartner {
      [Key]
      [SortName("id")]
      public int kp_ID { get; set; }

      [StringLength(50)]
      public string kp_Firma1 { get; set; }

      [StringLength(50)]
      public string kp_Firma2 { get; set; }

      [StringLength(50)]
      public string kp_Firma3 { get; set; }

      [StringLength(50)]
      [SortName("Name")]
      public string kp_Name { get; set; }

      [StringLength(50)]
      [SortName("Strasse")]
      public string kp_Strasse { get; set; }

      [StringLength(50)]
      [SortName("Ort")]
      public string kp_Ort { get; set; }

      [StringLength(50)]
      [SortName("Telefon")]
      public string kp_Tel { get; set; }

      [StringLength(50)]
      [SortName("Fax")]
      public string kp_Fax { get; set; }

      [StringLength(50)]
      [SortName("Email")]
      public string kp_eMail { get; set; }

      [StringLength(50)]
      [SortName("Web")]
      public string kp_Web { get; set; }

      [StringLength(50)]
      [SortName("Produktname")]
      public string kp_ProduktName { get; set; }

      [StringLength(50)]
      public string kp_Kontakt { get; set; }


   }
}
