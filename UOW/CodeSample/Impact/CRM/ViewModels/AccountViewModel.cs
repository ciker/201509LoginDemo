using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AccountType { get; set; }
        public int AccountStatus { get; set; }
    }
}