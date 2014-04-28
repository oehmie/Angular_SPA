using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Angular_SPA.Domain.Classes {
   public class PagedRequest {
      public int? pageSize { get; set; }
      public int? currentPage { get; set; }
      public string filter { get; set; }
      public List<string> SortFields { get; set; }
      public List<string> SortDirections { get; set; }

   }
}
