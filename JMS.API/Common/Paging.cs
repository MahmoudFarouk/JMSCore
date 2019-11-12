using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMS.API.Common
{
    public class PagingProperties
    { 
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class PageResult<T>
    {
        public int TotalItems { get; set; }
        public List<T> PageItems { get; set; }
    }
}
