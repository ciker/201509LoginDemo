using System.Data.Entity;
using System.Linq;
using Autofac.DataModel;

namespace Autofac.Repository.Impl
{
    public class UserRoleRepository : EFRepositoryBase<Sys_User_Roles>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context)
            : base(context)
        {
        }


    }


}
