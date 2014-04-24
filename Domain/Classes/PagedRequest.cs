using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class PagedRequest
    {
        public int? pageSize { get; set; }
        public int? currentPage { get; set; }
        public string sort { get; set; }
        public string filter { get; set; }

    }
}
