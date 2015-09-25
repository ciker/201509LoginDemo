using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.DataModel;
using Autofac.ViewModel;

namespace Autofac.CoreService
{
    public interface IUserManage
    {
        Sys_User LoadUser(int id);

        Object Users();

        bool SaveUser(UserModel user, out string message);
    }
}
