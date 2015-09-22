using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;

namespace LoginDemo.BLL.Interface
{
    public interface IUserAccountBLL
    {
        ReturnResponse<UserInfo> Login(UserInfoAndAccount user);

        ReturnResponse<UserInfo> Register(UserInfoAndAccount user);

        ReturnResponse<Pager<UserInfoAndAccount>> Query(UserInfoQueryParameter parameter);

    }
}
