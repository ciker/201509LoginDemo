using LoginDemo.Entity;
using LoginDemo.Commom;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

namespace LoginDemo.DAL.Interface
{
    public interface IUserAccountDAL : IDependency
    {
        Pager<UserInfoAccount> Query(UserInfoQueryParameter para);

        UserInfo Save(UserInfoAccount userInfo);

        UserInfo Update(UserInfoAccount userInfo);

        bool Delete(UserInfoAccount userInfo);
    }
}
