using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular_SPA.Domain.Models {


   public class WebUser {

      public string UserName { get; set; }
      public string KurzName { get; set; }
      public string Passwort { get; set; }

      public WebUser() {
      }

      public WebUser(DAL.Models.WebUser webUser) {
         UserName = webUser.zu_IZNr;
         KurzName = webUser.zu_KurzName;
         Passwort = webUser.zu_Passwort;
      }

      public DAL.Models.WebUser ToEntity() {
         return new DAL.Models.WebUser() {
            zu_IZNr = UserName,
            zu_KurzName = KurzName,
            zu_Passwort = Passwort
         };
      }

   }
}
