using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

// ReSharper disable once CheckNamespace
namespace LoginDemo.DAL.Interface
{
    // ReSharper disable once InconsistentNaming
    public interface IUserInfoDAL : IDependency
    {

        Pager<UserInfo> Query(UserInfoQueryParameter userInfo);
        UserInfo Save(UserInfo userInfo);

        UserInfo Update(UserInfo userInfo);

        int Delete(UserInfo userInfo);
    }
}
