
using System;

namespace LoginDemo.Entity
{
    // ReSharper disable once InconsistentNaming
    public interface BaseEntity
    {
         bool? IsDelete { get; set; }
         int? DataStatus { get; set; }

         DateTime? CreateDateTime { get; set; }

         DateTime? UpdateDateTime { get; set; }
    }


}
