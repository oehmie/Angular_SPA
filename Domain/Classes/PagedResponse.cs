using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Angular_SPA.Domain.Classes {
   public class PagedResponse<T> {
      public PagedResponse(IEnumerable<T> result, int totalRows) {
         Data = result;
         TotalRows = totalRows;
      }
      public int TotalRows { get; set; }
      public IEnumerable<T> Data { get; set; }


   }
}
