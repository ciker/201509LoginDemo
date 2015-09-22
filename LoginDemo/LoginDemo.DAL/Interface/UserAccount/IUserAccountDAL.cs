using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

namespace LoginDemo.DAL.Interface
{
    public interface IUserAccountDAL
    {
        Pager<UserInfoAndAccount> Query(UserInfoQueryParameter para);

        UserInfo Save(UserInfoAndAccount userInfo);

        UserInfo Update(UserInfoAndAccount userInfo);

        bool Delete(UserInfoAndAccount userInfo);
    }
}
