using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.ViewModel
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserTrueName { get; set; }
        public string Password { get; set; }
        public List<int> Role { get; set; }
    }
}
