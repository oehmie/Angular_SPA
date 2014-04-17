using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular_SPA.Domain.Models {

   public class Kooperationspartner {

      public int Id { get; set; }
      public string Name { get; set; }
      public string Strasse { get; set; }
      public string Ort { get; set; }
      public string Telefon { get; set; }
      public string Fax { get; set; }
      public string Email { get; set; }
      public string Web { get; set; }
      public string Produktname { get; set; }
      public string Kontakt { get; set; }


      public Kooperationspartner() {

      }

      public Kooperationspartner(DAL.Models.Kooperationspartner kooperationspartner) {
         Id = kooperationspartner.kp_ID;
         Name = kooperationspartner.kp_Name;
         Strasse = kooperationspartner.kp_Strasse;
         Ort = kooperationspartner.kp_Ort;
         Telefon = kooperationspartner.kp_Tel;
         Fax = kooperationspartner.kp_Fax;
         Email = kooperationspartner.kp_eMail;
         Web = kooperationspartner.kp_Web;
         Produktname = kooperationspartner.kp_ProduktName;
         Kontakt = kooperationspartner.kp_Kontakt;
      }

      public DAL.Models.Kooperationspartner ToEntity() {
         return new DAL.Models.Kooperationspartner() {
            kp_ID = Id,
            kp_Name = Name,
            kp_Strasse = Strasse,
            kp_Ort = Ort,
            kp_Tel = Telefon,
            kp_Fax = Fax,
            kp_eMail = Email,
            kp_Web = Web,
            kp_ProduktName = Produktname,
            kp_Kontakt = Kontakt
         };
      }
   }
}
