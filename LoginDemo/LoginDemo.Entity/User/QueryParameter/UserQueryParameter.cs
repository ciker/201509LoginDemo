using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.Commom;

namespace LoginDemo.Entity
{
    public class UserQueryParameter : BaseQueryParameter
    {
        public long? Id { get; set; }

        [IgnoreField]
        public bool IsLogin { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserPWD { get; set; }

        /// <summary>
        /// whether pager
        /// </summary>
        [IgnoreField]
        public bool IsPage { get; set; }
    }
}
