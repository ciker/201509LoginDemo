using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        //for soft delete of record
        public bool IsActive { get; set; }
    }
}
