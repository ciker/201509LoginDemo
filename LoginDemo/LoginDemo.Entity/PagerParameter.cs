using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Entity
{
    public class PagerParameter
    {
        public string TableName { get; set; }

        public string Fields { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string Condition { get; set; }
        public string Orderby { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
    }
}
