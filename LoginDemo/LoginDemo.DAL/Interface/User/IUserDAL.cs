using LoginDemo.Commom;
using LoginDemo.Entity;

namespace LoginDemo.DAL.Interface
{
    public interface IUserDAL : IDependency
    {
        Pager<User> QueryUsersByParameter(UserQueryParameter para);

        User Save(User user);

        User Update(User user);

        bool Delete(User user);
    }
}
