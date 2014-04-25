
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Classes {
   public class PagedResponse<T> {

      public PagedResponse(IEnumerable<T> data, int totalRows)
        {
            Data = data;
            TotalRows = totalRows;
        }
        public int TotalRows { get; set; }
        public IEnumerable<T> Data { get; set; }
   }
}