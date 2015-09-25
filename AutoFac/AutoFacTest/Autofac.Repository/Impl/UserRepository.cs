using System.Data.Entity;
using System.Linq;
using Autofac.DataModel;

namespace Autofac.Repository.Impl
{
    public class UserRepository : EFRepositoryBase<Sys_User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
