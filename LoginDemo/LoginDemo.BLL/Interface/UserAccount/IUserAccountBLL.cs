using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
using LoginDemo.ViewModels.UserInfo;

namespace LoginDemo.BLL.Interface
{
    public interface IUserAccountBLL : IDependency
    {
        ReturnResponse<UserInfoViewModels> Login(UserInfoViewModels user);

        ReturnResponse<UserInfoViewModels> Register(UserInfoViewModels user);

        ReturnResponse<Pager<UserInfoViewModels>> Query(UserInfoQueryParameter parameter);

    }
}
