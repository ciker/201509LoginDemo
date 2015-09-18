using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

namespace LoginDemo.DAL.Interface.UserAccount
{
    public interface IUserAccountDAL
    {
        Pager<UserInfo> Query(UserInfoQueryParameter para);

        UserInfo Save(UserInfo userInfo);

        UserInfo Update(UserInfo userInfo);

        bool Delete(UserInfo userInfo);
    }
}
