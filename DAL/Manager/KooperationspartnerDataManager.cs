using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angular_SPA.DAL.Models;


namespace Angular_SPA.DAL.Manager {

   public class KooperationspartnerDataManager : BaseDataManager {

      public KooperationspartnerDataManager(bool preloading = true, bool lazyLoading = false, bool tracking = false)
         : this(preloading, lazyLoading, tracking, null) {

      }

      /// <summary>
      /// Dieser Konstruktor muss internal sein, denn sonst würde die GL eine Referenz auf EF brauchen!!!
      /// </summary>
      internal KooperationspartnerDataManager(bool preloading = true, bool lazyLoading = false, bool tracking = false, FactoringContext context = null) {
         Init(lazyLoading, tracking, context);
         // Preloading lädt alle Kooperationspartner in den Kontext-Cache
         if (preloading) {
            this.preloading = true;
            context.KooperationspartnerSet.ToList();
         }
      }

      public List<Kooperationspartner> GetKooperationspartner() {
         return (from k in context.KooperationspartnerSet select k).ToList();
          //return context.KooperationspartnerSet.ToList();
      }



   }
}
