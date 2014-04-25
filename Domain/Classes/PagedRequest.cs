using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Classes {

   public class PagedRequest {

      public int? pageSize { get; set; }
      public int? currentPage { get; set; }
      public string sortField { get; set; }
      public string filter { get; set; }
   }
}