using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angular_SPA.DAL.Manager;
using Angular_SPA.Domain.Models;
using Angular_SPA.Domain.Classes;
using Angular_SPA.Domain.Extensions;
using Angular_SPA.DAL.Attribute;

namespace Angular_SPA.Domain.Manager {


   public class KooperationspartnerManager : IDisposable {

      KooperationspartnerDataManager kooperationspartnerDataManager = new KooperationspartnerDataManager(false);

      public void Dispose() {
         kooperationspartnerDataManager.Dispose();
      }

      public PagedResponse<Kooperationspartner> GetKooperationspartner(PagedRequest request) {
         IQueryable<Angular_SPA.DAL.Models.Kooperationspartner> kooperationspartnerList = kooperationspartnerDataManager.GetKooperationspartner();

         //Filter
         if (!String.IsNullOrWhiteSpace(request.filter)) {
            kooperationspartnerList = kooperationspartnerList.Where(k => k.kp_Name.ToLower().Contains(request.filter.ToLower())
                                                                      || k.kp_Ort.ToLower().Contains(request.filter.ToLower())
                                                                      || k.kp_Strasse.ToLower().Contains(request.filter.ToLower())
                                                                      || k.kp_Tel.ToLower().Contains(request.filter.ToLower())
                                                                      || k.kp_Fax.ToLower().Contains(request.filter.ToLower())
               );
         }

         //Sortierung
         if (request.SortFields != null && request.SortDirections != null && request.SortFields.Count() > 0 && request.SortDirections.Count() > 0 && request.SortFields.Count() == request.SortDirections.Count()) {
            for (int fieldNo = 0; fieldNo < request.SortDirections.Count(); fieldNo++) {
               string sortField = SortNameAttribute.GetFieldName(typeof(Angular_SPA.DAL.Models.Kooperationspartner), request.SortFields[fieldNo]);
               if (!String.IsNullOrWhiteSpace(sortField)) {
                  if (request.SortDirections[fieldNo].ToLower() == "asc") {
                     kooperationspartnerList = kooperationspartnerList.OrderBy(sortField);
                  }
                  else {
                     kooperationspartnerList = kooperationspartnerList.OrderByDescending(sortField);
                  }
               }
            }
         }
         else
            kooperationspartnerList = kooperationspartnerList.OrderBy(kp => kp.kp_ID);


         //Gesamtanzahl Datensätze
         int totalRows = kooperationspartnerList.Count();

         //Nur die angeforderte Seite abholen
         if (request.pageSize.HasValue) {
            if (request.currentPage.HasValue)
               kooperationspartnerList = kooperationspartnerList.Skip((request.currentPage.Value - 1) * request.pageSize.Value);
            kooperationspartnerList = kooperationspartnerList.Take(request.pageSize.Value);
         }

         List<Kooperationspartner> result = new List<Kooperationspartner>();
         foreach (Angular_SPA.DAL.Models.Kooperationspartner kooperationspartner in kooperationspartnerList) {
            result.Add(new Kooperationspartner(kooperationspartner));
         }
         return new PagedResponse<Kooperationspartner>(result, totalRows);
      }

   }


}
