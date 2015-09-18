using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

namespace LoginDemo.BLL.Interface.UserAccount
{
    public interface IUserAccountBLL
    {
        ReturnResponse<UserInfo> Login(UserInfo user);

        ReturnResponse<UserInfo> Register(UserInfo user);

        ReturnResponse<Pager<UserInfo>> Query(UserInfoQueryParameter parameter);

    }
}
