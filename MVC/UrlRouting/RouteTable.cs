using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlRouting
{
    public class RouteTable
    {
        public static RouteCollection Routes { get; set; }

        static RouteTable()
        {
            Routes = new RouteCollection();
        }
    }
}
