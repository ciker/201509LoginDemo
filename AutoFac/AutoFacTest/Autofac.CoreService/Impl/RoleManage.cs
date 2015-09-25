using System.Collections.Generic;
using System.Linq;
using Autofac.DataModel;
using Autofac.Repository;

namespace Autofac.CoreService.Impl
{
    public class RoleManage : IRoleManage
    {
        private readonly IRoleRepository _roleRepository;
        public RoleManage(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public List<Sys_Roles> GetListRoleIdName()
        {
            return _roleRepository.Entities().ToList();
        }
    }
}
