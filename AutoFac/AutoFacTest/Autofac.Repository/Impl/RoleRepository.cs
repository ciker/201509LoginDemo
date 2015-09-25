using System.Data.Entity;
using System.Linq;
using Autofac.DataModel;

namespace Autofac.Repository.Impl
{
    public class RoleRepository : EFRepositoryBase<Sys_Roles>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }
    }


}
