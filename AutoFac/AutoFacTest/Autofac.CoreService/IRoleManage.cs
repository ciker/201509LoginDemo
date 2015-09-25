using System.Collections.Generic;
using Autofac.DataModel;

namespace Autofac.CoreService
{
   public interface IRoleManage
   {
       List<Sys_Roles> GetListRoleIdName();
   }
}
