#region Using
using System.Linq;
using LoginDemo.BLL.Interface.UserAccount;
using LoginDemo.DAL.Interface.UserAccount;
using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
#endregion
namespace LoginDemo.BLL.UserAccount
{
    public class UserAccountBLL : IUserAccountBLL
    {
        #region properties

        private readonly IUserAccountDAL _userAccountDal;
        #endregion

        #region constructor
        public UserAccountBLL(IUserAccountDAL userAccountDal)
        {
            _userAccountDal = userAccountDal;
        }
        #endregion
        public ReturnResponse<UserInfo> Login(UserInfo user)
        {
            var response = new ReturnResponse<UserInfo>();
            var para = new UserInfoQueryParameter() { Account = user.Account, Password = user.Password.Md5Compute32(), Skip = 0, Take = 1, IsPage = false };
            var res = Query(para);
            if (res.IsSuccess && res.Body.Items.Any())
            {
                response.Body = res.Body.Items.FirstOrDefault();
                if (response.Body != null && response.Body.DataStatus == 1)
                {
                    response.ResponseCode = 403;
                    response.Message = "user has been forbidden";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "login success";
                }
            }
            else
            {
                response.ResponseCode = 400;
                response.Message = "user name or password error";
            }
            return response;
        }

        public ReturnResponse<UserInfo> Register(UserInfo userInfo)
        {
            var response = new ReturnResponse<UserInfo>();
            if (string.IsNullOrWhiteSpace(userInfo.Account))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserName can't be empty";
                return response;
            }
            if (string.IsNullOrWhiteSpace(userInfo.Password))
            {
                response.Body = null;
                response.ResponseCode = 100;
                response.Message = "UserPWD can't be empty";
                return response;
            }
            userInfo.Password = userInfo.Password.Md5Compute32();

            var usersRes = Query(new UserInfoQueryParameter() { Account = userInfo.Account, Skip = 0, Take = 1, IsPage = false });

            if (usersRes.ResponseCode == 1 && usersRes.Body.Items.Any())
            {
                response.Body = new UserInfo();
                response.ResponseCode = 400;
                response.Message = "UserName was exist";
                return response;
            }
            var res = _userAccountDal.Save(userInfo); //_IContainer.Resolve<IUserDAL>().Save(user);
            if (res != null && res.ID > 0)
            {
                response.Body = res;
                response.ResponseCode = 1;
                response.Message = "regist success";
            }
            else
            {
                response.Body = res;
                response.ResponseCode = -1;
                response.Message = " Insert failed";
            }
            return response;
        }

        public ReturnResponse<Pager<UserInfo>> Query(UserInfoQueryParameter parameter)
        {
            parameter.Skip = parameter.PageIndex - 1;
            parameter.Take = parameter.PageSize;
            return new ReturnResponse<Pager<UserInfo>>
            {
                Body = _userAccountDal.Query(parameter),
                ResponseCode = 1,
                Message = "Success"
            };
        }
    }
}
