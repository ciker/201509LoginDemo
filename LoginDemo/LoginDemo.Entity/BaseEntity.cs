
using System;

namespace LoginDemo.Entity
{
    public interface BaseEntity
    {
         bool? IsDelete { get; set; }
         int? DataStatus { get; set; }

         DateTime? CreateDateTime { get; set; }

         DateTime? UpdateDateTime { get; set; }
    }


}
