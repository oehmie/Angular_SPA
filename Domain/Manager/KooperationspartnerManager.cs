using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angular_SPA.DAL.Manager;
using Angular_SPA.Domain.Models;
using Domain.Classes;

namespace Angular_SPA.Domain.Manager {


   public class KooperationspartnerManager:IDisposable {

      KooperationspartnerDataManager kooperationspartnerDataManager = new KooperationspartnerDataManager(false);

      public void Dispose() { 
         kooperationspartnerDataManager.Dispose();
      }

      public PagedResponse<Kooperationspartner> GetKooperationspartner(PagedRequest request) {
         IQueryable<Angular_SPA.DAL.Models.Kooperationspartner> kooperationspartnerList = kooperationspartnerDataManager.GetKooperationspartner();
         int totalRows = kooperationspartnerList.Count();
         kooperationspartnerList = kooperationspartnerList.OrderBy(kp => kp.kp_ID);
         if (request.pageSize.HasValue) {
            if (request.currentPage.HasValue)
               kooperationspartnerList = kooperationspartnerList.Skip((request.currentPage.Value - 1) * request.pageSize.Value);
            kooperationspartnerList = kooperationspartnerList.Take(request.pageSize.Value);
         }
         
         List<Kooperationspartner> result = new List<Kooperationspartner>();
         foreach (Angular_SPA.DAL.Models.Kooperationspartner kooperationspartner in kooperationspartnerList) {
            result.Add(new Kooperationspartner(kooperationspartner));
         }
         return new PagedResponse<Kooperationspartner>(result,totalRows);
      }

   }


}
