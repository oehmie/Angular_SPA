using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_SPA.Classes {

   public class PagedRequest {

      public int? skip { get; set; }
      public int? take { get; set; }
      public string sort { get; set; }
      public string filter { get; set; }
   }
}