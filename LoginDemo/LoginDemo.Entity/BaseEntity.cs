
using System;

namespace LoginDemo.Entity
{
    public class BaseEntity
    {
        public bool? IsDelete { get; set; }
        public int? DataStatus { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }
    }


}
