using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMF.Demo.Site.Models
{
    public class MemberView
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime AddDate { get; set; }

        public int LoginLogCount { get; set; }

        public IEnumerable<string> RoleNames { get; set; }
    }
}
