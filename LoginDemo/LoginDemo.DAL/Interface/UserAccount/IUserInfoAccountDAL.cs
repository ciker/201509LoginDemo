using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

// ReSharper disable once CheckNamespace
namespace LoginDemo.DAL.Interface
{
    // ReSharper disable once InconsistentNaming
    public interface IUserInfoAccountDAL : IDependency
    {
        Pager<UserInfoAccount> Query(UserInfoQueryParameter userInfo);
        UserInfoAccount Save(UserInfoAccount userInfo);

        UserInfoAccount Update(UserInfoAccount userInfo);

        int Delete(UserInfo userInfo);
    }
}
