using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Service.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(Expression<Func<User, bool>> where);

        IEnumerable<User> GetMany(Expression<Func<User, bool>> where);

        int Insert(User user);

        int Update(User user);

        int Delete(int Id);

        int Delete(User user);

        int Delete(Expression<Func<User, bool>> where);


    }
}
