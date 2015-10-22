using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Account : BaseEntity
    {
        public Account()
        {
            IsActive = true;
            CreationDate = DateTime.Now;
            Contacts = new List<Contact>();
        }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Url { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        public AccountTypeEnum AccountType { get; set; }
        [Required]
        public AccountStatusEnum AccountStatus { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
