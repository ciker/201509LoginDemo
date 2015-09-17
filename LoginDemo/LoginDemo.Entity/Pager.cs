using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Entity
{
    public class Pager<T>
    {
        public int? Total { get; set; }
        public int? Pages { get; set; }
        public T[] Items { get; set; }
    }
}
