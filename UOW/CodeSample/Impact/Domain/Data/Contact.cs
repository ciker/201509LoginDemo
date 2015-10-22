using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
            IsActive = true;
            CreationDate = DateTime.Now;
        }

        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        public ContactTypeEnum ContactType { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
