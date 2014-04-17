using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angular_SPA.DAL.Manager;
using Angular_SPA.Domain.Models;

namespace Angular_SPA.Domain.Manager {


   public class KooperationspartnerManager:IDisposable {

      KooperationspartnerDataManager kooperationspartnerDataManager = new KooperationspartnerDataManager(false);

      public void Dispose() { 
         kooperationspartnerDataManager.Dispose();
      }

      public List<Kooperationspartner> GetKooperationspartner() {
         List<Angular_SPA.DAL.Models.Kooperationspartner> kooperationspartnerList = kooperationspartnerDataManager.GetKooperationspartner();
         List<Kooperationspartner> result = new List<Kooperationspartner>();
         foreach (Angular_SPA.DAL.Models.Kooperationspartner kooperationspartner in kooperationspartnerList) {
            result.Add(new Kooperationspartner(kooperationspartner));
         }
         return result;
      }

   }


}
