
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_SPA.Classes {
   public class PagedResponse<T> {

      public PagedResponse(IEnumerable<T> result, int totalRows, int totalPages)
        {
            Result = result;
            TotalRows = totalRows;
            TotalPages = totalPages;
        }
        public int TotalRows { get; set; }
        public IEnumerable<T> Result { get; set; }
        public int TotalPages { get; set; }
   }
}